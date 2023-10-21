using Dominio.Entidades;

namespace Dominio.Interfaces;

public interface IInsumo : IGenericRepository<Insumo>
{
    IEnumerable<Insumo> GetInsumosByProveedor(int proveedorId);

}
