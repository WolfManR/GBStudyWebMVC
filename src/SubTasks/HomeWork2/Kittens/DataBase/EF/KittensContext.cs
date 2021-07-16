using System.Reflection;
using DataBase.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataBase.EF
{
    public class KittensContext : DbContext
    {
        public KittensContext(DbContextOptions<KittensContext> options) : base(options) { }

        public DbSet<Kitten> Kittens { get; set; }
        public DbSet<Clinic> Clinics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationForCurrentContext(Assembly.GetExecutingAssembly(), this);
            base.OnModelCreating(modelBuilder);
        }
    }
}
