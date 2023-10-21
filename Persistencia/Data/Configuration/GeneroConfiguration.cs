using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class GeneroConfiguration : IEntityTypeConfiguration<Genero> {
    public void Configure(EntityTypeBuilder<Genero> builder) {

        builder.ToTable("Genero");

        builder.Property(gen => gen.Descripcion)
        .HasColumnType("varchar(50)");       



    }
}

