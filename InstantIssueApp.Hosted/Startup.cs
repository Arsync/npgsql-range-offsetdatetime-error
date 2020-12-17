using EasyCaching.InMemory;
using EFCoreSecondLevelCacheInterceptor;
using InstantIssueApp.Configurations;
using InstantIssueApp.Contexts;
using InstantIssueApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace InstantIssueApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Add an in-memory cache service provider
            // More info: https://easycaching.readthedocs.io/en/latest/In-Memory/
            services.AddEasyCaching(options =>
            {
                options.UseInMemory(config =>
                {
                    config.DBConfig = new InMemoryCachingOptions
                    {
                        // scan time, default value is 60s
                        ExpirationScanFrequency = 60,
                        // total count of cache items, default value is 10000
                        SizeLimit = 100,

                        // enable deep clone when reading object from cache or not, default value is true.
                        EnableReadDeepClone = false,
                        // enable deep clone when writing object to cache or not, default value is false.
                        EnableWriteDeepClone = false,
                    };

                    // the max random second will be added to cache's expiration, default value is 120
                    config.MaxRdSecond = 120;
                    // whether enable logging, default is false
                    config.EnableLogging = false;
                    // mutex key's alive time(ms), default is 5000
                    config.LockMs = 5000;
                    // when mutex key alive, it will sleep some time, default is 300
                    config.SleepMs = 300;

                }, CachingProviders.EntityFrameworkCache);
            });

            services.AddEFSecondLevelCache(options =>
            {
                options.UseEasyCachingCoreProvider(
                    CachingProviders.EntityFrameworkCache,
                    CacheExpirationMode.Absolute,
                    TimeSpan.FromMinutes(5)
                )
                .DisableLogging(true);
            });

            services.AddDbContextFactory<ApplicationDbContext>((provider, options) =>
            {
                // This changes exception message:
                options.AddInterceptors(
                    provider.GetRequiredService<SecondLevelCacheInterceptor>());

                ApplicationDbContext.Configure(options);
            });

            services.AddHostedService<AppService>();
        }
    }
}
