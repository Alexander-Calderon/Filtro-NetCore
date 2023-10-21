namespace Dominio.Entidades;

public class RefreshToken : BaseEntity
{
    public int Usuario_Fk { get; set; }
    public Usuario Usuario { get; set; }
    public string Token { get; set; }
    public DateTime Expiracion { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expiracion;
    public DateTime Creacion { get; set; }
    public DateTime? Revoked { get; set; }
    public bool IsActive => Revoked == null && !IsExpired;
}