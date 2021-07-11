using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.EF;
using DataLayer.Abstractions.Entities;
using DataLayer.Abstractions.Repositories;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repository
{
    public class KittensRepository : IKittensRepository
    {
        private readonly IDbContextFactory<KittensContext> _contextFactory;

        public KittensRepository(IDbContextFactory<KittensContext> contextFactory, IMapper mapper) => _contextFactory = contextFactory;

        public async Task<List<Kitten>> Get()
        {
            await using var context = _contextFactory.CreateDbContext();
            return await context.Kittens.ProjectToType<Kitten>().ToListAsync();
        }

        public async Task Add(Kitten kitten)
        {
            await using var context = _contextFactory.CreateDbContext();
            await context.AddAsync(kitten);
            await context.SaveChangesAsync();
        }
    }
}
