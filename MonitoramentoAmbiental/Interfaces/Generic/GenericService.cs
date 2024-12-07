using MonitoramentoAmbiental.Models;

namespace MonitoramentoAmbiental.Services;

public interface IGenericService<T> where T : class
{
    Task<T> Criar(T entity);
    Task<PagedResult<T>> ListarTodos(int page, int pageSize);
    Task<T?> BuscarPorId(int id);
    Task<T?> Atualizar(int id, T entity);
    Task<bool> Deletar(int id);
}
