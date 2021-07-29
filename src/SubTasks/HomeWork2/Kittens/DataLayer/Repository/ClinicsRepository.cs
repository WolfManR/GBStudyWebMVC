using System;
using System.Collections.Generic;
using System.Linq;
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
        public ClinicsRepository(KittensContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<IEnumerable<Kitten>> ListKittensInClinic(int clinicId)
        {
            var clinic = await Context.Clinics.AsNoTracking().Include(c => c.Patients).SingleOrDefaultAsync(c => c.Id == clinicId);
            return clinic is null 
                ? Array.Empty<Kitten>() 
                : Mapper.Map<IEnumerable<Kitten>>(clinic.Patients.OfType<dbEntities::Kitten>());
        }

        public async Task RegisterKittenInClinic(int clinicId, int kittenId)
        {
            var clinic = await Context.Clinics.Include(c => c.Patients).SingleOrDefaultAsync(c => c.Id == clinicId);
            if (clinic is null) throw new ArgumentException("Clinic not exist");

            var kitten = await Context.Kittens.SingleOrDefaultAsync(c => c.Id == kittenId);
            if (kitten is null) throw new ArgumentException("Kitten not exist");

            if (clinic.Patients.Contains(kitten)) return;
            clinic.Patients.Add(kitten);

            await Context.SaveChangesAsync();
        }

        public async Task UnRegisterKittenFromClinic(int clinicId, int kittenId)
        {
            var clinic = await Context.Clinics.Include(c => c.Patients).SingleOrDefaultAsync(c => c.Id == clinicId);
            if (clinic is null) throw new ArgumentException("Clinic not exist");

            var kitten = await Context.Kittens.SingleOrDefaultAsync(c => c.Id == kittenId);
            if (kitten is null) throw new ArgumentException("Kitten not exist");

            if (!clinic.Patients.Contains(kitten)) return;
            clinic.Patients.Remove(kitten);

            await Context.SaveChangesAsync();
        }
    }
}