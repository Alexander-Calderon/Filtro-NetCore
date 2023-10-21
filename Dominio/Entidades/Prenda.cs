namespace Dominio.Entidades;

public class Prenda : BaseEntity
{
    public int IdPrenda { get; set; }
    public string Nombre { get; set; }
    public decimal ValorUnitCop { get; set; }
    public decimal ValorUnitUsd { get; set; }
    

    public int IdEstadoFk {get; set;}
    public int IdTipoProteccionFk {get; set;}
    public int IdGeneroFk {get; set;}

    public Estado Estado {get; set;}
    public TipoProteccion TipoProteccion {get; set;}
    public Genero Genero {get; set;}

    
    public ICollection<InsumoPrenda> InsumosPrendas { get; set; }
    public ICollection<Inventario> Inventarios { get; set; }
    public ICollection<DetalleOrden> DetallesOrdenes { get; set; }



}
