using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class DetalleOrdenCliente : IEntityTypeConfiguration<DetalleOrden> {
    public void Configure(EntityTypeBuilder<DetalleOrden> builder) {

        builder.ToTable("DetalleOrden");

        builder.Property(d => d.CantidadProducir)          
        .HasColumnType("int");

        builder.Property(d => d.CantidadProducida)          
        .HasColumnType("int");


        builder.HasOne(dor => dor.Orden)
        .WithMany(o => o.DetallesOrdenes)
        .HasForeignKey(dor => dor.IdOrden);

        builder.HasOne(dor => dor.Prenda)
        .WithMany(p => p.DetallesOrdenes)
        .HasForeignKey(dor => dor.IdPrenda);

        builder.HasOne(dor => dor.Color)
        .WithMany(c => c.DetallesOrdenes)
        .HasForeignKey(dor => dor.IdColorFk);

        builder.HasOne(dor => dor.Estado)
        .WithMany(e => e.DetallesOrdenes)
        .HasForeignKey(dor => dor.IdEstadoFk);



    }
}
