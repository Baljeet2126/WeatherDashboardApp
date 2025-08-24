using Microsoft.EntityFrameworkCore;
using WeatherApp.DataAccess.Models;

namespace WeatherApp.DataAccess.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<City> Cities => Set<City>();

        public DbSet<UserPreference> UserPreferences => Set<UserPreference>();
        public DbSet<WeatherRecord> WeatherStats => Set<WeatherRecord>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CityEntityConfiguration());
            modelBuilder.ApplyConfiguration(new WeatherRecordEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserPreferenceEntityConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=WeatherAppDB.db");
            }
        }
    }
}
