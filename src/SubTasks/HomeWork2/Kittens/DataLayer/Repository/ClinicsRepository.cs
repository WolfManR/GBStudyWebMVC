using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.EF;
using DataLayer.Abstractions.Entities;
using DataLayer.Abstractions.Repositories;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

using dbEntities = DataBase.Abstractions.Entities;

namespace DataLayer.Repository
{
    public class ClinicsRepository : KittensContextRepository<Clinic, dbEntities::Clinic, int>, IClinicsRepository
    {
        public ClinicsRepository(IDbContextFactory<KittensContext> contextFactory, IMapper mapper) : base(contextFactory, mapper)
        {
        }

        public async Task<IEnumerable<Kitten>> ListKittensInClinic(int clinicId)
        {
            await using var context = ContextFactory.CreateDbContext();
            var clinic = await context.Clinics.AsNoTracking().Include(c => c.Kittens).SingleOrDefaultAsync(c => c.Id == clinicId);
            return clinic is null 
                ? Array.Empty<Kitten>() 
                : Mapper.Map<IEnumerable<Kitten>>(clinic.Kittens);
        }

        public async Task RegisterKittenInClinic(int clinicId, int kittenId)
        {
            await using var context = ContextFactory.CreateDbContext();

            var clinic = await context.Clinics.Include(c => c.Kittens).SingleOrDefaultAsync(c => c.Id == clinicId);
            if (clinic is null) throw new ArgumentException("Clinic not exist");

            var kitten = await context.Kittens.SingleOrDefaultAsync(c => c.Id == kittenId);
            if (kitten is null) throw new ArgumentException("Kitten not exist");

            if (clinic.Kittens.Contains(kitten)) return;
            clinic.Kittens.Add(kitten);

            await context.SaveChangesAsync();
        }

        public async Task UnRegisterKittenFromClinic(int clinicId, int kittenId)
        {
            await using var context = ContextFactory.CreateDbContext();

            var clinic = await context.Clinics.Include(c => c.Kittens).SingleOrDefaultAsync(c => c.Id == clinicId);
            if (clinic is null) throw new ArgumentException("Clinic not exist");

            var kitten = await context.Kittens.SingleOrDefaultAsync(c => c.Id == kittenId);
            if (kitten is null) throw new ArgumentException("Kitten not exist");

            if (!clinic.Kittens.Contains(kitten)) return;
            clinic.Kittens.Remove(kitten);

            await context.SaveChangesAsync();
        }
    }
}