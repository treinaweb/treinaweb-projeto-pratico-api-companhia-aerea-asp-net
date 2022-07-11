using CiaAerea.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CiaAerea.EntityConfigurations;

public class VooConfiguration : IEntityTypeConfiguration<Voo>
{
    public void Configure(EntityTypeBuilder<Voo> builder)
    {
        builder.ToTable("Voos");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Origem)
               .IsRequired()
               .HasMaxLength(3);

        builder.Property(v => v.Destino)
               .IsRequired()
               .HasMaxLength(3);

        builder.Property(v => v.DataHoraPartida)
               .IsRequired();
        
        builder.Property(v => v.DataHoraChegada)
               .IsRequired();

        builder.HasOne(v => v.Piloto)
               .WithMany(p => p.Voos)
               .HasForeignKey(v => v.PilotoId);

        builder.HasOne(v => v.Aeronave)
               .WithMany(a => a.Voos)
               .HasForeignKey(v => v.AeronaveId);

        builder.HasOne(v => v.Cancelamento)
               .WithOne(c => c.Voo)
               .HasForeignKey<Cancelamento>(c => c.VooId);
    }
}