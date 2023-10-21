using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class ColorConfiguration : IEntityTypeConfiguration<Color> {
    public void Configure(EntityTypeBuilder<Color> builder) {

        builder.ToTable("Color");

        builder.Property(c => c.Descripcion)          
        .HasColumnType("varchar(75)");



    }
}
