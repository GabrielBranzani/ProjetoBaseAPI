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
				// Corrigido: Definir a data de expiração do RefreshToken como a data atual + 20 minutos
				expiracaoRefreshToken = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:RefreshTokenExpirationMinutes"])),
				datCriacao = DateTime.UtcNow,
				staAtivo = true
			};

			await _authRepository.CriarSessao(sessao);

			return token;
		}

		public async Task<LoginResponse> RefreshToken(string refreshToken)
		{
			// Log: Início do processo de refresh token
			Console.WriteLine($"AuthService - RefreshToken: Iniciando processo de refresh token. RefreshToken: {refreshToken}");

			try
			{
				var sessao = await _authRepository.ObterSessaoPorRefreshToken(refreshToken);

				if (sessao == null || sessao.expiracaoRefreshToken < DateTime.UtcNow || !sessao.staAtivo)
				{
					// Log: Refresh token inválido ou expirado
					Console.WriteLine($"AuthService - RefreshToken: Refresh token inválido ou expirado. RefreshToken: {refreshToken}");
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

				// Log: Refresh token concluído com sucesso
				Console.WriteLine($"AuthService - RefreshToken: Refresh token concluído com sucesso. RefreshToken: {refreshToken}");
				return newToken;
			}
			catch (Exception ex)
			{
				// Log: Erro ao renovar o token
				Console.WriteLine($"AuthService - RefreshToken: Erro ao renovar o token. RefreshToken: {refreshToken} - Erro: {ex.Message}");
				throw; // Repassa a exceção para ser tratada pelo controlador
			}
		}

		public async Task Logout(string refreshToken)
		{
			await _authRepository.RemoverSessao(refreshToken);
		}
	}
}