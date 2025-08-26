using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeatherApp.DataAccess.Context;
using WeatherApp.DataAccess.Models;
using WeatherApp.DomainModel.Configurations;
using WeatherApp.DomainModel.Contracts;
using WeatherApp.DomainModel.DomainModels;
using WeatherApp.DomainModel.Enums;

namespace WeatherApp.DataAccess.Repositories
{
    public class WeatherAppRepository : IWeatherAppRepository
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IConfigurationService _configurationService;
        private readonly WeatherAppConfiguration _weatherAppConfiguration;
        public WeatherAppRepository(
            DataContext context,
            IMapper autoMapper,
            ILogger<WeatherAppRepository> logger,
            IConfigurationService configurationService)
        {

            _dbContext = context;
            _mapper = autoMapper;
            _logger = logger;
            _configurationService = configurationService;
            _weatherAppConfiguration = _configurationService.GetWeatherAppConfiguration();
        }
        public async Task AddWeatherRecordAsyc(WeatherRecordDomainModel weatherRecordDomainModel)
        {
            try
            {
                var weatherRecord = new WeatherRecord()
                {
                    CityId = weatherRecordDomainModel.CityId,
                    Description = weatherRecordDomainModel.Description,
                    Temperature = weatherRecordDomainModel.Temperature,
                    SunriseTime = weatherRecordDomainModel.SunriseTime,
                    SunsetTime = weatherRecordDomainModel.SunsetTime,
                    RecordedAt = weatherRecordDomainModel.RecordedAt
                };
                await _dbContext.WeatherStats.AddAsync(weatherRecord);
                await _dbContext.SaveChangesAsync();
               // _logger.LogInformation($"Weather record saved for city {weatherRecord.City.Name} at {weatherRecord.RecordedAt}");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to saved weather stats for {weatherRecordDomainModel.CityName}");
                throw;
            }
        }

        public async Task<IEnumerable<CityDomainModel>> GetCitiesAsync()
        {
            try
            {
                return await _dbContext.Cities.AsNoTracking()
                    .Select(city => new CityDomainModel
                    {
                        Id = city.Id,
                        Name = city.Name,
                        Country = city.Country

                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch cities.");
                return Enumerable.Empty<CityDomainModel>();
            }
        }


        public async Task<WeatherRecordDomainModel?> GetLatestWeatherReportAsync(string cityName)
        {
            try
            {
                return await _dbContext.WeatherStats
                    .AsNoTracking()
                    .Where(w => w.City.Name == cityName)
                    .OrderByDescending(w => w.RecordedAt)
                    .Select(weatherDetail => new WeatherRecordDomainModel
                    {
                        Id = weatherDetail.Id,
                        CityName = weatherDetail.City.Name,
                        CityId = weatherDetail.City.Id,
                        Description = weatherDetail.Description,
                        Icon = "",
                        Temperature = weatherDetail.Temperature,
                        SunriseTime = weatherDetail.SunriseTime,
                        SunsetTime = weatherDetail.SunsetTime,
                        RecordedAt = weatherDetail.RecordedAt
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch latest weather report for {CityName}", cityName);
                return null;
            }
        }

        public async Task<IEnumerable<WeatherRecordDomainModel>?> GetWeatherHistoryAsync(string cityName)
        {
            try
            {
                return await _dbContext.WeatherStats
                    .AsNoTracking()
                    .Where(w => w.City.Name == cityName)
                    .OrderBy(w => w.RecordedAt)
                    .Select(weatherDetail => new WeatherRecordDomainModel
                    {
                        Id = weatherDetail.Id,
                        CityName = weatherDetail.City.Name,
                        CityId = weatherDetail.City.Id,
                        Description = weatherDetail.Description,
                        Icon = "",
                        Temperature = weatherDetail.Temperature,
                        SunriseTime = weatherDetail.SunriseTime,
                        SunsetTime = weatherDetail.SunsetTime,
                        RecordedAt = weatherDetail.RecordedAt
                    })
                    .Take(_weatherAppConfiguration.HistoryDayCount)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch weather history report for {CityName}", cityName);
                return null;
            }
        }

        public async Task SaveUserPerference(UserPreferenceDomainModel userPreference)
        {
            try
            {
                var userPerference = await _dbContext.UserPreferences.FirstOrDefaultAsync(u => u.UserId == userPreference.UserId);
                if (userPerference == null)
                {
                    userPerference = new UserPreference
                    {
                        UserId = userPreference.UserId,
                        TemperatureUnit = userPreference.TemperatureUnit
                    };
                    await _dbContext.UserPreferences.AddAsync(userPerference);
                }
                else
                {
                    userPerference.TemperatureUnit = userPreference.TemperatureUnit;
                    _dbContext.UserPreferences.Update(userPerference);
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to save user preference for userId {userPreference.UserId}");
                throw;
            }
        }

        public async Task<UserPreferenceDomainModel?> GetUserPerference(string userId)
        {
            try
            {
                return await _dbContext.UserPreferences
                    .Where(w => w.UserId == userId)
                    .Select(u=> new UserPreferenceDomainModel
                    {
                        UserId = u.UserId,
                        TemperatureUnit = u.TemperatureUnit
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to fetch user perference for {userId}");
                return null;
            }
        }
    }
}
