using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class TallaConfiguration : IEntityTypeConfiguration<Talla> {
    public void Configure(EntityTypeBuilder<Talla> builder) {

        builder.ToTable("Talla");

        builder.Property(tall => tall.Descripcion)
        .HasColumnType("varchar(75)");




    }
}

