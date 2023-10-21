namespace API.Dtos;

public class OrdenDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }    

    
    
    
    public int IdEmpleadoFk {get; set;}
    public int IdClienteFk {get; set;}
    public int IdEstadoFk {get; set;}
    


}
