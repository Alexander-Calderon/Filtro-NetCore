namespace Dominio.Interfaces;

public interface IUnitOfWork
{
    IUsuario Usuarios {get;}
    IRol Roles {get;}

    ICargo Cargos {get;}
    ICliente Clientes {get;}
    IColor Colores {get;}
    IDepartamento Departamentos {get;}
    IDetalleOrden DetallesOrdenes {get;}
    IDetalleVenta DetallesVentas {get;}
    IEmpleado Empleados {get;}
    IEmpresa Empresas {get;}
    IEstado Estados {get;}
    IFormaPago FormasPagos {get;}
    IGenero Generos {get;}
    IInsumo Insumos {get;}    
    IInventario Inventarios {get;}
    IInventarioTalla InventariosTallas {get;}
    IMunicipio Municipios {get;}
    IOrden Ordenes {get;}
    IPais Paises {get;}
    IPrenda Prendas {get;}
    IProveedor Proveedores {get;}
    ITalla Tallas {get;}
    ITipoEstado TiposEstados {get;}
    ITipoPersona TiposPersonas {get;}
    ITipoProteccion TiposProtecciones {get;}
    IVenta Ventas {get;}


    Task<int> SaveAsync();
}
