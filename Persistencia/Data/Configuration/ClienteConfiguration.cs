using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente> {
    public void Configure(EntityTypeBuilder<Cliente> builder) {

        builder.ToTable("CLIENTE");

        builder.Property(c => c.IdCliente)          
        .HasColumnType("varchar(30)");
        
        builder.HasIndex(c => c.IdCliente)
        .IsUnique();
        
        builder.Property(c => c.Nombre)          
          .HasColumnType("varchar(50)");

        builder.Property(c => c.FechaRegistro)
          .HasColumnName("SueldoBase")
          .HasColumnType("datetime");



        builder.HasOne(c => c.TipoPersona)
        .WithMany(t => t.Clientes)
        .HasForeignKey(c => c.IdTipoPersonaFk);

        builder.HasOne(c => c.Municipio)
        .WithMany(m => m.Clientes)
        .HasForeignKey(c => c.IdMunicipioFk);


    }
}
