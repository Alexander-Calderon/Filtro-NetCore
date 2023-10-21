using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor> {
    public void Configure(EntityTypeBuilder<Proveedor> builder) {

        builder.ToTable("Proveedor");

        builder.Property(prov => prov.NitProveedor)
        .HasColumnType("varchar(50)");
        builder.HasIndex(prov => prov.NitProveedor)
        .IsUnique();

        builder.Property(prov => prov.Nombre)
        .HasColumnType("varchar(50)");

        builder.Property(prov => prov.FechaRegistro)
        .HasColumnType("datetime");


        builder.HasOne(prov => prov.Municipio)
        .WithMany(mun => mun.Proveedores)
        .HasForeignKey(prov => prov.IdMunicipioFk);

        builder.HasOne(prov => prov.TipoPersona)
        .WithMany(tp => tp.Proveedores)
        .HasForeignKey(prov => prov.IdTipoPersonaFk);


        


    }
}

