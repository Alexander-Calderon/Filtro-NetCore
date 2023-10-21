using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class VentaConfiguration : IEntityTypeConfiguration<Venta> {
    public void Configure(EntityTypeBuilder<Venta> builder) {

        builder.ToTable("Venta");

        builder.Property(vnt => vnt.Fecha)
        .HasColumnType("datetime");



        builder.HasOne(vent => vent.Empleado)
        .WithMany(emp => emp.Ventas)
        .HasForeignKey(vent => vent.IdEmpleadoFk);

        builder.HasOne(vent => vent.Cliente)
        .WithMany(cln => cln.Ventas)
        .HasForeignKey(vent => vent.IdClienteFk);

        builder.HasOne(vent => vent.FormaPago)
        .WithMany(fp => fp.Ventas)
        .HasForeignKey(vent => vent.IdEmpleadoFk);



    }
}

