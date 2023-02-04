using System;
using System.Drawing.Imaging;
using System.Drawing;
using AuditQueue.DbModels;
using AuditQueue.Models;
using AuditQueue.Services;
using Dapper;
using Npgsql;
using System.Security.Policy;
using Amazon.S3;
using Amazon.S3.Model;

namespace AuditQueue.Repo
{
	public class ViolationRepo : IViolationRepo
    {
        private const string TABLE_NAME = "violations";
        private readonly NpgsqlConnection connection;
        private readonly ILogger<ViolationRepo> logger;
        private readonly IConfiguration configuration;
       private readonly IAmazonS3 s3Client;

        private  string? CONNECTION_STRING { get; set; }

        public ViolationRepo(ILogger<ViolationRepo> logger, IConfiguration configuration) // IAmazonS3 s3Client)
		{
            CONNECTION_STRING = configuration["ConnectionStrings:DbConnection"];

            connection = new NpgsqlConnection(CONNECTION_STRING);
            connection.Open();
            this.logger = logger;
            this.configuration = configuration;
           // this.s3Client = s3Client;
            logger.LogInformation($"Connection open for Database");
        }

        public async Task<int> AddAsync(Violation violation)
        {
            string commandText = $"INSERT INTO {TABLE_NAME} (id, location, systemid, threshold, vehiclespeed,status,transdate) " +
                $" VALUES (@id, @location, @systemid, @threshold, @vehiclespeed, @status, @transdate)";

            var queryArguments = new
            {
                id = violation.Id,
                location = violation.Location,
                systemid = violation.SystemId,
                threshold = violation.ThresholdSpeed,
                vehiclespeed = violation.VehicleSpeed,
                status = violation.Status,
                transdate = DateTime.UtcNow
            };

           return await connection.ExecuteAsync(commandText, queryArguments);
        }

        public async Task<int> DeleteAsync(long id)
        {
            string commandText = $"DELETE FROM {TABLE_NAME} WHERE ID=(@p)";

            var queryArguments = new
            {
                p = id
            };

            return await connection.ExecuteAsync(commandText, queryArguments);
        }

        public async Task<Violation> GetAsync(long id)
        {
            string commandText = $"SELECT * FROM {TABLE_NAME} WHERE ID = @id";

            var queryArgs = new { Id = id };
            var violation = await connection.QueryFirstAsync<Violation>(commandText, queryArgs);
            return violation;
        }

        public async Task<IEnumerable<Violation>> GetAllAsync()
        {
            string commandText = $"SELECT * FROM {TABLE_NAME}";
            var violations = await connection.QueryAsync<Violation>(commandText);

            return violations;
        }

        public async Task UpdateAsync(long id)
        {
            var commandText = $@"UPDATE {TABLE_NAME}
                SET status = 1
                WHERE id = @id ; UPDATE media
                SET status = 1
                WHERE violation_id = @id";

            var queryArgs = new
            {
                ID = id
            };

            await connection.ExecuteAsync(commandText, queryArgs);
        }

        public async Task GetAlprImageAsync(AlprDataDto alprDataDto)
        {
            var bytes = new byte[1024]; //await httpService.DownloadAlprImageAsync(alprDataDto);
            //if image not available may be add something on database in the future
            if (bytes.Length > 0)
            {
                HttpClient _client = new HttpClient();
                HttpResponseMessage responseMessage = await _client.GetAsync("Pass your url");
                Stream inputStream = await responseMessage.Content.ReadAsStreamAsync();

              Bitmap bmp1 = new Bitmap(inputStream);

                var path = @"C:\dev\working\autonotice\UvedMediaUploader\UvedMediaUploader\File\image.jpg";
                Bitmap bmp = new(path);

                DataBarRepo repo = new DataBarRepo();

                Bitmap bitmap = repo.ResizeAndAddDatabar(bmp, new DataBar
                {

                    Location = "Highway 270 South, Beaver, OK",
                    ViolationDate = DateTime.Now.ToString("MM/dd/yyyy"),
                    ViolationTime = DateTime.Now.ToShortTimeString()
                }, 960, 720);
                bitmap.Save(@"C:\dev\working\autonotice\UvedMediaUploader\UvedMediaUploader\File\image1.jpg", ImageFormat.Jpeg);

                var modelImageUpload = new ImageUpload
                {
                    FileName = alprDataDto.BestUuid
                   // Location= alprDataDto.,
                };

                var updatedBase64String = AddDataBarAndGetString(modelImageUpload, bytes);
                await UploadImageAsync(updatedBase64String, configuration["S3BucketName"].ToString(), modelImageUpload.FileName.ToLower(), "JPG".ToLower());
            }

        }

        private static string AddDataBarAndGetString(ImageUpload modelImageUpload, byte[] imageDataByteArray)
        {
            string Imagestring = string.Empty;

            byte[] bytes;// System.IO.File.ReadAllBytes(file);
            using (var memoryStream = new MemoryStream(imageDataByteArray))
            {
                bytes = memoryStream.ToArray();
            }

            DataBarRepo repo = new DataBarRepo();
            Bitmap bmp = repo.BytesToBitmap(bytes);

            Bitmap bitmap = repo.ResizeAndAddDatabar(bmp, new DataBar
            {

                Location = modelImageUpload?.Location?.Trim(),
                ViolationDate = modelImageUpload?.ViolationDate,
                ViolationTime = modelImageUpload?.ViolationTime
            }, 960, 720);

            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                Imagestring = Convert.ToBase64String(ms.GetBuffer()); //Get Base64
            }
            return Imagestring;
        }

        public async Task<string> UploadImageAsync(string base64String, string bucketName, string fileName, string fileExtension)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(base64String);

                var putResponse = new PutObjectResponse();

                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    // CannedACL = S3CannedACL.NoACL,
                    Key = $"{configuration["S3Key"]}/{fileName}.{fileExtension}"
                };
                using (var ms = new MemoryStream(bytes))
                {
                    request.InputStream = ms;
                    putResponse = await s3Client.PutObjectAsync(request);
                }
                //return putResponse;

                //var url = _s3Client.GetPreSignedURL(new GetPreSignedUrlRequest
                //{
                //    BucketName = bucketName,
                //    Key = $"{fileName}.{fileExtension}",
                //    Expires = DateTime.Now.AddMinutes(5)
                //});

                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }

    }
}

