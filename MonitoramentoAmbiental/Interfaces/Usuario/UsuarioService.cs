using MonitoramentoAmbiental.Models;

namespace MonitoramentoAmbiental.Services;

public interface IUsuarioService
{
    Task<string> Cadastrar(UsuarioModel usuario);
    Task<string?> Login(string email, string senha);
}
