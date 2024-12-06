using MonitoramentoAmbiental.Models;
using Microsoft.EntityFrameworkCore;

namespace MonitoramentoAmbiental.Repositories;

public class AlertaRepository : GenericRepository<AlertaModel>, IAlertaRepository
{
    public AlertaRepository(DbContext context) : base(context)
    {
    }
}
