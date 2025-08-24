using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WeatherApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPreference",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    TemperatureUnit = table.Column<int>(type: "INTEGER", nullable: false),
                    ShowSunriseOrSunSet = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreference", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Temperature = table.Column<double>(type: "REAL", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    SunriseTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SunsetTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RecordedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherStatistics_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[,]
                {
                    { 1, "Austria", "Vienna" },
                    { 2, "UK", "London" },
                    { 3, "Slovenia", "Ljubljana" },
                    { 4, "Serbia", "Belgrade" },
                    { 5, "Malta", "Valletta" }
                });

            migrationBuilder.InsertData(
                table: "UserPreference",
                columns: new[] { "Id", "ShowSunriseOrSunSet", "TemperatureUnit", "UserId" },
                values: new object[] { 1, true, 0, "DemoUser" });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherStatistics_CityId_RecordedAt",
                table: "WeatherStatistics",
                columns: new[] { "CityId", "RecordedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPreference");

            migrationBuilder.DropTable(
                name: "WeatherStatistics");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
