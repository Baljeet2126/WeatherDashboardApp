using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.DomainModel.Contracts
{
    public interface ICacheService
    {
        Task<T?> GetByKeyAsync<T>(string key);
        Task AddToCache<T>(string key, T value, TimeSpan expiration);
        Task DeleteByKeyAsync<T>(string key);
    }
}
