namespace WeatherApp.DomainModel.Configurations
{
    public class DatabaseConfiguration
    {
        public const string SectionName = "DatabaseSettings";
        public string DatabaseFile { get; set; } = string.Empty;
    }
}
