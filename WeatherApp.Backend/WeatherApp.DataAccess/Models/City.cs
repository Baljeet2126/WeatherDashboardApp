using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherApp.DataAccess.Models
{
    [Table("City")]
    public class City
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Country { get; set; } = string.Empty;
        public ICollection<WeatherRecord> WeatherRecords { get; set; } = [];
    }

    public class CityEntityConfiguration : IEntityTypeConfiguration<City>
    {
        readonly List<City> cities = new()
                    {
                        new() {
                            Id = 1,
                            Name = "Vienna",
                            Country = "Austria"
                        },
                        new() {
                            Id = 2,
                            Name = "London",
                            Country = "UK"

                         },
                        new()
                        {
                            Id = 3,
                            Name = "Ljubljana",
                            Country ="Slovenia",
                        },
                        new()
                        {
                            Id = 4,
                            Name = "Belgrade",
                            Country ="Serbia",
                        },
                        new()
                        {
                            Id = 5,
                            Name = "Valletta",
                            Country ="Malta",
                        }
                     };

        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasMany(v => v.WeatherRecords)
            .WithOne(b => b.City)
            .HasForeignKey(b => b.CityId);
            builder.HasData(cities);
        }
    }
}
