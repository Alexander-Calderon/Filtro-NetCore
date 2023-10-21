namespace Dominio.Entidades;

public class TipoEstado : BaseEntity
{    
    public string Descripcion { get; set; }
    
    
    public ICollection<Estado> Estados { get; set; }


}
