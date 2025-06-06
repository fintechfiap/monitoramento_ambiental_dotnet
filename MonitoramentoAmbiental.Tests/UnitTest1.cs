using System.Net.Http.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using MonitoramentoAmbiental.Models;
using Newtonsoft.Json;
using Xunit.Abstractions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MonitoramentoAmbiental.Tests;

[Collection("Sequencial")]
public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;
    private const string Request = "http://localhost:5045/api/Alerta";

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private int _idTeste = 23;

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
            var response = await client.PostAsync(Request, content);

            var resultString = response.Content.ReadAsStringAsync().Result;
                
            int resultId = JsonSerializer.Deserialize<AlertaModel>(resultString)!.Id;

            _idTeste = resultId;
            
            response.EnsureSuccessStatusCode();
        }
    }
    
    [Fact]
    public async Task ListarAlertas()
    {
        using (var client = new HttpClient())
        {
            var response = await client.GetAsync(Request);

            response.EnsureSuccessStatusCode();
        }
    }

    [Fact]
    public async Task BuscarAlertaPorId()
    {
        using (var client = new HttpClient())
        {
            
            var response = await client.GetAsync(Request + "/" + _idTeste);

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
            
            var response = await client.PutAsync(Request + "/" + _idTeste, content);

            response.EnsureSuccessStatusCode();
        }
    }

    [Fact]
    public async Task DeletarAlerta()
    {
        using (var client = new HttpClient())
        {

            var response = await client.DeleteAsync(Request + "/" + _idTeste);

            response.EnsureSuccessStatusCode();
        }
    }

}