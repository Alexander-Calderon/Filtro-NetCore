namespace Dominio.Entidades;

public class Insumo : BaseEntity
{
    public string Nombre { get; set; }
    public decimal ValorUnit { get; set; }
    public int StockMin { get; set; }
    public int StockMax { get; set; }

    
    public ICollection<InsumoProveedor> InsumosProveedores { get; set; }
    public ICollection<InsumoPrenda> InsumosPrendas { get; set; }



}
