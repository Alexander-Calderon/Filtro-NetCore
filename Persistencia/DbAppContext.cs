using System.Reflection;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Persistencia;

public class DbAppContext : DbContext
{
    public DbAppContext(DbContextOptions<DbAppContext> options) : base(options)
    {
    }
    //Aqui se establecen los DbSet<Entity> Entities { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<UsuarioRol> UsuarioRoles { get; set; }



    public DbSet<Cargo> Cargo { get; set; }
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<Color> Color { get; set; }
    public DbSet<Departamento> Departamento { get; set; }
    public DbSet<DetalleOrden> DetalleOrden { get; set; }
    public DbSet<DetalleVenta> DetalleVenta { get; set; }
    public DbSet<Empleado> Empleado { get; set; }
    public DbSet<Empresa> Empresa { get; set; }
    public DbSet<Estado> Estado { get; set; }
    public DbSet<FormaPago> FormaPago { get; set; }
    public DbSet<Genero> Genero { get; set; }
    public DbSet<Insumo> Insumo { get; set; }
    public DbSet<InsumoPrenda> InsumoPrenda { get; set; }
    public DbSet<InsumoProveedor> InsumoProveedor { get; set; }
    public DbSet<Inventario> Inventario { get; set; }
    public DbSet<InventarioTalla> InventarioTalla { get; set; }
    public DbSet<Municipio> Municipio { get; set; }
    public DbSet<Orden> Orden { get; set; }
    public DbSet<Pais> Pais { get; set; }
    public DbSet<Prenda> Prenda { get; set; }
    public DbSet<Proveedor> Proveedor { get; set; }
    public DbSet<Talla> Talla { get; set; }
    public DbSet<TipoEstado> TipoEstado { get; set; }
    public DbSet<TipoPersona> TipoPersona { get; set; }
    public DbSet<TipoProteccion> TipoProteccion { get; set; }
    public DbSet<Venta> Venta { get; set; }




    
    protected override void OnModelCreating( ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
