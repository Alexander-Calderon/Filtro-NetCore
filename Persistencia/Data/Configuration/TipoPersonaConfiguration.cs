using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class TipoPersonaConfiguration : IEntityTypeConfiguration<TipoPersona> {
    public void Configure(EntityTypeBuilder<TipoPersona> builder) {

        builder.ToTable("TipoPersona");

        builder.Property(tp => tp.Nombre)
        .HasColumnType("varchar(75)");




    }
}

