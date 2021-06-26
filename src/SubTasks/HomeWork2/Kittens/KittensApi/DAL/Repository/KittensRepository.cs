using KittensApi.Models;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace KittensApi.DAL.Repository
{
    public class KittensRepository : IKittensRepository
    {
        private readonly IDbContextFactory<KittensContext> contextFactory;

        public KittensRepository(IDbContextFactory<KittensContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<List<Kitten>> Get()
        {
            await using var context = contextFactory.CreateDbContext();
            return await context.Kittens.ToListAsync();
        }

        public async Task Add(Kitten kitten)
        {
            await using var context = contextFactory.CreateDbContext();
            await context.AddAsync(kitten);
            await context.SaveChangesAsync();
        }
    }
}
