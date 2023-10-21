namespace API.Dtos;

public class DetalleVentaDto
{
    public int Id { get; set; }
    public int Cantidad {get;set;}
    public decimal ValorUnit {get;set;}

    public int IdVentaFk { get; set; }
    public int IdProductoFk { get; set; } //Inventario
    public int IdTallaFk { get; set; }

    


    




}
