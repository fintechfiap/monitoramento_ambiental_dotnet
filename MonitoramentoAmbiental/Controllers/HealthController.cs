using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoramentoAmbiental.Data.Contexts;
using Swashbuckle.AspNetCore.Annotations;

namespace MonitoramentoAmbiental.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    private readonly DatabaseContext _context;

    public HealthController(DatabaseContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Verifica a saúde da aplicação", Description = "Endpoint para monitoramento da saúde da API e suas dependências")]
    [SwaggerResponse(200, "API está saudável")]
    [SwaggerResponse(503, "API está com problemas")]
    public async Task<IActionResult> Check()
    {
        try
        {
            // Verifica conexão com banco de dados
            await _context.Database.CanConnectAsync();

            return Ok(new
            {
                status = "Healthy",
                timestamp = DateTime.UtcNow,
                checks = new
                {
                    database = "Healthy",
                    api = "Healthy"
                }
            });
        }
        catch (Exception ex)
        {
            return StatusCode(503, new
            {
                status = "Unhealthy",
                timestamp = DateTime.UtcNow,
                checks = new
                {
                    database = "Unhealthy",
                    api = "Healthy"
                },
                error = ex.Message
            });
        }
    }
} 