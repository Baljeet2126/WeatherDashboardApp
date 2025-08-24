using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.DataAccess.Context
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            string baseDirectory = AppContext.BaseDirectory;
            Console.WriteLine($"Database baseDirectory Path: {baseDirectory}");

            string solutionRoot = Path.Combine(baseDirectory, "..", "..", "..", "..");
            Console.WriteLine($"Database solutionRoot Path: {solutionRoot}");
            string relativePathToDatabase = Path.Combine(solutionRoot,"Database", "WeatherAppDB.db");
            // Get the absolute path
            string absolutePath = Path.GetFullPath(relativePathToDatabase);
            Console.WriteLine($"Database Absolute Path: {absolutePath}");
            var sqliteConnectionString = $"Data Source={absolutePath};";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlite(sqliteConnectionString);
            return new DataContext(optionsBuilder.Options);
        }
    }
}
