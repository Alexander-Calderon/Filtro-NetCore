namespace Dominio.Entidades;

public class InsumoPrenda
{

    public int Cantidad {get; set;}



    public int IdInsumoFk {get; set;}
    public int IdPrendaFk {get; set;}
    

    public Insumo Insumo { get; set; }
    public Prenda Prenda { get; set; }


}
