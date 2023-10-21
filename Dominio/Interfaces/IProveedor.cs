using Dominio.Entidades;

namespace Dominio.Interfaces;

public interface IProveedor : IGenericRepository<Proveedor>
{
    IEnumerable<Proveedor> GetProveedoresNaturales();
}
