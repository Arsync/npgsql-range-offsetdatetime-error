using InstantIssueApp.Helpers;
using InstantIssueApp.Mappings;
using InstantIssueApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InstantIssueApp.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;

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
            if (b.IsConfigured)
                return;

            b.UseNpgsql(_connectionString,
                o =>
                {
                    o.UseNodaTime();
                });
        }
    }
}
