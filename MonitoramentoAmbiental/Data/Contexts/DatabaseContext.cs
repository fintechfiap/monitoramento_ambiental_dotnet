using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using MonitoramentoAmbiental.Models;

namespace MonitoramentoAmbiental.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<AlertaModel> Alertas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AlertaModel>().ToTable("TBL_ALERTAS_CLIMATICOS");
            modelBuilder.Entity<UsuarioModel>().ToTable("TBL_USUARIOS");
        }
    }
}