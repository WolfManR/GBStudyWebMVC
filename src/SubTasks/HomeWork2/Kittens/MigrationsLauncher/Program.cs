﻿using DataBase.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MigrationsLauncher
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureAppConfiguration(
            (host, builder) =>
            {
                builder.AddUserSecrets<Program>();
            }).ConfigureServices(ConfigureServices);

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddDbContext<KittensContext>(options =>
                options
                    .UseNpgsql(
                        host.Configuration.GetConnectionString("Default"),
                        builder => builder.MigrationsAssembly(nameof(MigrationsLauncher)))
                    .UseLoggerFactory(LoggerFactory.Create(builder =>
                    {
                        builder
                            .AddConsole()
                            .AddFilter((category, logLevel) =>
                                category == DbLoggerCategory.Database.Command.Name &&
                                logLevel == LogLevel.Information);
                    })), 
                ServiceLifetime.Singleton);
            services.AddHostedService<Worker>();
        }
    }
}
