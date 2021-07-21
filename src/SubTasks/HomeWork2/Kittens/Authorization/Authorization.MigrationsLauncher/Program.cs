using System;
using System.Threading.Tasks;
using Authorization.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Authorization.MigrationsLauncher
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Func<string[], IHostBuilder> hostedFunc = CreateHostBuilder;
            await hostedFunc.StartHostedWork(Work);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
#if DEBUG
            .UseEnvironment("Development")
#endif
            .ConfigureServices((host, services) => services
                .AddDbContext<AuthorizationContext>(options => options.UseNpgsql(host.Configuration.GetConnectionString("Default"))));

        static async ValueTask Work(IServiceProvider services)
        {
            var db = services.Get<AuthorizationContext>();
            await db.Database.MigrateAsync();
        }
    }

    static class DiExtensions
    {
        public static TService Get<TService>(this IServiceProvider provider)
        {
            try
            {
                return provider.GetRequiredService<TService>();
            }
            catch
            {
                return default;
            }
        }

        public static async Task StartHostedWork(this Func<string[], IHostBuilder> hostBuild, Func<IServiceProvider, ValueTask> behavior)
        {
            if (hostBuild is null) throw new InvalidOperationException("Host not configured");
            using var host = hostBuild.Invoke(Environment.GetCommandLineArgs()).Build();
            await host.StartAsync();

            await behavior.Invoke(host.Services);

            await host.StopAsync();
        }
    }
}
