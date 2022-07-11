using CiaAerea.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CiaAerea.EntityConfigurations;

public class CancelamentoConfiguration : IEntityTypeConfiguration<Cancelamento>
{
    public void Configure(EntityTypeBuilder<Cancelamento> builder)
    {
        builder.ToTable("Cancelamentos");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Motivo)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.DataHoraNotificacao)
               .IsRequired();
    }
}