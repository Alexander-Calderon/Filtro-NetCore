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



    public DbSet<Cargo> Cargos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Color> Colores { get; set; }
    public DbSet<Departamento> Departamentos { get; set; }
    public DbSet<DetalleOrden> DetallesOrdenes { get; set; }
    public DbSet<DetalleVenta> DetallesVentas { get; set; }
    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Estado> Estados { get; set; }
    public DbSet<FormaPago> FormasPagos { get; set; }
    public DbSet<Genero> Generos { get; set; }
    public DbSet<Insumo> Insumos { get; set; }
    public DbSet<InsumoPrenda> InsumosPrendas { get; set; }
    public DbSet<InsumoProveedor> InsumosProveedores { get; set; }
    public DbSet<Inventario> Inventarios { get; set; }
    public DbSet<InventarioTalla> InventariosTallas { get; set; }
    public DbSet<Municipio> Municipios { get; set; }
    public DbSet<Orden> Ordenes { get; set; }
    public DbSet<Pais> Paises { get; set; }
    public DbSet<Prenda> Prendas { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<Talla> Tallas { get; set; }
    public DbSet<TipoEstado> TiposEstados { get; set; }
    public DbSet<TipoPersona> TiposPersonas { get; set; }
    public DbSet<TipoProteccion> TiposProtecciones { get; set; }
    public DbSet<Venta> Venta { get; set; }




    
    protected override void OnModelCreating( ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
