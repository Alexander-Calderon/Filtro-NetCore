using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class UsuarioRepository : GenericRepository<Usuario>, IUsuario
{
    private readonly DbAppContext _context;

    public UsuarioRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

     public async Task<Usuario> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Usuarios
            .Include(u => u.Roles)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
    }

    public async Task<Usuario> GetByUsernameAsync(string username)
    {
        return await _context.Usuarios
                            .Include(u=>u.Roles)
                            .FirstOrDefaultAsync(u=>u.Username.ToLower()==username.ToLower());
    }
}
