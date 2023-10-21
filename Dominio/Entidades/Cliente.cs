namespace Dominio.Entidades;

public class Cliente : BaseEntity
{
    public string IdCliente { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaRegistro { get; set; }

    
    
    
    public int IdTipoPersonaFk {get; set;}
    public int IdMunicipioFk {get; set;}
    
    public TipoPersona TipoPersona {get; set;}
    public Municipio Municipio {get; set;}


    public ICollection<Venta> Ventas { get; set; }
    public ICollection<Orden> Ordenes { get; set; }


}
