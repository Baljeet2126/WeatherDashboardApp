using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DomainModel.Enums;

namespace WeatherApp.DomainModel.DomainModels
{
    public class WeatherRecordDomainModel
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; } = string.Empty;

        public double Temperature { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Icon { get; set; } = string.Empty;

        public DateTime SunriseTime {  get; set; } 

        public DateTime SunsetTime { get; set; }

        public DateTime RecordedAt { get; set; }

        public TemperatureTrend Trend { get; set; }
    }
}
