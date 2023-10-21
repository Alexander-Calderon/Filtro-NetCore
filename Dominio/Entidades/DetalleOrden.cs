namespace Dominio.Entidades;

public class DetalleOrden : BaseEntity
{    

    public int CantidadProducir { get; set; }
    public int CantidadProducida { get; set; }


    public int IdOrden { get; set; }
    public int IdPrenda { get; set; }
    public int IdColorFk {get; set;}
    public int IdEstadoFk {get; set;}
    

    

    public Orden Orden {get; set;}
    public Prenda Prenda {get; set;}
    public Color Color {get; set;}
    public Estado Estado {get; set;}
    



}
