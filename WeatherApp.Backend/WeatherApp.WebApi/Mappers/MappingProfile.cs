using AutoMapper;
using WeatherApp.DataContract.Api;
using WeatherApp.DomainModel.DomainModels;

namespace WeatherApp.WebApi.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<WeatherRecordDomainModel, WeatherResponseModel>();
        }
    }
}
