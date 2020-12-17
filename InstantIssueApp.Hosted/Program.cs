using InstantIssueApp.Extensions;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace InstantIssueApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args)
                .UseConsoleLifetime(opts => opts.SuppressStatusMessages = true)
                .Build();

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
