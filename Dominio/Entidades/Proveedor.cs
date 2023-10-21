namespace Dominio.Entidades;

public class Proveedor : BaseEntity
{
    public string NitProveedor { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaRegistro { get; set; }

    
    
    
    public int IdMunicipioFk {get; set;}
    public int IdTipoPersonaFk {get; set;}
    
    public Municipio Municipio {get; set;}
    public TipoPersona TipoPersona {get; set;}


    public ICollection<InsumoProveedor> InsumosProveedores { get; set; }
    public ICollection<Orden> Ordenes { get; set; }
    


}
