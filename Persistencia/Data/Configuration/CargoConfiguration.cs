using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class CargosConfiguration : IEntityTypeConfiguration<Cargo> {
    public void Configure(EntityTypeBuilder<Cargo> builder) {

        builder.ToTable("CARGOS");
        
        builder.Property(c => c.Descripcion)
          .HasColumnName("descripcion")
          .HasColumnType("varchar(75)");

        builder.Property(c => c.SueldoBase)
          .HasColumnName("SueldoBase")
          .HasColumnType("decimal(22,2)");


    }
}
