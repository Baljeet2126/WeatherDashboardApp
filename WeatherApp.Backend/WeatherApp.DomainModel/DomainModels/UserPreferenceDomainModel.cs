using WeatherApp.DomainModel.Enums;

namespace WeatherApp.DomainModel.DomainModels
{
    public class UserPreferenceDomainModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;

        public TemperatureUnit TemperatureUnit { get; set; }

        public bool ShowSunriseOrSunSet { get; set; } = true;
    }
}
