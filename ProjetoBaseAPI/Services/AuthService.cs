using Microsoft.Extensions.Configuration;
using ProjetoBaseAPI.Data.Repositories;
using ProjetoBaseAPI.Models;
using ProjetoBaseAPI.Models.Auth;
using ProjetoBaseAPI.Utils;
using System.Security.Claims;

namespace ProjetoBaseAPI.Services
{
	public class AuthService
	{
		private readonly IAuthRepository _authRepository;
		private readonly IConfiguration _configuration;

		public AuthService(IAuthRepository authRepository, IConfiguration configuration)
		{
			_authRepository = authRepository;
			_configuration = configuration;
		}

		public async Task<LoginResponse> Login(LoginRequest loginRequest)
		{
			var usuario = await _authRepository.ObterUsuarioPorEmail(loginRequest.nomEmail);

			if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginRequest.senhaHash, usuario.senhaHash))
			{
				throw new Exception("Usuário ou senha inválidos.");
			}

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, usuario.codUsuario.ToString()),
				new Claim(ClaimTypes.Name, usuario.nomUsuario),
				new Claim(ClaimTypes.Email, usuario.nomEmail)
			};

			var token = TokenHelper.GenerateAccessToken(usuario.nomEmail, claims, _configuration);

			var sessao = new SessaoModel
			{
				codUsuario = usuario.codUsuario,
				token = token.AccessToken,
				refreshToken = token.RefreshToken,
				expiracaoRefreshToken = token.AccessTokenExpiration.Value.AddMinutes(int.Parse(_configuration["Jwt:RefreshTokenExpirationMinutes"])),
				datCriacao = DateTime.UtcNow,
				staAtivo = true
			};

			await _authRepository.CriarSessao(sessao);

			return token;
		}

		public async Task<LoginResponse> RefreshToken(string refreshToken)
		{
			var sessao = await _authRepository.ObterSessaoPorRefreshToken(refreshToken);

			if (sessao == null || sessao.expiracaoRefreshToken < DateTime.UtcNow || !sessao.staAtivo)
			{
				throw new Exception("Refresh token inválido ou expirado.");
			}

			var principal = TokenHelper.GetPrincipalFromExpiredToken(sessao.token, _configuration);
			var username = principal.Identity.Name;
			var claims = principal.Claims;

			var newToken = TokenHelper.GenerateAccessToken(username, claims, _configuration);

			sessao.token = newToken.AccessToken;
			sessao.refreshToken = newToken.RefreshToken;
			sessao.expiracaoRefreshToken = newToken.AccessTokenExpiration.Value.AddMinutes(int.Parse(_configuration["Jwt:RefreshTokenExpirationMinutes"]));

			await _authRepository.AtualizarSessao(sessao);

			return newToken;
		}

		public async Task Logout(string refreshToken)
		{
			await _authRepository.RemoverSessao(refreshToken);
		}
	}
}