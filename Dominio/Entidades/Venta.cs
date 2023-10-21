namespace Dominio.Entidades;

public class Venta : BaseEntity
{
    public DateTime Fecha { get; set; }    

    
    
    
    public int IdEmpleadoFk {get; set;}
    public int IdClienteFk {get; set;}
    public int IdFormaPagoFk {get; set;}
    
    public Empleado Empleado {get; set;}
    public Cliente Cliente {get; set;}
    public FormaPago FormaPago {get; set;}


    public ICollection<DetalleVenta> DetallesVentas { get; set; }
    public ICollection<Orden> Ordenes { get; set; }


}
