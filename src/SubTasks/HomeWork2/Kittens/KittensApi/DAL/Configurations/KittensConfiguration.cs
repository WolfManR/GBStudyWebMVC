using KittensApi.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KittensApi.DAL.Configurations
{
    public class KittensConfiguration : IEntityTypeConfiguration<Kitten>
    {
        public void Configure(EntityTypeBuilder<Kitten> builder)
        {
            builder.Property(k => k.Nickname).IsRequired();
        }
    }
}
