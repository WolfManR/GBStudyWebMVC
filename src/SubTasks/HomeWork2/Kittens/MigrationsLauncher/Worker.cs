using System.Linq;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using DataBase.EF;
using Microsoft.EntityFrameworkCore;
using DataBase.Abstractions.Entities;
using Microsoft.Extensions.Logging;

namespace MigrationsLauncher
{
    public class Worker : IHostedService
    {
        private readonly IDbContextFactory<KittensContext> _contextFactory;
        private readonly ILogger<Worker> _logger;

        public Worker(IDbContextFactory<KittensContext> contextFactory, ILogger<Worker> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await using var context = _contextFactory.CreateDbContext();
            if ((await context.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
            {
                await context.Database.MigrateAsync(cancellationToken);
            }

            var result = await context.Database.ExecuteSqlRawAsync(
                @"UPDATE public.""Patient""
            SET ""PatientType"" = 'kitten'
            WHERE ""PatientType"" = ''; ",
                cancellationToken);
            _logger.LogInformation("Updated rows = {count}", result);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}