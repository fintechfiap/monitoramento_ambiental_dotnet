using Microsoft.AspNetCore.Mvc;

namespace MonitoramentoAmbiental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new {Mensagem = "Tudo certo!"});
        }
    }
}
