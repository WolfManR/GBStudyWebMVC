using DataBase.Abstractions.Entities;
using DataBase.Abstractions.Entities.Analyzes;
using Microsoft.EntityFrameworkCore;

namespace DataBase.EF
{
    public class KittensContext : DbContext
    {
        public KittensContext(DbContextOptions<KittensContext> options) : base(options) { }

        public DbSet<Kitten> Kittens { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Analysis> Analysis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Clinic>()
                .Property(k => k.Name).IsRequired();

            modelBuilder.Entity<Clinic>()
                .HasMany(p => p.Patients)
                .WithMany(p => p.Clinics);

            modelBuilder.Entity<Clinic>()
                .HasMany(p => p.Analyzes)
                .WithOne(p => p.Clinic);

            modelBuilder.Entity<Patient>()
                .HasDiscriminator<string>("PatientType")
                .HasValue<Kitten>("kitten");

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Analysis)
                .WithOne(p => p.Patient);

            modelBuilder.Entity<Kitten>()
                .Property(k => k.Nickname)
                .IsRequired();

            modelBuilder.Entity<Analysis>()
                .HasDiscriminator<string>("AnalysisType")
                .HasValue<InspectionAnalysis>("inspection")
                .HasValue<BloodAnalysis>("blood");
        }
    }
}
