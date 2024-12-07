using System.Net.Http.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using MonitoramentoAmbiental.Models;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace MonitoramentoAmbiental.Tests;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;
    readonly string request = "http://localhost:5045/api/Alerta";

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private int idTeste = 5;

    [Fact]
    public async Task ListarAlertas()
    {
        using (var client = new HttpClient())
        {
            var response = await client.GetAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }

    [Fact]
    public async Task BuscarAlertaPorId()
    {
        using (var client = new HttpClient())
        {
            var response = await client.GetAsync(request + "/" + idTeste);

            response.EnsureSuccessStatusCode();
        }
    }

    [Fact]
    public async Task CriarAlerta()
    {
        var alerta = new AlertaModel();

        alerta.Tipo = "Tipo teste";
        alerta.Descricao = "Descricao teste";
        alerta.Localidade = "Localidade teste";
        alerta.Categoria = "Categoria teste";
        alerta.DataInicio = DateTime.Now;
        alerta.PrevisaoTermino = DateTime.Now;
        
        using (var client = new HttpClient())
        {
            var content = new StringContent(JsonConvert.SerializeObject(alerta), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(request, content);

            response.EnsureSuccessStatusCode();
        }
    }

    [Fact]
    public async Task AtualizarAlerta()
    {
        using (var client = new HttpClient())
        {
            var idASerAtualizado = 3;

            var alerta = new AlertaModel();

            alerta.Id = idASerAtualizado;
            alerta.Tipo = "Tipo teste";
            alerta.Descricao = "Descricao teste";
            alerta.Localidade = "Localidade teste";
            alerta.Categoria = "Categoria teste só que atualizada";
            alerta.DataInicio = DateTime.Now;
            alerta.PrevisaoTermino = DateTime.Now;

            var content = new StringContent(JsonConvert.SerializeObject(alerta), Encoding.UTF8, "application/json");
            
            var response = await client.PutAsync(request + "/" + idTeste, content);

            response.EnsureSuccessStatusCode();
        }
    }

    [Fact]
    public async Task deletarAlerta()
    {
        using (var client = new HttpClient())
        {
            var idASerDeletado = 3;

            var response = await client.DeleteAsync(request + "/" + idTeste);
        }
    }
}