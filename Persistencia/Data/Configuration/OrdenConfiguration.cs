using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class OrdenConfiguration : IEntityTypeConfiguration<Orden> {
    public void Configure(EntityTypeBuilder<Orden> builder) {

        builder.ToTable("Orden");

        builder.Property(ord => ord.Fecha)
        .HasColumnType("datetime");

        

   
        builder.HasOne(ord => ord.Empleado)
        .WithMany(emp => emp.Ordenes)
        .HasForeignKey(ord => ord.IdEmpleadoFk);
        
        builder.HasOne(ord => ord.Cliente)
        .WithMany(clt => clt.Ordenes)
        .HasForeignKey(ord => ord.IdClienteFk);

        builder.HasOne(ord => ord.Estado)
        .WithMany(est => est.Ordenes)
        .HasForeignKey(ord => ord.IdEstadoFk);


    }
}

