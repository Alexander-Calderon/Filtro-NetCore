namespace Dominio.Entidades;

public class TipoPersona : BaseEntity
{
    public string Nombre { get; set; }
    public ICollection<Cliente> Clientes { get; set; }
    public ICollection<Proveedor> Proveedores { get; set; }


}
