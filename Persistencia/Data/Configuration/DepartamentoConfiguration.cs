using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class DepartamentoCliente : IEntityTypeConfiguration<Departamento> {
    public void Configure(EntityTypeBuilder<Departamento> builder) {

        builder.ToTable("Departamento");

        builder.Property(d => d.Nombre)          
        .HasColumnType("varchar(50)");


        builder.HasOne(d => d.Pais)
        .WithMany(p => p.Departamentos)
        .HasForeignKey(d => d.IdPaisFk);



    }
}
