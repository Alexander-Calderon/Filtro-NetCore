namespace API.Dtos;

public class InventarioDto
{
    public int Id { get; set; }
    public int CodInv { get; set; }
    public decimal ValorVtaCop { get; set; } //ValorVenta
    public decimal ValorVtaUsd { get; set; }
    

    public int IdPrendaFk { get; set; }
    

    
}
