﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonitoramentoAmbiental.Models;
using MonitoramentoAmbiental.Services;
using MonitoramentoAmbiental.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace MonitoramentoAmbiental.Controllers;

[Route("api/[controller]")]
[SwaggerTag("Gerencia alertas climáticos, incluindo criação, atualização, leitura e exclusão.")]
public class AlertaController : ControllerBase
{
    private readonly IAlertaService _alertaService;
    private readonly IMapper _mapper;
    public AlertaController(IAlertaService alertaService, IMapper mapper)
    {
        _alertaService = alertaService;
        _mapper = mapper;
    }

    private IActionResult ErrorResponse(string message, int statusCode = 404)
    {
        return StatusCode(statusCode, new { mensagem = message });
    }


    [HttpPost]
    [Authorize(Policy = "PodeCriarAlerta")]
    [SwaggerOperation(Summary = "Cria um novo alerta climático.", Description = "Recebe um modelo de alerta e o cria no banco de dados.")]
    [SwaggerResponse(201, "O alerta foi criado com sucesso.", typeof(AlertaModel))]
    [SwaggerResponse(400, "O modelo enviado é inválido.")]
    public async Task<IActionResult> CriarAlerta([FromBody] AlertaViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var alertaModel = _mapper.Map<AlertaModel>(viewModel);

        var alertaCriado = await _alertaService.Criar(alertaModel);
        return CreatedAtAction(nameof(BuscarPorId), new { alertaId = alertaCriado.Id }, alertaCriado);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Lista todos os alertas climáticos.")]
    [SwaggerResponse(200, "Lista de alertas retornada com sucesso.", typeof(List<AlertaModel>))]
    public async Task<IActionResult> ListarAlertas([FromQuery] int page = 1, [FromQuery] int pageSize = 1)
    {
        var alertas = await _alertaService.ListarTodos(page, pageSize);
        return Ok(alertas);
    }

    [HttpGet("{alertaId}")]
    [SwaggerOperation(Summary = "Obtém um alerta climático pelo ID.", Description = "Retorna os detalhes de um alerta com base no ID fornecido.")]
    [SwaggerResponse(200, "Detalhes do alerta retornados com sucesso.", typeof(AlertaModel))]
    [SwaggerResponse(404, "Alerta não encontrado.")]
    public async Task<IActionResult> BuscarPorId(int alertaId)
    {
        var alerta = await _alertaService.BuscarPorId(alertaId);
        return alerta == null ? ErrorResponse("Alerta não encontrado.") : Ok(alerta);

    }

    [HttpPut("{alertaId}")]
    [Authorize(Policy = "PodeAtualizarAlerta")]
    [SwaggerOperation(Summary = "Atualiza um alerta climático existente.", Description = "Atualiza os dados do alerta com base no ID fornecido.")]
    [SwaggerResponse(200, "O alerta foi atualizado com sucesso.", typeof(AlertaModel))]
    [SwaggerResponse(404, "Alerta não encontrado.")]
    public async Task<IActionResult> AtualizarAlerta(int alertaId, [FromBody] AlertaViewModel viewModel)
    {
        var alertaModel = _mapper.Map<AlertaModel>(viewModel);
        var alertaAtualizado = await _alertaService.Atualizar(alertaId, alertaModel);
        return alertaAtualizado == null ? ErrorResponse("Alerta não encontrado.") : Ok(alertaAtualizado);
    }

    [HttpDelete("{alertaId}")]
    [Authorize(Policy = "PodeDeletarAlerta")]
    [SwaggerOperation(Summary = "Exclui um alerta climático.", Description = "Marca um alerta como excluído com base no ID fornecido.")]
    [SwaggerResponse(204, "O alerta foi excluído com sucesso.")]
    [SwaggerResponse(404, "Alerta não encontrado.")]
    public async Task<IActionResult> DeletarAlerta(int alertaId)
    {
        var sucesso = await _alertaService.Deletar(alertaId);
        return !sucesso ? ErrorResponse("Alerta não encontrado.") : NoContent();
    }
}
