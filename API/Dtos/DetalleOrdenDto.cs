namespace API.Dtos;

public class DetalleOrdenDto
{    
    public int Id { get; set; }
    public int CantidadProducir { get; set; }
    public int CantidadProducida { get; set; }


    public int IdOrden { get; set; }
    public int IdPrenda { get; set; }
    public int IdColorFk {get; set;}
    public int IdEstadoFk {get; set;}
    

    

    



}
