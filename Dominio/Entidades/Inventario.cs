namespace Dominio.Entidades;

public class Inventario : BaseEntity
{
    public int CodInv { get; set; }
    public decimal ValorVtaCop { get; set; } //ValorVenta
    public decimal ValorVtaUsd { get; set; }
    

    public int IdPrendaFk { get; set; }
    

    public Prenda Prenda {get; set;}
    

    
    public ICollection<InventarioTalla> InventariosTallas { get; set; }
    public ICollection<DetalleVenta> DetallesVentas { get; set; }



}
