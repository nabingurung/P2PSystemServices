using AutoMapper;
using ReadApi.DTOModels;
using ReadApi.Models;

namespace ReadApi.Configurations
{
    public class AutoMapperProfiler: Profile
    {
        public AutoMapperProfiler()
        {
            CreateMap<DtoViolation, Violation>();
            CreateMap<DtoMedia, Media>();
          
                
        }
    }
}
