using Dominio.Entidades;

namespace Dominio.Interfaces;

public interface IPrenda : IGenericRepository<Prenda>
{
    IEnumerable<IGrouping<string, Prenda>> GetPrendasByTipoProteccion();
    
}
