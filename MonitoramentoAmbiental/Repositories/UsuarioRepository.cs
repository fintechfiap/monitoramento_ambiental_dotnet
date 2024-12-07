using Microsoft.EntityFrameworkCore;
using MonitoramentoAmbiental.Models;

namespace MonitoramentoAmbiental.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DbContext _context;
    private readonly DbSet<UsuarioModel> _dbSet;

    public UsuarioRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<UsuarioModel>();
    }

    public async Task<UsuarioModel> Register(UsuarioModel usuario)
    {
        _context.Entry(usuario).Property("CriadoEm").CurrentValue = DateTime.Now;
        await _dbSet.AddAsync(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<UsuarioModel?> FindByEmail(string email)
    {
        var emailLower = email.ToLower();

        return await _dbSet
            .Where(entity => EF.Property<DateTime?>(entity, "DeletadoEm") == null)
            .FirstOrDefaultAsync(entity => EF.Property<string>(entity, "Email").ToLower() == emailLower);
    }
}
