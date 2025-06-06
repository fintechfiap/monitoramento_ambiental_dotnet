using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MonitoramentoAmbiental.Data.Contexts;
using MonitoramentoAmbiental.Repositories;
using MonitoramentoAmbiental.Services;
using System.Text;
using AutoMapper;
using MonitoramentoAmbiental.Models;
using MonitoramentoAmbiental.ViewModels;

namespace MonitoramentoAmbiental;

public partial class Program 
{ 
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adiciona Health Checks
        builder.Services.AddHealthChecks()
            .AddDbContextCheck<DatabaseContext>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidAudience = builder.Configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
            };

            options.Events = new JwtBearerEvents
            {
                OnChallenge = context =>
                {
                    context.HandleResponse();

                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";

                    var json = new { mensagem = "Não autorizado." };
                    return context.Response.WriteAsJsonAsync(json);
                }
            };
        });

        // Adiciona as políticas de autorização
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("PodeCriarAlerta", policy => 
                policy.RequireRole("Administrador", "Operador"));
            
            options.AddPolicy("PodeDeletarAlerta", policy => 
                policy.RequireClaim("PodeExcluir", "true"));
            
            options.AddPolicy("PodeAtualizarAlerta", policy => 
                policy.RequireRole("Administrador", "Operador"));
        });

        var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
        builder.Services.AddDbContext<DatabaseContext>(
            opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)
        );

        builder.Services.AddScoped<DbContext, DatabaseContext>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Insira o token JWT no formato: Bearer {seu_token}"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        var mapperConfig = new AutoMapper.MapperConfiguration(c =>
        {
            c.AllowNullCollections = true;
            c.AllowNullDestinationValues = true;
            c.CreateMap<AlertaModel, AlertaViewModel>();
            c.CreateMap<AlertaViewModel, AlertaModel>();
        });

        IMapper mapper = mapperConfig.CreateMapper();

        builder.Services.AddSingleton(mapper);

        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
        builder.Services.AddScoped<IAlertaRepository, AlertaRepository>();
        builder.Services.AddScoped<IAlertaService, AlertaService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        // Adiciona autenticação antes da autorização
        app.UseAuthentication();
        app.UseAuthorization();

        // Mapeia o endpoint de health check
        app.MapHealthChecks("/health");
            
        app.MapControllers();

        app.Run();
    }
}
