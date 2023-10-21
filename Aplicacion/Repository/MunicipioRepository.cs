using Persistencia;
using Dominio.Entidades;
using Dominio.Interfaces;

namespace Aplicacion.Repository;

public class MunicipioRepository : GenericRepository<Municipio>, IMunicipio
{
    private readonly DbAppContext _context;
    public MunicipioRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    
    // Implementaciones detalladas (Consultas específicas):






    // Paginación
    // public override async Task<(int totalRegistros, IEnumerable<Municipio> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    // {
    //     var totalRegistros = await _context.Set<Municipio>().CountAsync();
    //     var registros = await _context.Set<Municipio>()
    //         .Skip((pageIndex - 1) * pageSize)
    //         .Take(pageSize)
    //         .Include(c => c.Mascota).ThenInclude(m =>m.Propietario)
    //         .Include(c => c.Veterinario)
    //         .ToListAsync();
    //     return (totalRegistros, registros);
    // }



}
