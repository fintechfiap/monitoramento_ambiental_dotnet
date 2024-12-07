using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MonitoramentoAmbiental.Models;
using MonitoramentoAmbiental.Services;
using MonitoramentoAmbiental.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace MonitoramentoAmbiental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("Gerencia os usuários, incluindo cadastro e autenticação.")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        public UsuarioController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        private IActionResult ErrorResponse(string message, int statusCode = 400)
        {
            return StatusCode(statusCode, new { mensagem = message });
        }

        [HttpPost("cadastro")]
        [SwaggerOperation(Summary = "Cadastra um novo usuário.", Description = "Recebe um modelo de usuário e o cria no banco de dados, retornando um token JWT.")]
        [SwaggerResponse(201, "Usuário cadastrado com sucesso.", typeof(object))]
        [SwaggerResponse(400, "O e-mail já está cadastrado ou o modelo enviado é inválido.")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] UsuarioCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var usuarioModel = _mapper.Map<UsuarioModel>(viewModel);

                var token = await _usuarioService.Cadastrar(usuarioModel);
                
                return Created("", new { token });
            }
            catch (InvalidOperationException ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Autentica um usuário.", Description = "Recebe email e senha e retorna um token JWT em caso de sucesso.")]
        [SwaggerResponse(200, "Login realizado com sucesso.", typeof(object))]
        [SwaggerResponse(401, "E-mail e/ou senha inválidas.")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _usuarioService.Login(viewModel.Email, viewModel.Senha);

            if (string.IsNullOrEmpty(token))
                return ErrorResponse("E-mail e/ou senha inválidas.", 401);

            return Ok(new { token });
        }
    }
}
