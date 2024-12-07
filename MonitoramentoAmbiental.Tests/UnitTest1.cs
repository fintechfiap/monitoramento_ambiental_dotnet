using System.Net.Http.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using MonitoramentoAmbiental.Models;
using Newtonsoft.Json;
using Xunit.Abstractions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MonitoramentoAmbiental.Tests;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;
    readonly string request = "http://localhost:5045/api/Alerta";

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private int _idTeste;

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

            var resultString = response.Content.ReadAsStringAsync().Result;
            
            _idTeste = JsonSerializer.Deserialize<AlertaModel>(resultString)!.Id;
            
            response.EnsureSuccessStatusCode();
        }
    }
    
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
            
            var response = await client.GetAsync(request + "/" + _idTeste);

            response.EnsureSuccessStatusCode();
        }
    }


    [Fact]
    public async Task AtualizarAlerta()
    {
        using (var client = new HttpClient())
        {

            var alerta = new AlertaModel();

            alerta.Id = _idTeste;
            alerta.Tipo = "Tipo teste";
            alerta.Descricao = "Descricao teste";
            alerta.Localidade = "Localidade teste";
            alerta.Categoria = "Categoria teste só que atualizada";
            alerta.DataInicio = DateTime.Now;
            alerta.PrevisaoTermino = DateTime.Now;

            var content = new StringContent(JsonConvert.SerializeObject(alerta), Encoding.UTF8, "application/json");
            
            var response = await client.PutAsync(request + "/" + _idTeste, content);

            response.EnsureSuccessStatusCode();
        }
    }

    [Fact]
    public async Task DeletarAlerta()
    {
        using (var client = new HttpClient())
        {

            var response = await client.DeleteAsync(request + "/" + _idTeste);
        }
    }
}