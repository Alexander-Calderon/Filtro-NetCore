using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class TipoProteccionConfiguration : IEntityTypeConfiguration<TipoProteccion> {
    public void Configure(EntityTypeBuilder<TipoProteccion> builder) {

        builder.ToTable("TipoProteccion");

        builder.Property(tp => tp.Descripcion)
        .HasColumnType("varchar(75)");




    }
}

