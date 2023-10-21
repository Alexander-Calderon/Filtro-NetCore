using Dominio.Entidades;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class RolRepository : GenericRepository<Rol>, IRol
{
    public readonly DbAppContext _context;
    public RolRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

}
