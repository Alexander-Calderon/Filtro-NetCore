using Persistencia;
using Dominio.Entidades;
using Dominio.Interfaces;

namespace Aplicacion.Repository;

public class PrendaRepository : GenericRepository<Prenda>, IPrenda
{
    private readonly DbAppContext _context;
    public PrendaRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    
    // Implementaciones detalladas (Consultas específicas):

    // C3
    public IEnumerable<IGrouping<string, Prenda>> GetPrendasByTipoProteccion()
    {
        return _context.Prendas
        .GroupBy(p => p.TipoProteccion.Descripcion);
    }






    // Paginación
    // public override async Task<(int totalRegistros, IEnumerable<Prenda> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    // {
    //     var totalRegistros = await _context.Set<Prenda>().CountAsync();
    //     var registros = await _context.Set<Prenda>()
    //         .Skip((pageIndex - 1) * pageSize)
    //         .Take(pageSize)
    //         .Include(c => c.Mascota).ThenInclude(m =>m.Propietario)
    //         .Include(c => c.Veterinario)
    //         .ToListAsync();
    //     return (totalRegistros, registros);
    // }



}
