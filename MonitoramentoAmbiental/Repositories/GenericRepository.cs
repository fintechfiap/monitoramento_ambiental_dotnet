using Microsoft.EntityFrameworkCore;

namespace MonitoramentoAmbiental.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T> Create(T entity)
    {
        _context.Entry(entity).Property("CriadoEm").CurrentValue = DateTime.Now;
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<T>> GetAll()
    {
        return await _dbSet
            .Where(entity => EF.Property<DateTime?>(entity, "DeletadoEm") == null)
            .OrderByDescending(entity => EF.Property<DateTime>(entity, "CriadoEm"))
            .ToListAsync();
    }

    public async Task<T?> GetById(int id)
    {
        return await _dbSet
            .Where(entity => EF.Property<DateTime?>(entity, "DeletadoEm") == null)
            .FirstOrDefaultAsync(entity => EF.Property<int>(entity, "Id") == id);
    }

    public async Task<T?> Update(int id, T entity)
    {
        var existingEntity = await GetById(id);
        if (existingEntity == null) return null;

        foreach (var property in _context.Entry(entity).Properties)
        {
            if (property.Metadata.IsPrimaryKey() || property.Metadata.Name == "CriadoEm")
                continue;

            _context.Entry(existingEntity).Property(property.Metadata.Name).CurrentValue = property.CurrentValue;
        }

        var propertyAlteradoEm = _context.Entry(existingEntity).Property("AlteradoEm");
        if (propertyAlteradoEm != null)
        {
            propertyAlteradoEm.CurrentValue = DateTime.Now;
        }

        await _context.SaveChangesAsync();
        return existingEntity;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await GetById(id);
        if (entity == null) return false;

        _context.Entry(entity).Property("DeletadoEm").CurrentValue = DateTime.Now;
        await _context.SaveChangesAsync();
        return true;
    }
}
