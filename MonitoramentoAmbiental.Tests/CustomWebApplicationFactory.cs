using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MonitoramentoAmbiental.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace MonitoramentoAmbiental.Tests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove o DbContext original
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<DatabaseContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Adiciona o banco em memória
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            // Desabilita a autenticação e autorização para os testes
            services.RemoveAll(typeof(IAuthorizationHandler));
            services.AddSingleton<IAuthorizationHandler>(new AllowAnonymousAuthorizationHandler());
        });

        var host = base.CreateHost(builder);

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<DatabaseContext>();

            // Limpa o banco
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        return host;
    }
}

public class AllowAnonymousAuthorizationHandler : IAuthorizationHandler
{
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        foreach (var requirement in context.PendingRequirements.ToList())
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
} 