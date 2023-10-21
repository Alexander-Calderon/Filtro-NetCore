using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado> {
    public void Configure(EntityTypeBuilder<Empleado> builder) {

        builder.ToTable("Empleado");

        builder.Property(e => e.Nombre)          
        .HasColumnType("varchar(50)");

        builder.Property(e => e.FechaIngreso)          
        .HasColumnType("datetime");


        builder.HasOne(emp => emp.Municipio)
        .WithMany(mun => mun.Empleados)
        .HasForeignKey(emp => emp.IdMunicipioFk);

        builder.HasOne(emp => emp.Cargo)
        .WithMany(crg => crg.Empleados)
       .HasForeignKey(emp => emp.IdCargoFk);



    }
}
