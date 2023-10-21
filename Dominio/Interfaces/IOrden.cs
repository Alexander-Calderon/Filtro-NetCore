using Dominio.Entidades;

namespace Dominio.Interfaces;

public interface IOrden : IGenericRepository<Orden>
{
    IEnumerable<Prenda> GetPrendasEnProduccion(int ordenProduccionId);
}
