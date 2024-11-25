using Dapper;
using ProjetoBaseAPI.Models.Permissoes;

namespace ProjetoBaseAPI.Data.Repositories
{
	public class PermissaoRepository : IPermissaoRepository
	{
		private readonly DapperContext _context;

		public PermissaoRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<PermissaoModel>> ObterTodasPermissoes()
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadPermissoes @Acao = 'CONSULTARTODOS'";
			return await connection.QueryAsync<PermissaoModel>(query);
		}

		public async Task<PermissaoModel> ObterPermissaoPorId(int codPermissao)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadPermissoes @Acao = 'CONSULTAPORID', @codPermissao = @codPermissao";
			return await connection.QueryFirstOrDefaultAsync<PermissaoModel>(query, new { codPermissao });
		}

		public async Task<PermissaoModel> CriarPermissao(PermissaoModel permissao)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadPermissoes @Acao = 'INSERIR', @codGrupoUsuario = @codGrupoUsuario, @codFormulario = @codFormulario, @consultar = @consultar, @adicionar = @adicionar, @editar = @editar, @excluir = @excluir, @staAtivo = @staAtivo";
			return await connection.QuerySingleAsync<PermissaoModel>(query, permissao);
		}

		public async Task<PermissaoModel> AtualizarPermissao(PermissaoModel permissao)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadPermissoes @Acao = 'ATUALIZAR', @codPermissao = @codPermissao, @codGrupoUsuario = @codGrupoUsuario, @codFormulario = @codFormulario, @consultar = @consultar, @adicionar = @adicionar, @editar = @editar, @excluir = @excluir, @staAtivo = @staAtivo";
			return await connection.QuerySingleAsync<PermissaoModel>(query, permissao);
		}

		public async Task DesativarPermissao(int codPermissao)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadPermissoes @Acao = 'DESATIVAR', @codPermissao = @codPermissao";
			await connection.ExecuteAsync(query, new { codPermissao });
		}
	}
}