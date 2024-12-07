using MonitoramentoAmbiental.Models;

namespace MonitoramentoAmbiental.Repositories;

public interface IUsuarioRepository
{
    Task<UsuarioModel?> FindByEmail(string email);
    Task<UsuarioModel> Register(UsuarioModel usuario);
}
