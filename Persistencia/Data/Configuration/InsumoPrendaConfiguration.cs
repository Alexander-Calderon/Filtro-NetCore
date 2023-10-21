using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class InsumoPrendaConfiguration : IEntityTypeConfiguration<InsumoPrenda> {
    public void Configure(EntityTypeBuilder<InsumoPrenda> builder) {

        builder.ToTable("InsumoPrenda");

        builder.Property(insPr => insPr.Cantidad)
        .HasColumnType("varchar(50)");

        builder.HasKey(insp => new { insp.IdInsumoFk, insp.IdPrendaFk });

        builder.HasOne(insPr => insPr.Insumo)
        .WithMany(ins => ins.InsumosPrendas)
        .HasForeignKey(insPr => insPr.IdInsumoFk);

        builder.HasOne(insPr => insPr.Prenda)
        .WithMany(prnd => prnd.InsumosPrendas)
        .HasForeignKey(insPr => insPr.IdPrendaFk);

    }
}

