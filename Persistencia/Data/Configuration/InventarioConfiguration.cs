using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class InventarioConfiguration : IEntityTypeConfiguration<Inventario> {
    public void Configure(EntityTypeBuilder<Inventario> builder) {

        builder.ToTable("Inventario");

        builder.Property(inv => inv.CodInv)
        .HasColumnType("varchar(50)");
        builder.HasIndex(inv => inv.CodInv)
        .IsUnique();

        builder.Property(inv => inv.ValorVtaCop)
        .HasColumnType("decimal(22,2)");

        builder.Property(inv => inv.ValorVtaUsd)
        .HasColumnType("decimal(22,2)");





        builder.HasOne(inv => inv.Prenda)
        .WithMany(prd => prd.Inventarios)
        .HasForeignKey(inv => inv.IdPrendaFk);

    }
}

