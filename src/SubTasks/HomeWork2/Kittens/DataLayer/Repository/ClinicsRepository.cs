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
        private readonly IMapper _mapper;
        public ClinicsRepository(IDbContextFactory<KittensContext> contextFactory, IMapper mapper) : base(contextFactory)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<Kitten>> ListKittensInClinic(int clinicId)
        {
            await using var context = ContextFactory.CreateDbContext();
            var clinic = await context.Clinics.AsNoTracking().Include(c => c.Kittens).SingleOrDefaultAsync(c => c.Id == clinicId);
            return clinic is null 
                ? Array.Empty<Kitten>() 
                : _mapper.Map<IEnumerable<Kitten>>(clinic.Kittens);
        }

        public async Task RegisterKittenInClinic(int clinicId, int kittenId)
        {
            await using var context = ContextFactory.CreateDbContext();

            var clinic = await context.Clinics.SingleOrDefaultAsync(c => c.Id == clinicId);
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

            var clinic = await context.Clinics.SingleOrDefaultAsync(c => c.Id == clinicId);
            if (clinic is null) throw new ArgumentException("Clinic not exist");

            var kitten = await context.Kittens.SingleOrDefaultAsync(c => c.Id == kittenId);
            if (kitten is null) throw new ArgumentException("Kitten not exist");

            if (!clinic.Kittens.Contains(kitten)) return;
            clinic.Kittens.Remove(kitten);

            await context.SaveChangesAsync();
        }
    }
}