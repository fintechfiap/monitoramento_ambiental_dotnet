using MonitoramentoAmbiental.Models;

namespace MonitoramentoAmbiental.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T> Create(T entity);
    Task<PagedResult<T>> GetAll(int page, int pageSize);
    Task<T?> GetById(int id);
    Task<T?> Update(int id, T entity);
    Task<bool> Delete(int id);
}
