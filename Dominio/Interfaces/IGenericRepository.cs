using Dominio.Entidades;
using System.Linq.Expressions;

namespace Dominio.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    void Add(T entity);
    void Remove(T entity);
    void Update(T entity);
    Task<T> GetByIdAsync(int id);
    Task<T> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    void AddRange(IEnumerable<T> entities);
    void RemoveRange(IEnumerable<T> entities);
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(int pageIndex, int pageSize, string search);
}
