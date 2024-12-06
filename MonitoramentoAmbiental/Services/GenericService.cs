using MonitoramentoAmbiental.Repositories;

namespace MonitoramentoAmbiental.Services;

public class GenericService<T> : IGenericService<T> where T : class
{
    private readonly IGenericRepository<T> _repository;

    public GenericService(IGenericRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<T> Criar(T entity)
    {
        return await _repository.Create(entity);
    }

    public async Task<List<T>> ListarTodos()
    {
        return await _repository.GetAll();
    }

    public async Task<T?> BuscarPorId(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<T?> Atualizar(int id, T entity)
    {
        return await _repository.Update(id, entity);
    }

    public async Task<bool> Deletar(int id)
    {
        return await _repository.Delete(id);
    }
}
