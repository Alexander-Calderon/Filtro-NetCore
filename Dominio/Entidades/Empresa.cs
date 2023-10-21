namespace Dominio.Entidades;

public class Empresa : BaseEntity
{
    public string Nit { get; set; }
    public string RazonSocial { get; set; }
    public string RepresentanteLegal { get; set; }
    public DateTime FechaCreacion { get; set; }

    
    
    
    public int IdMunicipioFk {get; set;}    
    
    public Municipio Municipio {get; set;}
    
}
