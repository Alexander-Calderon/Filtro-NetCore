namespace Dominio.Entidades;

public class InventarioTalla : BaseEntity
{
    
    public int Cantidad { get; set; }

    public int IdInvFk { get; set; }
    public int IdTallaFk { get; set; }

    

    public Inventario Inventario {get; set;}
    public Talla Talla {get; set;}
    




}
