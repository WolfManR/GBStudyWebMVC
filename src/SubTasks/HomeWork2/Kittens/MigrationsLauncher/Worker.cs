using System.Linq;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using DataBase.EF;
using Microsoft.EntityFrameworkCore;

namespace MigrationsLauncher
{
    public class Worker : IHostedService
    {
        private readonly IDbContextFactory<KittensContext> _contextFactory;

        public Worker(IDbContextFactory<KittensContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await using var context = _contextFactory.CreateDbContext();
            if ((await context.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
            {
                await context.Database.MigrateAsync(cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}