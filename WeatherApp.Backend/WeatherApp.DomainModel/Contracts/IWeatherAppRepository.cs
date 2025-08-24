using WeatherApp.DomainModel.DomainModels;
using WeatherApp.DomainModel.Enums;

namespace WeatherApp.DomainModel.Contracts
{
    public interface IWeatherAppRepository
    {

        Task<IEnumerable<CityDomainModel>> GetCitiesAsync();
        Task AddWeatherRecordAsyc(WeatherRecordDomainModel weatherRecord);
        Task<IEnumerable<WeatherRecordDomainModel>?> GetWeatherHistoryAsync(string cityName);
        Task SaveUserPerference(UserPreferenceDomainModel userPreference);
        Task<UserPreferenceDomainModel?> GetUserPerference(string userId);


    }
}
