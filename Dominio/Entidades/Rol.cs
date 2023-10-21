namespace Dominio.Entidades;

public class Rol : BaseEntity
{
    public string Descripcion { get; set; }
    public ICollection<Usuario> Usuarios { get; set; } = new HashSet<Usuario>();
    public ICollection<UsuarioRol> UsuariosRoles { get; set; }
}
