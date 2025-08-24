using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherApp.DomainModel.DomainModels
{
    public class WeatherClientApiRespone
    {
        [JsonPropertyName("main")]
        public MainData MainInfo {  get; set; } = new MainData();

        [JsonPropertyName("weather")]
        public WeatherData[] WeatherInfo { get; set; } = Array.Empty<WeatherData>();

        [JsonPropertyName("sys")]
        public SysData SysInfo { get; set; } = new SysData();
        public string Name { get; set; } = string.Empty;


        public class MainData
        {
            public double Temp { get; set; }
        }

        public class WeatherData
        {
            public string Description { get; set; } = string.Empty;
            public string Icon { get; set; } = string.Empty;
        }

        public class SysData
        {
            public string Country {  get; set; } = string.Empty;
            public long Sunrise { get; set; }
            public long Sunset { get; set; }

           
        }
    }
}
