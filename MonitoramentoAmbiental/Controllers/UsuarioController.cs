using Microsoft.AspNetCore.Mvc;

namespace MonitoramentoAmbiental.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : Controller
    {
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new {Mensagem = "Tudo certo!"});
        }

        // TODO: Cadastro
        // TODO: Login
    }
}
