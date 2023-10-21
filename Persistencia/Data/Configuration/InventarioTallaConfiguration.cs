using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class InventarioTallaConfiguration : IEntityTypeConfiguration<InventarioTalla> {
    public void Configure(EntityTypeBuilder<InventarioTalla> builder) {

        builder.ToTable("InventarioTalla");

        builder.HasKey(invT => new { invT.IdInvFk, invT.IdTallaFk });

        builder.Property(invT => invT.Cantidad)
        .HasColumnType("int");

   
        builder.HasOne(invT => invT.Inventario)
        .WithMany(inv => inv.InventariosTallas)
        .HasForeignKey(invT => invT.IdInvFk);

        builder.HasOne(invT => invT.Talla)
        .WithMany(tall => tall.InventariosTallas)
        .HasForeignKey(invT => invT.IdTallaFk);

    }
}

