namespace Dominio.Entidades;

public class Color : BaseEntity
{    

    public string Descripcion { get; set; }
    

    public ICollection<DetalleOrden> DetallesOrdenes {get; set;}
    



}
