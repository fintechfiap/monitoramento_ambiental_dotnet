using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MonitoramentoAmbiental.Models;
using MonitoramentoAmbiental.Repositories;

namespace MonitoramentoAmbiental.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordHasher<UsuarioModel> _passwordHasher;
        private readonly IConfiguration _configuration;

        public UsuarioService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = new PasswordHasher<UsuarioModel>();
            _configuration = configuration;
        }

        public async Task<string> Cadastrar(UsuarioModel usuario)
        {
            var usuarioExistente = await _usuarioRepository.FindByEmail(usuario.Email);
            if (usuarioExistente != null)
            {
                throw new InvalidOperationException("E-mail já está cadastrado.");
            }

            // Valida se a role é válida
            if (usuario.Role != "Administrador" && usuario.Role != "Operador")
            {
                throw new InvalidOperationException("Role inválida. Use 'Administrador' ou 'Operador'.");
            }

            usuario.SenhaHash = _passwordHasher.HashPassword(usuario, usuario.SenhaHash);
            usuario.CriadoEm = DateTime.Now;

            var usuarioCadastrado = await _usuarioRepository.Register(usuario);

            return GerarTokenJwt(usuarioCadastrado);
        }

        public async Task<string?> Login(string email, string senha)
        {
            var usuario = await _usuarioRepository.FindByEmail(email);

            if (usuario == null ||
                _passwordHasher.VerifyHashedPassword(usuario, usuario.SenhaHash, senha) == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return GerarTokenJwt(usuario);
        }

        private string GerarTokenJwt(UsuarioModel usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Name, usuario.Nome),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // Adiciona a role do usuário
            claims.Add(new Claim(ClaimTypes.Role, usuario.Role));

            // Se o usuário for administrador, adiciona a claim de exclusão
            if (usuario.Role == "Administrador")
            {
                claims.Add(new Claim("PodeExcluir", "true"));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(double.Parse(_configuration["JWT:ExpirationHours"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
