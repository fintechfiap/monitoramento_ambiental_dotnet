using System.Net;
using System.Net.Http.Json;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using MonitoramentoAmbiental.Models;
using MonitoramentoAmbiental.ViewModels;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using Xunit;

namespace MonitoramentoAmbiental.Tests.Steps;

[Binding]
public class AlertaSteps : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private AlertaViewModel? _alertaViewModel;
    private HttpResponseMessage? _response;
    private AlertaModel? _alertaCriado;
    private readonly ScenarioContext _scenarioContext;

    public AlertaSteps(CustomWebApplicationFactory<Program> factory, ScenarioContext scenarioContext)
    {
        _factory = factory;
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        _scenarioContext = scenarioContext;
    }

    [Given(@"que tenho um novo alerta climático válido para criar")]
    public void DadoTenhoUmNovoAlertaClimaticoValidoParaCriar()
    {
        _alertaViewModel = new AlertaViewModel
        {
            Tipo = "Tempestade",
            Descricao = "Alerta de tempestade severa",
            Localidade = "São Paulo - SP",
            Categoria = "Meteorológico",
            DataInicio = DateTime.Now,
            PrevisaoTermino = DateTime.Now.AddDays(1)
        };
    }

    [When(@"eu enviar uma requisição POST para criar o alerta")]
    public async Task QuandoEnviarRequisicaoPostParaCriarAlerta()
    {
        var content = new StringContent(JsonConvert.SerializeObject(_alertaViewModel), Encoding.UTF8, "application/json");
        _response = await _client.PostAsync("/api/Alerta", content);
        
        if (_response.IsSuccessStatusCode)
        {
            _alertaCriado = await _response.Content.ReadFromJsonAsync<AlertaModel>();
            _scenarioContext["AlertaCriado"] = _alertaCriado;
        }
    }

    [Then(@"o sistema deve retornar status code (.*)")]
    public void EntaoSistemaDeveRetornarStatusCode(int statusCode)
    {
        _response.Should().NotBeNull();
        _response!.StatusCode.Should().Be((HttpStatusCode)statusCode);
    }

    [Then(@"retornar os dados do alerta criado com um ID")]
    public void EntaoRetornarDadosAlertaCriadoComId()
    {
        _alertaCriado.Should().NotBeNull();
        _alertaViewModel.Should().NotBeNull();
        
        _alertaCriado!.Id.Should().BeGreaterThan(0);
        _alertaCriado.Tipo.Should().Be(_alertaViewModel!.Tipo);
        _alertaCriado.Descricao.Should().Be(_alertaViewModel.Descricao);
        _alertaCriado.Localidade.Should().Be(_alertaViewModel.Localidade);
        _alertaCriado.Categoria.Should().Be(_alertaViewModel.Categoria);
    }

    [Then(@"o alerta deve estar disponível para consulta")]
    public async Task EntaoOAlertaDeveEstarDisponivelParaConsulta()
    {
        _alertaCriado.Should().NotBeNull();
        var alertaCriado = _scenarioContext.Get<AlertaModel>("AlertaCriado");
        _response = await _client.GetAsync($"/api/Alerta/{alertaCriado.Id}");
        _response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var alertaConsultado = await _response.Content.ReadFromJsonAsync<AlertaModel>();
        alertaConsultado.Should().NotBeNull();
        alertaConsultado!.Id.Should().Be(alertaCriado.Id);
    }

    [Given(@"que tenho dados inválidos de um alerta")]
    public void DadoTenhoDadosInvalidosDeUmAlerta()
    {
        _alertaViewModel = new AlertaViewModel
        {
            Tipo = "", // Tipo é obrigatório
            Descricao = "Alerta de tempestade severa",
            Localidade = "São Paulo - SP",
            Categoria = "Meteorológico",
            DataInicio = DateTime.Now,
            PrevisaoTermino = DateTime.Now.AddDays(1)
        };
    }

    [Then(@"retornar as mensagens de validação apropriadas")]
    public async Task EntaoRetornarMensagensValidacaoApropriadas()
    {
        _response.Should().NotBeNull();
        var content = await _response!.Content.ReadAsStringAsync();
        content.Should().Contain("Tipo é obrigatório");
    }

    [Given(@"que existe um alerta climático cadastrado")]
    public async Task DadoExisteAlertaClimaticoCadastrado()
    {
        DadoTenhoUmNovoAlertaClimaticoValidoParaCriar();
        await QuandoEnviarRequisicaoPostParaCriarAlerta();
        _response.Should().NotBeNull();
        _response!.IsSuccessStatusCode.Should().BeTrue();
    }

    [When(@"eu enviar uma requisição GET para buscar o alerta pelo ID")]
    public async Task QuandoEnviarRequisicaoGetParaBuscarAlertaPorId()
    {
        var alertaCriado = _scenarioContext.Get<AlertaModel>("AlertaCriado");
        _response = await _client.GetAsync($"/api/Alerta/{alertaCriado.Id}");
    }

    [Then(@"retornar os dados completos do alerta")]
    public async Task EntaoRetornarDadosCompletosDoAlerta()
    {
        _response.Should().NotBeNull();
        var alertaRetornado = await _response!.Content.ReadFromJsonAsync<AlertaModel>();
        var alertaCriado = _scenarioContext.Get<AlertaModel>("AlertaCriado");
        
        alertaRetornado.Should().NotBeNull();
        alertaRetornado!.Id.Should().Be(alertaCriado.Id);
        alertaRetornado.Tipo.Should().Be(alertaCriado.Tipo);
        alertaRetornado.Descricao.Should().Be(alertaCriado.Descricao);
        alertaRetornado.Localidade.Should().Be(alertaCriado.Localidade);
        alertaRetornado.Categoria.Should().Be(alertaCriado.Categoria);
    }

    [When(@"eu enviar uma requisição GET para buscar um alerta com ID inexistente")]
    public async Task QuandoEnviarRequisicaoGetParaBuscarAlertaInexistente()
    {
        _response = await _client.GetAsync("/api/Alerta/99999");
    }

    [Then(@"retornar uma mensagem informando que o alerta não foi encontrado")]
    public async Task EntaoRetornarMensagemAlertaNaoEncontrado()
    {
        _response.Should().NotBeNull();
        var content = await _response!.Content.ReadAsStringAsync();
        content.Should().Contain("Alerta não encontrado");
    }
} 