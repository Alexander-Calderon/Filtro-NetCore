using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class MunicipioConfiguration : IEntityTypeConfiguration<Municipio> {
    public void Configure(EntityTypeBuilder<Municipio> builder) {

        builder.ToTable("Municipio");

        builder.Property(mun => mun.Nombre)
        .HasColumnType("varchar(50)");

        

   
        builder.HasOne(mun => mun.Departamento)
        .WithMany(dep => dep.Municipios)
        .HasForeignKey(mun => mun.IdDepartamentoFk);


    }
}

