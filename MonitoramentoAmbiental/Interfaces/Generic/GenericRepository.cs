namespace MonitoramentoAmbiental.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T> Create(T entity);
    Task<List<T>> GetAll();
    Task<T?> GetById(int id);
    Task<T?> Update(int id, T entity);
    Task<bool> Delete(int id);
}
