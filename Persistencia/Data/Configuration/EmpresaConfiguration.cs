using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa> {
    public void Configure(EntityTypeBuilder<Empresa> builder) {

        builder.ToTable("Empresa");

        builder.Property(emp => emp.Nit)          
        .HasColumnType("varchar(50)");

        builder.Property(emp => emp.RazonSocial)          
        .HasColumnType("varchar(50)");

        builder.Property(emp => emp.RepresentanteLegal)          
        .HasColumnType("varchar(50)");

        builder.Property(emp => emp.FechaCreacion)          
        .HasColumnType("datetime");
        



        builder.HasOne(emp => emp.Municipio)
        .WithMany(mun => mun.Empresas)
        .HasForeignKey(emp => emp.IdMunicipioFk);


    }
}
