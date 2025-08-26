using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeatherApp.DomainModel.Enums;

namespace WeatherApp.DataAccess.Models
{
    [Table("UserPreference")]
    public class UserPreference
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string UserId { get; set; } = string.Empty;
        public TemperatureUnit TemperatureUnit { get; set; } = TemperatureUnit.Metric;

        public bool ShowSunrise { get; set; } = true;
    }

    public class UserPreferenceEntityConfiguration : IEntityTypeConfiguration<UserPreference>
    {
        public void Configure(EntityTypeBuilder<UserPreference> builder)
        {

            builder.HasData(new UserPreference()
            { Id = 1, UserId = "DemoUser" });
        }
    }
}
