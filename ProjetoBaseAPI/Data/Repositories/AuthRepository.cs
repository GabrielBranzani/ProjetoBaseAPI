using Dapper;
using ProjetoBaseAPI.Models;
using ProjetoBaseAPI.Models.Auth;

namespace ProjetoBaseAPI.Data.Repositories
{
	public class AuthRepository : IAuthRepository
	{
		private readonly DapperContext _context;

		public AuthRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<UsuarioModel> ObterUsuarioPorEmail(string nomEmail)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadUsuario @Acao = 'CONSULTARPOREMAIL', @nomEmail = @nomEmail";
			return await connection.QueryFirstOrDefaultAsync<UsuarioModel>(query, new { nomEmail });
		}

		public async Task<SessaoModel> CriarSessao(SessaoModel sessao)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalControleDeSessoes @Acao = 'INSERIR', @codUsuario = @codUsuario, @token = @token, " +
						"@refreshToken = @refreshToken, @expiracaoRefreshToken = @expiracaoRefreshToken, @datCriacao = @datCriacao, @staAtivo = @staAtivo";
			return await connection.QuerySingleAsync<SessaoModel>(query, sessao);
		}

		public async Task<SessaoModel> ObterSessaoPorRefreshToken(string refreshToken)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalControleDeSessoes @Acao = 'CONSULTAPORREFRESHTOKEN', @refreshToken = @refreshToken";
			return await connection.QueryFirstOrDefaultAsync<SessaoModel>(query, new { refreshToken });
		}

		public async Task<SessaoModel> AtualizarSessao(SessaoModel sessao)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalControleDeSessoes @Acao = 'RENOVARTOKEN', @codSessao = @codSessao, @token = @token, " +
						"@refreshToken = @refreshToken, @expiracaoRefreshToken = @expiracaoRefreshToken";
			return await connection.QuerySingleAsync<SessaoModel>(query, sessao);
		}

		public async Task RemoverSessao(string refreshToken)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalControleDeSessoes @Acao = 'DESATIVAR', @refreshToken = @refreshToken";
			await connection.ExecuteAsync(query, new { refreshToken });
		}
	}
}