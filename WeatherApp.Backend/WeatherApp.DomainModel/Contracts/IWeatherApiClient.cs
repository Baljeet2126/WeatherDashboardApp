using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DomainModel.DomainModels;

namespace WeatherApp.DomainModel.Contracts
{
    public interface IWeatherApiClient
    {
        Task<WeatherClientApiRespone> GetCurrentWeatherRecordAsync(string cityName,string units);
    }
}
