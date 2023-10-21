namespace Dominio.Entidades;

public class DetalleVenta : BaseEntity
{
    
    public int Cantidad {get;set;}
    public decimal ValorUnit {get;set;}

    public int IdVentaFk { get; set; }
    public int IdProductoFk { get; set; } //Inventario
    public int IdTallaFk { get; set; }


    public Venta Venta { get; set; }
    public Inventario Producto { get; set; } //Inventario
    public Talla Talla { get; set; }
    


    




}
