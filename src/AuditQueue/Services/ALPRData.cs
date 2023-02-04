using AuditQueue.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditQueue.Services
{
    public class ALPRData : IALPRData
    {
       // private readonly IHttpService httpService;

        //public ALPRData(IHttpService httpService)
        //{
        //    this.httpService = httpService;
        //}
       
        public async Task GetAlprImageAsync(AlprDataDto alprDataDto)
        {
            var bytes = new byte[1024]; //await httpService.DownloadAlprImageAsync(alprDataDto);
            //if image not available may be add something on database in the future
            if (bytes.Length >0)
            {
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
            }
            
        }

        private static string AddDataBarAndGetString(ViolationInfo modelImageUpload, byte[] imageDataByteArray)
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
    }
}
