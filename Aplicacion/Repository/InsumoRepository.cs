using Persistencia;
using Dominio.Entidades;
using Dominio.Interfaces;

namespace Aplicacion.Repository;

public class InsumoRepository : GenericRepository<Insumo>, IInsumo
{
    private readonly DbAppContext _context;
    public InsumoRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    
    // Implementaciones detalladas (Consultas específicas):

    public IEnumerable<Insumo> GetInsumosByProveedor(int proveedorId)
    {
        return _context.InsumosProveedores
            .Where(ip => ip.IdProveedorFk == proveedorId)
            .Select(ip => ip.Insumo);
    }






    // Paginación
    // public override async Task<(int totalRegistros, IEnumerable<Insumo> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    // {
    //     var totalRegistros = await _context.Set<Insumo>().CountAsync();
    //     var registros = await _context.Set<Insumo>()
    //         .Skip((pageIndex - 1) * pageSize)
    //         .Take(pageSize)
    //         .Include(c => c.Mascota).ThenInclude(m =>m.Propietario)
    //         .Include(c => c.Veterinario)
    //         .ToListAsync();
    //     return (totalRegistros, registros);
    // }



}
