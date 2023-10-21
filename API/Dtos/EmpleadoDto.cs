namespace API.Dtos;

public class EmpleadoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaIngreso { get; set; }


    
    public int IdMunicipioFk {get; set;}
    public int IdCargoFk {get; set;}
    


}
