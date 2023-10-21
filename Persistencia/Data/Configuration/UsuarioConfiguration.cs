using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario> {
  public void Configure(EntityTypeBuilder<Usuario> builder) {
    builder.ToTable("USUARIOS");
    
    builder.Property(u => u.Username)
      .HasColumnName("username")
      .HasColumnType("varchar(50)");

    builder.Property(u => u.Email)
      .HasColumnName("email")
      .HasColumnType("varchar(50)");

    builder.Property(u => u.Password)
      .HasColumnName("password")
      .HasColumnType("varchar(255)");

      

    builder.HasMany(usu => usu.Roles)
    .WithMany(rl => rl.Usuarios)
    .UsingEntity<UsuarioRol>(

      j => j
          .HasOne(ur => ur.Rol)
          .WithMany(r => r.UsuariosRoles)
          .HasForeignKey(ru => ru.RolId),
          
      j => j
          .HasOne(ur => ur.Usuario)
          .WithMany(u => u.UsuariosRoles)
          .HasForeignKey(ur => ur.UsuarioId),


      j =>
      {
          j.ToTable("ROLES_USUARIOS");
          j.HasKey(ur => new { ur.RolId, ur.UsuarioId });

      }




    );


  }



}
