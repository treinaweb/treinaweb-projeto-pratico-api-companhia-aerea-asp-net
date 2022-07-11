using CiaAerea.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CiaAerea.EntityConfigurations;

public class AeronaveConfiguration : IEntityTypeConfiguration<Aeronave>
{
    public void Configure(EntityTypeBuilder<Aeronave> builder)
    {
        builder.ToTable("Aeronaves");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Fabricante)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(a => a.Modelo)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(a => a.Codigo)
               .IsRequired()
               .HasMaxLength(10);

        builder.HasMany(a => a.Manutencoes)
               .WithOne(m => m.Aeronave)
               .HasForeignKey(m => m.AeronaveId);
    }
}