using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class EstadoConfiguration : IEntityTypeConfiguration<Estado> {
    public void Configure(EntityTypeBuilder<Estado> builder) {

        builder.ToTable("Estado");

        builder.Property(est => est.Descripcion)          
        .HasColumnType("varchar(50)");       



        builder.HasOne(est => est.TiposEstados)
        .WithMany(te => te.Estados)
        .HasForeignKey(est => est.IdTipoEstadoFk);


    }
}
