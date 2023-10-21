using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class InsumoProveedorConfiguration : IEntityTypeConfiguration<InsumoProveedor> {
    public void Configure(EntityTypeBuilder<InsumoProveedor> builder) {

        builder.ToTable("InsumoProveedor");        

        builder.HasKey(insp => new { insp.IdInsumoFk, insp.IdProveedorFk });



        builder.HasOne(insPr => insPr.Insumo)
        .WithMany(ins => ins.InsumosProveedores)
        .HasForeignKey(insPr => insPr.IdInsumoFk);

        builder.HasOne(insPr => insPr.Proveedor)
        .WithMany(prov => prov.InsumosProveedores)
        .HasForeignKey(insPr => insPr.IdProveedorFk);

    }
}

