using KittensApi.Models;

using Microsoft.EntityFrameworkCore;

using System.Reflection;

namespace KittensApi.DAL
{
    public class KittensContext : DbContext
    {
        public KittensContext(DbContextOptions<KittensContext> options) : base(options) { }

        public DbSet<Kitten> Kittens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationForCurrentContext(Assembly.GetExecutingAssembly(), this);
            base.OnModelCreating(modelBuilder);
        }
    }
}
