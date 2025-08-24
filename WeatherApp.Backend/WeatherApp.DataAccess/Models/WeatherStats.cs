using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherApp.DataAccess.Models
{
    [Table("WeatherStatistics")]
    public class WeatherRecord
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; } = null!;

        [Required]
        public double Temperature { get; set; }

        [Required, MaxLength(200)]
        public string Description { get; set; } = String.Empty;

        [Required]

        public DateTime SunriseTime { get; set; }

        [Required]

        public DateTime SunsetTime { get; set; }

        [Required]
        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
    }

    public class WeatherRecordEntityConfiguration : IEntityTypeConfiguration<WeatherRecord>
    {
        public void Configure(EntityTypeBuilder<WeatherRecord> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Temperature).IsRequired();
            builder.Property(w => w.RecordedAt).IsRequired();

            // Relationships
            builder.HasOne(w => w.City)
                .WithMany(c => c.WeatherRecords)
                .HasForeignKey(w => w.CityId);

           //Indexes
            builder.HasIndex(w => new { w.CityId, w.RecordedAt });
        }
    }
}
