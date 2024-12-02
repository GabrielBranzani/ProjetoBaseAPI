using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjetoBaseAPI.Data.Repositories;
using ProjetoBaseAPI.Models.Gerenciador;
using ProjetoBaseAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjetoBaseAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UsuarioRepository _repository;
		private readonly TokenService _tokenService;
		private readonly IConfiguration _configuration; // Adicione esta linha

		public AuthController(UsuarioRepository repository, TokenService tokenService, IConfiguration configuration) // Adicione IConfiguration aqui
		{
			_repository = repository;
			_tokenService = tokenService;
			_configuration = configuration; // Inicialize aqui
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(UsuarioLoginDto usuarioLogin)
		{
			// 1. Buscar o usuário no banco de dados por CPF
			var usuario = await _repository.ConsultarPorCPF(usuarioLogin.numCPF);
			if (usuario == null)
				return Unauthorized(new { message = "CPF não encontrado." });

			// 2. Verificar se o usuário está ativo
			if (!usuario.staAtivo)
				return Unauthorized(new { message = "Usuário inativo." });

			// 3. Validar a senha usando BCrypt.Net-Next
			if (!BCrypt.Net.BCrypt.Verify(usuarioLogin.Senha, usuario.SenhaHash))
				return Unauthorized(new { message = "Senha incorreta." });

			// 4. Gerar o token JWT
			var token = _tokenService.GerarToken(usuario);

			// 5. Gerar o refresh token como JWT
			var refreshToken = _tokenService.GerarRefreshToken(usuario);

			// 6. Configurar o cookie HttpOnly com o refresh token
			Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
			{
				HttpOnly = true,
				Expires = DateTime.UtcNow.AddDays(30),
				Secure = true // Use apenas em HTTPS
			});

			// 7. Retornar o token JWT principal
			return Ok(new { token });
		}

		[HttpPost("renovar-token")]
		public async Task<IActionResult> RenovarToken()
		{
			// 1. Obter o refresh token do cookie
			var refreshToken = Request.Cookies["refreshToken"];

			// 2. Validar o refresh token
			if (string.IsNullOrEmpty(refreshToken) || !ValidarRefreshToken(refreshToken))
				return BadRequest(new { message = "Refresh token inválido." });

			// 3. Obter o usuário associado ao refresh token (extrair do token)
			var usuario = ObterUsuarioDoRefreshToken(refreshToken);
			if (usuario == null)
				return NotFound(new { message = "Usuário não encontrado." });

			// 4. Gerar um novo token JWT
			var novoToken = _tokenService.GerarToken(usuario);

			// 5. Retornar o novo token
			return Ok(new { token = novoToken });
		}

		// Validação do refresh token
		private bool ValidarRefreshToken(string refreshToken)
		{
			try
			{
				// 1. Verificar se o token é um JWT válido
				var tokenHandler = new JwtSecurityTokenHandler();
				tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"])),
					ValidateIssuer = true,
					ValidIssuer = _configuration["Jwt:Issuer"],
					ValidateAudience = true,
					ValidAudience = _configuration["Jwt:Audience"],
					ValidateLifetime = true, // Verifica se o token expirou
					ClockSkew = TimeSpan.Zero // Remove a tolerância de tempo
				}, out SecurityToken validatedToken);

				// 2. Verificar se o token não expirou (já verificado na validação acima)
				// 3. Outras validações que você precisar...

				return true;
			}
			catch (Exception)
			{
				// Logar a exceção (opcional)
				return false;
			}
		}

		// Extrair informações do usuário do refresh token
		private UsuarioModel ObterUsuarioDoRefreshToken(string refreshToken)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.ReadJwtToken(refreshToken);

			var codUsuarioClaim = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
			var numCpfClaim = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

			if (codUsuarioClaim != null && numCpfClaim != null)
			{
				// Consultar o usuário no banco de dados pelo codUsuario e numCPF (opcional)
				// ...

				return new UsuarioModel { codUsuario = int.Parse(codUsuarioClaim), numCPF = numCpfClaim };
			}

			return null;
		}
	}
}