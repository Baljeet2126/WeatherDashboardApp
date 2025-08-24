using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DomainModel.Configurations;
using WeatherApp.DomainModel.Contracts;
using WeatherApp.DomainModel.DomainModels;

namespace WeatherApp.Service
{
    public class WeatherApiClient : IWeatherApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseApiUrl;
        private readonly WeatherAppConfiguration _configuration;
        private ILogger _log;
        
        public WeatherApiClient(  HttpClient httpClient, IConfigurationService configuration, ILogger<WeatherApiClient> logger ) {
            _httpClient = httpClient;
            _log = logger;
            _apiKey = "";
            _baseApiUrl = "";
            _configuration = configuration.GetWeatherAppConfiguration();
        }
        public async Task<WeatherClientApiRespone> GetCurrentWeatherRecordAsync(string cityName, string units)
        {
            var url = $"{_configuration.BaseApiUrl}/weather?q={cityName}&appId={_configuration.ApiKey}&units={units}";
            var response = await _httpClient.GetFromJsonAsync <WeatherClientApiRespone>(url);
            return response ?? new WeatherClientApiRespone();
        }
    }
}
