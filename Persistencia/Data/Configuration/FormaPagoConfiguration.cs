using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class FormaPagoConfiguration : IEntityTypeConfiguration<FormaPago> {
    public void Configure(EntityTypeBuilder<FormaPago> builder) {

        builder.ToTable("FormaPago");

        builder.Property(fg => fg.Descripcion)          
        .HasColumnType("varchar(50)");       



    }
}

