using ReadApi.Application.Interfaces.EfCore;
using ReadApi.Core.Entities;
using ReadApi.Infrastructure.Peristence.EfCore;

namespace ReadApi.Infrastructure.Repository.EfCore
{
    public class MediaRepositoryCore : GenericRepositoryCore<Media>, IMediaRepositoryCore
    {
        public MediaRepositoryCore(AppDbContext context) : base(context)
        {
        }
    }
}
