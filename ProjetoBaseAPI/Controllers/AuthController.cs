using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjetoBaseAPI.Models;
using ProjetoBaseAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjetoBaseAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IUsuarioService _usuarioService;
		private readonly ISessaoService _sessaoService;
		private readonly IConfiguration _configuration;

		public AuthController(IUsuarioService usuarioService, ISessaoService sessaoService, IConfiguration configuration)
		{
			_usuarioService = usuarioService;
			_sessaoService = sessaoService;
			_configuration = configuration;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(UsuariosModel usuario)
		{
			var usuarioAutenticado = await _usuarioService.AutenticarUsuario(usuario.numCPF, usuario.SenhaHash);
			if (usuarioAutenticado == null)
			{
				return Unauthorized();
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

			// Criar uma nova sessão com TokenJWT vazio inicialmente
			var sessao = new SessaoModel
			{
				codUsuario = usuarioAutenticado.codUsuario,
				TokenJWT = "",
				expTokenJWT = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:AccessTokenExpirationMinutes"])),
				codSignalR = 0,
				staAtivo = true
			};
			var novaSessao = await _sessaoService.CriarSessao(sessao);

			// Criar o token JWT com a claim codSessao após criar a sessão
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
					{
										new Claim(ClaimTypes.Name, usuarioAutenticado.codUsuario.ToString()),
										new Claim("codSessao", novaSessao.codSessao.ToString())
					}),
				Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:AccessTokenExpirationMinutes"])),
				Issuer = _configuration["Jwt:Issuer"],
				Audience = _configuration["Jwt:Audience"],
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);

			// Atualizar o TokenJWT na sessão com o tokenString gerado
			await _sessaoService.AtualizarToken(novaSessao.codSessao, tokenString, novaSessao.expTokenJWT);

			// Retornar o token e o código da sessão
			return Ok(new { Token = tokenString, CodSessao = novaSessao.codSessao });
		}
	}
}