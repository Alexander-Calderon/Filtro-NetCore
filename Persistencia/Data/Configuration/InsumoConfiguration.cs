using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class InsumoConfiguration : IEntityTypeConfiguration<Insumo> {
    public void Configure(EntityTypeBuilder<Insumo> builder) {

        builder.ToTable("Insumo");

        builder.Property(ins => ins.Nombre)
        .HasColumnType("varchar(50)");       

        builder.Property(ins => ins.ValorUnit)
        .HasColumnType("decimal(22,2)");

        builder.Property(ins => ins.StockMin)
        .HasColumnType("int");

        builder.Property(ins => ins.StockMax)
        .HasColumnType("int");



    }
}

