using ReadApi.Models;

namespace ReadApi.Peristence
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly ViolationDbContext violationDbContext;

        public DataAccessProvider(ViolationDbContext violationDbContext)
        {
            this.violationDbContext = violationDbContext;
        }
        public async Task<int> AddNewViolationAsync(Violation violation)
        {
            //var media = new Media
            //{
            //    Id = Guid.NewGuid(),
            //    CreatedDate = DateTime.UtcNow,
            //    ImageUID =  Guid.NewGuid().ToString(),
            //    LastUpdatedDate = DateTime.UtcNow,
            //    Status = 0,
            //    Violation = violation
            //};
            //violation.Medias = media;
           await violationDbContext.ViolationSet.AddAsync(violation);

            //var media = new Media
            //{
            //    CreatedDate = DateTime.UtcNow,
            //    ImageUID = new Guid().ToString(),
            //    LastUpdatedDate = DateTime.UtcNow,
            //    Status = 0,

            //    Violation = violation
            //};

            // var media = violation.Medias.FirstOrDefault();

            //await violationDbContext.Media.AddAsync(
            //        new Media()
            //        {
            //            CreatedDate = DateTime.UtcNow,
            //            LastUpdatedDate = DateTime.UtcNow,
            //            ImageUID = new Guid().ToString(),
            //            Status = 0,
            //            Violation = violation
            //        }
            //    );
            return await violationDbContext.SaveChangesAsync();
        }
    }
}
