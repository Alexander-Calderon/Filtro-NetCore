namespace Dominio.Entidades;

public class FormaPago : BaseEntity
{
    public string Descripcion { get; set; }    

    

    public ICollection<Venta> Ventas { get; set; }



}
