using Dapper;
using ProjetoBaseAPI.Models;

namespace ProjetoBaseAPI.Data.Repositories
{
	public class GrupoRepository : IGrupoRepository
	{
		private readonly DapperContext _context;

		public GrupoRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<GrupoModel>> ObterTodosGrupos()
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadGrupoUsuarios @Acao = 'CONSULTARTODOS'";
			return await connection.QueryAsync<GrupoModel>(query);
		}

		public async Task<GrupoModel> ObterGrupoPorId(int codGrupoUsuario)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadGrupoUsuarios @Acao = 'CONSULTAPORID', @codGrupoUsuario = @codGrupoUsuario";
			return await connection.QueryFirstOrDefaultAsync<GrupoModel>(query, new { codGrupoUsuario });
		}

		public async Task<GrupoModel> CriarGrupo(GrupoModel grupo)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadGrupoUsuarios @Acao = 'INSERIR', @nomGrupoUsuario = @nomGrupoUsuario, @staAtivo = @staAtivo";
			return await connection.QuerySingleAsync<GrupoModel>(query, grupo);
		}

		public async Task<GrupoModel> AtualizarGrupo(GrupoModel grupo)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadGrupoUsuarios @Acao = 'ATUALIZAR', @codGrupoUsuario = @codGrupoUsuario, @nomGrupoUsuario = @nomGrupoUsuario, @staAtivo = @staAtivo";
			return await connection.QuerySingleAsync<GrupoModel>(query, grupo);
		}

		public async Task DesativarGrupo(int codGrupoUsuario)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadGrupoUsuarios @Acao = 'DESATIVAR', @codGrupoUsuario = @codGrupoUsuario";
			await connection.ExecuteAsync(query, new { codGrupoUsuario });
		}
	}
}