using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using WeatherApp.DomainModel.Contracts;
using WeatherApp.DomainModel.DomainModels;
using WeatherApp.DomainModel.Enums;

namespace WeatherApp.Service
{
    public class WeatherAppService : IWeatherAppService
    {

        private IWeatherAppRepository _repository;
        private IWeatherApiClient _apiClient;
        private readonly ICacheService _cacheService;
        private readonly ILogger _logger;
        private readonly TimeSpan _cacheExiration;
        private readonly IConfigurationService _configurationService;
        public WeatherAppService(
            IWeatherAppRepository repository,
            IWeatherApiClient weatherApiClient,
            ICacheService cacheService,
            ILogger<WeatherAppService>  loggger,
            IConfigurationService configurationService
            
            ) 
        {
            _repository = repository;
            _apiClient = weatherApiClient;
            _cacheService = cacheService;
            _configurationService = configurationService;
            _logger = loggger;
            _cacheExiration = TimeSpan.FromMinutes( configurationService.GetWeatherAppConfiguration().CacheExiprationMinutes);
        }
      

        public  async Task<IEnumerable<WeatherRecordDomainModel>> GetCurrentWeatherRecordsForAllCitiesAsync(string userId)
        {
            const string cacheKey = "current_weather_all";
            var cachedData = await GetCurrentWeatherDetailsFromCache(cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            var cities = await _repository.GetCitiesAsync();
            var weatherRecords = new List<WeatherRecordDomainModel>();
            var userTemperatureUnitPreference = await GetUserPreferenceAsync(userId);
            foreach (var city in cities)
            {
                try
                {
                   
                    var weatherApiResponse = await _apiClient.GetCurrentWeatherRecordAsync(city.Name, userTemperatureUnitPreference.TemperatureUnit.ToString());
                    var weatherRecordDomainModel = WeatherApiResponseMapper(weatherApiResponse);
                    weatherRecordDomainModel.CityId = city.Id;
                    await _repository.AddWeatherRecordAsyc(weatherRecordDomainModel);
                    var trend = await GetTemperatureTrendAsync(city.Name);
                    weatherRecordDomainModel.Trend = trend;
                    weatherRecords.Add(weatherRecordDomainModel);
                }
                catch (Exception ex) {
                    _logger.LogError(ex, $"Failed to fetch weather for {city.Name}");
                }
                
            }
            await _cacheService.AddToCache(cacheKey, weatherRecords.OrderBy(x=>x.RecordedAt), _cacheExiration);
            return weatherRecords.OrderBy(x => x.RecordedAt);
        }

        public  async Task<TemperatureTrend> GetTemperatureTrendAsync(string cityName)
        {

            var weatherHistory = await _repository.GetWeatherHistoryAsync(cityName);
            if(weatherHistory == null || weatherHistory.Count() < 2)
            {
                return TemperatureTrend.Stable;
            }
            else
            {
                var intialWeatherData = weatherHistory.First().Temperature;
                var lastRecordedWeatherData = weatherHistory.Last().Temperature;
                if (lastRecordedWeatherData > intialWeatherData)
                {
                    return TemperatureTrend.Rising;
                }
                if (lastRecordedWeatherData < intialWeatherData)
                {
                    return TemperatureTrend.Falling;
                }
                else return TemperatureTrend.Stable;
            }
        }

        public Task<Dictionary<string, string>> GetTemperatureTrendForAllCitiesAsync()
        {
            throw new NotImplementedException();
        }

        #region UserPreference
        public async Task<UserPreferenceDomainModel> GetUserPreferenceAsync(string userId)
        {
            var cacheKey = $"UserTemperaturePreference_{userId}";
            var cachedUserPreference = await GetUserPreferenceFromCache(cacheKey);

            if (cachedUserPreference == null)
            {
                var userPreference = await _repository.GetUserPerference(userId);

                if (userPreference != null)
                {

                    await _cacheService.AddToCache(cacheKey, userPreference, _cacheExiration);
                }

                return userPreference ?? new UserPreferenceDomainModel
                {
                    UserId = userId,
                    TemperatureUnit = TemperatureUnit.Metric,
                    ShowSunriseOrSunSet = true
                };
            }

            return cachedUserPreference;
        }

        public async Task<bool> SaveUserPreferenceAsync(UserPreferenceDomainModel userPreference)
        {
            var cacheKey = $"UserTemperaturePreference_{userPreference.UserId}";
            await _repository.SaveUserPerference(userPreference);
            await _cacheService.AddToCache(cacheKey, userPreference, _cacheExiration);
            return true;
        }
        #endregion

        private async Task<IEnumerable<WeatherRecordDomainModel>?> GetCurrentWeatherDetailsFromCache(string key)
        {
            var cachedWeatherDetails = await _cacheService.GetByKeyAsync<IEnumerable<WeatherRecordDomainModel>>(key);
            if (cachedWeatherDetails != null)
            {
                return cachedWeatherDetails;
            }
            return null;
        }

        private async Task<UserPreferenceDomainModel?> GetUserPreferenceFromCache(string key)
        {
            var cachedUserPreference = await _cacheService.GetByKeyAsync<UserPreferenceDomainModel>(key);
            if (cachedUserPreference != null)
            {
                return cachedUserPreference;
            }
            return null;
        }

        private static WeatherRecordDomainModel WeatherApiResponseMapper(WeatherClientApiRespone weatherClientApiRespone)
        {
            return new WeatherRecordDomainModel()
            {
                CityName = weatherClientApiRespone.Name,
                Description = weatherClientApiRespone.WeatherInfo.FirstOrDefault().Description,
                Icon = weatherClientApiRespone.WeatherInfo.FirstOrDefault().Icon,
                Temperature = weatherClientApiRespone.MainInfo.Temp,
                SunriseTime = DateTimeOffset.FromUnixTimeSeconds( weatherClientApiRespone.SysInfo.Sunrise).UtcDateTime,
                SunsetTime = DateTimeOffset.FromUnixTimeSeconds(weatherClientApiRespone.SysInfo.Sunset).UtcDateTime,
                RecordedAt = DateTime.UtcNow
            };
        }

        public Task<WeatherRecordDomainModel> GetCurrentWeatherRecordAsync(string cityName)
        {
            throw new NotImplementedException();
        }

    }
}
