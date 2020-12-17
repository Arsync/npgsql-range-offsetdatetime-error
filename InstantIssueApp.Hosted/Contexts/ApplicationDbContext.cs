using InstantIssueApp.Helpers;
using InstantIssueApp.Mappings;
using InstantIssueApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace InstantIssueApp.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<SampleModel> SampleModels { get; set; }

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.ApplyConfiguration(new SampleModelMap());
            b.ToSnakeCase(); // As in big project, just for sure.
        }

        protected override void OnConfiguring(DbContextOptionsBuilder b)
        {
            Configure(b);
        }

        public static void Configure(DbContextOptionsBuilder b)
        {
            if (b.IsConfigured)
                return;

            b.UseNpgsql(GetDefaultConnectionString(),
                o =>
                {
                    o.UseNodaTime();
                });
        }

        private static string GetDefaultConnectionString()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            return connectionString;
        }
    }
}
