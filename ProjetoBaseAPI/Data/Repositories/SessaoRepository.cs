using Dapper;
using ProjetoBaseAPI.Models;

namespace ProjetoBaseAPI.Data.Repositories
{
	public class SessaoRepository : ISessaoRepository
	{
		private readonly DapperContext _context;

		public SessaoRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<SessaoModel> GerarSessao(SessaoModel sessao)
		{
			using (var connection = _context.CreateConnection())
			{
				var parameters = new DynamicParameters();
				parameters.Add("Acao", "GERARSESSAO");
				parameters.Add("codUsuario", sessao.codUsuario);
				parameters.Add("TokenJWT", sessao.TokenJWT);
				parameters.Add("expTokenJWT", sessao.expTokenJWT);
				parameters.Add("codSignalR", sessao.codSignalR);
				parameters.Add("staAtivo", sessao.staAtivo);

				return await connection.QueryFirstOrDefaultAsync<SessaoModel>("stpCalControleDeSessoes", parameters, commandType: System.Data.CommandType.StoredProcedure);
			}
		}

		public async Task<SessaoModel> AtualizarToken(int codSessao, string tokenJWT, DateTime expTokenJWT)
		{
			using (var connection = _context.CreateConnection())
			{
				var parameters = new DynamicParameters();
				parameters.Add("Acao", "ATUALIZARTOKEN");
				parameters.Add("codSessao", codSessao);
				parameters.Add("TokenJWT", tokenJWT);
				parameters.Add("expTokenJWT", expTokenJWT);

				return await connection.QueryFirstOrDefaultAsync<SessaoModel>("stpCalControleDeSessoes", parameters, commandType: System.Data.CommandType.StoredProcedure);
			}
		}

		public async Task<SessaoModel> AtualizarSignalR(int codSessao, int codSignalR)
		{
			using (var connection = _context.CreateConnection())
			{
				var parameters = new DynamicParameters();
				parameters.Add("Acao", "ATUALIZARSIGNALR");
				parameters.Add("codSessao", codSessao);
				parameters.Add("codSignalR", codSignalR);

				return await connection.QueryFirstOrDefaultAsync<SessaoModel>("stpCalControleDeSessoes", parameters, commandType: System.Data.CommandType.StoredProcedure);
			}
		}

		public async Task<SessaoModel> FinalizarSessao(int codSessao)
		{
			using (var connection = _context.CreateConnection())
			{
				var parameters = new DynamicParameters();
				parameters.Add("Acao", "FINALIZARSESSAO");
				parameters.Add("codSessao", codSessao);

				return await connection.QueryFirstOrDefaultAsync<SessaoModel>("stpCalControleDeSessoes", parameters, commandType: System.Data.CommandType.StoredProcedure);
			}
		}

		public async Task<SessaoModel> ManterAtiva(int codSessao)
		{
			using (var connection = _context.CreateConnection())
			{
				var parameters = new DynamicParameters();
				parameters.Add("Acao", "MANTERATIVA");
				parameters.Add("codSessao", codSessao);

				return await connection.QueryFirstOrDefaultAsync<SessaoModel>("stpCalControleDeSessoes", parameters, commandType: System.Data.CommandType.StoredProcedure);
			}
		}

		public async Task<SessaoModel> ConsultarPorId(int codSessao)
		{
			using (var connection = _context.CreateConnection())
			{
				var parameters = new DynamicParameters();
				parameters.Add("Acao", "CONSULTAPORID");
				parameters.Add("codSessao", codSessao);

				return await connection.QueryFirstOrDefaultAsync<SessaoModel>("stpCalControleDeSessoes", parameters, commandType: System.Data.CommandType.StoredProcedure);
			}
		}

		public async Task<IEnumerable<SessaoModel>> ConsultarTodas()
		{
			using (var connection = _context.CreateConnection())
			{
				var parameters = new DynamicParameters();
				parameters.Add("Acao", "CONSULTARTODAS");

				return await connection.QueryAsync<SessaoModel>("stpCalControleDeSessoes", parameters, commandType: System.Data.CommandType.StoredProcedure);
			}
		}

		public async Task DesativarSessoesExpiradas()
		{
			using (var connection = _context.CreateConnection())
			{
				var parameters = new DynamicParameters();
				parameters.Add("Acao", "DESATIVAR_EXPIRADAS");

				await connection.ExecuteAsync("stpCalControleDeSessoes", parameters, commandType: System.Data.CommandType.StoredProcedure);
			}
		}

	}
}