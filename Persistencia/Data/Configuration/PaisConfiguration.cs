using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class PaisConfiguration : IEntityTypeConfiguration<Pais> {
    public void Configure(EntityTypeBuilder<Pais> builder) {

        builder.ToTable("Pais");

        builder.Property(pai => pai.Nombre)
        .HasColumnType("varchar(50)");

        


    }
}

