using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DomainModel.Enums;

namespace WeatherApp.DomainModel.Configurations
{
    public class WeatherAppConfiguration
    {
        public const string SectionName = "WeatherDashboard";
        public string ApiKey { get; set; } = string.Empty;

        public string BaseApiUrl { get; set; } = string.Empty;

        public int CacheExiprationMinutes { get; set; } = 10;

        public TemperatureUnit DefualtTemperatureUnit { get; set; } = TemperatureUnit.Metric;

        public int HistoryDayCount { get; set; } = 5;
    }
}
