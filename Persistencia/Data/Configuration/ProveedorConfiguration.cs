using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class PrendaConfiguration : IEntityTypeConfiguration<Prenda> {
    public void Configure(EntityTypeBuilder<Prenda> builder) {

        builder.ToTable("Prenda");

        builder.Property(pai => pai.IdPrenda)
        .HasColumnType("varchar(50)");
        builder.HasIndex(pai => pai.IdPrenda)
        .IsUnique();

        builder.Property(pai => pai.Nombre)
        .HasColumnType("varchar(50)");

        builder.Property(pai => pai.ValorUnitCop)
        .HasColumnType("decimal(22,2)");

        builder.Property(pai => pai.ValorUnitUsd)
        .HasColumnType("decimal(22,2)");


        builder.HasOne(prnd => prnd.Estado)
        .WithMany(est => est.Prendas)
        .HasForeignKey(prnd => prnd.IdEstadoFk);

        builder.HasOne(prnd => prnd.TipoProteccion)
        .WithMany(tp => tp.Prendas)
        .HasForeignKey(prnd => prnd.IdTipoProteccionFk);

        builder.HasOne(prnd => prnd.Genero)
        .WithMany(gen => gen.Prendas)
        .HasForeignKey(prnd => prnd.IdGeneroFk);

        


    }
}

