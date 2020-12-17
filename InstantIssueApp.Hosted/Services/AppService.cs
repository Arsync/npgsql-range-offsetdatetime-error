using InstantIssueApp.Contexts;
using InstantIssueApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InstantIssueApp.Services
{
    public class AppService : IHostedService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly IHostApplicationLifetime _lifetime;

        public AppService(IDbContextFactory<ApplicationDbContext> dbContextFactory, IHostApplicationLifetime lifetime)
        {
            _dbContextFactory = dbContextFactory;
            _lifetime = lifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Connection string is moved to appsettings.json

            // Adding the data
            using (ApplicationDbContext db = _dbContextFactory.CreateDbContext())
            {
                if (!db.SampleModels.Any())
                {
                    var model1 = new SampleModel();
                    var model2 = new SampleModel();

                    db.SampleModels.AddRange(model1, model2);

                    // InvalidCastException: Can't cast database type tstzrange to NpgsqlRange`1
                    db.SaveChanges();
                }
            }

            // Manual insert to test second action:
            // INSERT INTO public.sample_models (id) VALUES (1)

            // Retreiving data
            using (ApplicationDbContext db = _dbContextFactory.CreateDbContext())
            {
                //var items = db.SampleModels.ToList();

                var q = (from item in db.SampleModels
                         select item);

                Console.WriteLine("Models list:");

                // Exception if data exists:
                // Unable to cast object of type 'NodaTime.Instant' to type 'NodaTime.OffsetDateTime'.
                //
                foreach (var item in q)
                {
                    Console.WriteLine($"{item.Id} created at {item.CreationTime}");
                }
            }

            //_lifetime.StopApplication();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
