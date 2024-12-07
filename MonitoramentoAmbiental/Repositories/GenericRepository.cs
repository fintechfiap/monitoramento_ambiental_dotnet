using Microsoft.EntityFrameworkCore;
using MonitoramentoAmbiental.Models;

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

    public async Task<PagedResult<T>> GetAll(int page, int pageSize)
    {
        var totalItems = await _dbSet.CountAsync(entity => EF.Property<DateTime?>(entity, "DeletadoEm") == null);

        var items = await _dbSet
            .Where(entity => EF.Property<DateTime?>(entity, "DeletadoEm") == null)
            .OrderByDescending(entity => EF.Property<DateTime>(entity, "CriadoEm"))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<T>
        {
            Items = items,
            Meta = new MetaData
            {
                TotalItems = totalItems,
                ItemCount = items.Count,
                ItemsPerPage = pageSize,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                CurrentPage = page
            }
        };
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
