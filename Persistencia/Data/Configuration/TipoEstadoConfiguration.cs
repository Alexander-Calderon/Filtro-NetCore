using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class TipoEstadoConfiguration : IEntityTypeConfiguration<TipoEstado> {
    public void Configure(EntityTypeBuilder<TipoEstado> builder) {

        builder.ToTable("TipoEstado");

        builder.Property(te => te.Descripcion)
        .HasColumnType("varchar(75)");




    }
}

