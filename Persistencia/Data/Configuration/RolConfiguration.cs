using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class RolConfiguration : IEntityTypeConfiguration<Rol> {
    public void Configure(EntityTypeBuilder<Rol> builder) {

        builder.ToTable("ROLES");
        
        builder.Property(r => r.Descripcion)
          .HasColumnName("descripcion")
          .HasColumnType("varchar(75)");
    }
}
