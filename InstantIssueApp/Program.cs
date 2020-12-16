using InstantIssueApp.Contexts;
using InstantIssueApp.Models;
using System;
using System.Linq;

namespace InstantIssueApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connString = "";

            // Context constructor is calling Database.EnsureCreated();

            // Adding data
            using (ApplicationDbContext db = new ApplicationDbContext(connString))
            {
                if (!db.SampleModels.Any())
                {
                    var model1 = new SampleModel();
                    var model2 = new SampleModel();

                    db.SampleModels.AddRange(model1, model2);

                    //InvalidCastException: Can't cast database type tstzrange to NpgsqlRange`1
                    db.SaveChanges();
                }
            }

            //// Retreiving data
            using (ApplicationDbContext db = new ApplicationDbContext(connString))
            {
                //var items = db.SampleModels.ToList();

                var q = (from item in db.SampleModels
                         select item);

                Console.WriteLine("Models list:");

                foreach (var item in q)
                {
                    Console.WriteLine($"{item.Id} created at {item.CreationTime}");
                }
            }
        }
    }
}
