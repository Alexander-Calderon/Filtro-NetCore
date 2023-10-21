namespace API.Dtos;

public class ProveedorDto
{
    public int Id { get; set; }
    public string NitProveedor { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaRegistro { get; set; }

    
    
    
    public int IdMunicipioFk {get; set;}
    public int IdTipoPersonaFk {get; set;}
        
    
    


}
