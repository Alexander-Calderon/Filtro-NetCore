namespace API.Dtos;

public class ClienteDto
{
    public int Id { get; set; }
    public string IdCliente { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaRegistro { get; set; }

    
    
    
    public int IdTipoPersonaFk {get; set;}
    public int IdMunicipioFk {get; set;}
    



}
