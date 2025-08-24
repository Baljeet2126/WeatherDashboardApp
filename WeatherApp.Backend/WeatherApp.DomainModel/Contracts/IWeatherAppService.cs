using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DomainModel.DomainModels;
using WeatherApp.DomainModel.Enums;

namespace WeatherApp.DomainModel.Contracts
{
    public interface IWeatherAppService
    {
       // Task<WeatherRecordDomainModel> GetCurrentWeatherRecordAsync(string cityName);
        Task<IEnumerable<WeatherRecordDomainModel>> GetCurrentWeatherRecordsForAllCitiesAsync(string userId);

        Task<Boolean> SaveUserPreferenceAsync(UserPreferenceDomainModel userPreference);

        Task<UserPreferenceDomainModel> GetUserPreferenceAsync(string userId);

        Task<TemperatureTrend> GetTemperatureTrendAsync(string cityName);

       // Task<Dictionary<string,string>> GetTemperatureTrendForAllCitiesAsync();


    }
}
