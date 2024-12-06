using Microsoft.EntityFrameworkCore;
namespace MonitoramentoAmbiental.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        protected DatabaseContext()
        {
        }
    }
}