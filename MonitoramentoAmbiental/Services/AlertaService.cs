using MonitoramentoAmbiental.Models;
using MonitoramentoAmbiental.Repositories;

namespace MonitoramentoAmbiental.Services;

public class AlertaService : GenericService<AlertaModel>, IAlertaService
{
    public AlertaService(IAlertaRepository repository) : base(repository)
    {
    }
}
