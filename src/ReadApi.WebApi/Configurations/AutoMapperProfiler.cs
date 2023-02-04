using AutoMapper;
using ReadApi.Application.Contracts;
using ReadApi.Core.Entities;

namespace ReadApi.WebApi.Configurations
{
    public class AutoMapperProfiler : Profile
    {
        public AutoMapperProfiler()
        {
            CreateMap<DtoViolation, Violation>()
                .ForMember(d=>d.VioId, s=> s.MapFrom(src =>src.PKId))
                .ReverseMap();
            CreateMap<DtoMedia, Media>().ReverseMap();

           
            
            CreateMap<DtoEvent, Violation>()
               // .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ForSourceMember(d =>d.id,opt =>opt.DoNotValidate())            
                .ForMember(d=>d.ImageUID, s => s.MapFrom(src=>src.id))
                .ForMember(d => d.LicenseNumber, s => s.MapFrom(src => src.plate))
                .ForMember(d => d.SystemId, s => s.MapFrom(src => src.systemId))
                .ForMember(d=>d.Location, s=>s.MapFrom(src=>src.systemInfo.name))
                .ForMember(d=>d.ThresholdSpeed,s=>s.MapFrom(src=>src.systemInfo.thresholdSpeed))
                .ForMember(d=>d.PostedSpeed, s=>s.MapFrom(src=>src.systemInfo.postedSpeed))
                .ForMember(d=>d.locationId, s=>s.MapFrom(src=>src.locationInfo.id))
                .ForMember(d=>d.gpsLat, s=>s.MapFrom(src=>src.locationInfo.gpsLat))
                .ForMember(d=>d.gpsLong, s=>s.MapFrom(src=>src.locationInfo.gpsLong))
                .ForMember(d=>d.TotalDistanceTravelled, s=>s.MapFrom(src=>src.locationInfo.distanceFromPrevious))
                ;

        }
    }
}
