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
        private readonly KittensContext _context;

        public Worker(KittensContext context) => _context = context;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if ((await _context.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
            {
                await _context.Database.MigrateAsync(cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}