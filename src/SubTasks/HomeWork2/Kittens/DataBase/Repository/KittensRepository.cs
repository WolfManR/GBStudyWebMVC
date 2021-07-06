using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.EF;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Repository
{
    public class KittensRepository : IKittensRepository
    {
        private readonly IDbContextFactory<KittensContext> _contextFactory;

        public KittensRepository(IDbContextFactory<KittensContext> contextFactory) => _contextFactory = contextFactory;

        public async Task<List<Kitten>> Get()
        {
            await using var context = _contextFactory.CreateDbContext();
            return await context.Kittens.ToListAsync();
        }

        public async Task Add(Kitten kitten)
        {
            await using var context = _contextFactory.CreateDbContext();
            await context.AddAsync(kitten);
            await context.SaveChangesAsync();
        }
    }
}
