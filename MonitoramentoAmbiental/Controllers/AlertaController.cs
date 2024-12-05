using Microsoft.AspNetCore.Mvc;
using MonitoramentoAmbiental.Models;

namespace MonitoramentoAmbiental.Controllers
{
    public class AlertaController : Controller
    {

        [HttpPost]
        public async Alerta Criar()
        {
            return View();
        }
    }
}
