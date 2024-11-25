using Dapper;
using ProjetoBaseAPI.Models.Menu;

namespace ProjetoBaseAPI.Data.Repositories
{
	public class ModuloRepository : IModuloRepository
	{
		private readonly DapperContext _context;

		public ModuloRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<ModuloModel>> ObterTodosModulos()
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadModulos @Acao = 'CONSULTARTODOS'";
			return await connection.QueryAsync<ModuloModel>(query);
		}

		public async Task<ModuloModel> ObterModuloPorId(int codModulo)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadModulos @Acao = 'CONSULTAPORID', @codModulo = @codModulo";
			return await connection.QueryFirstOrDefaultAsync<ModuloModel>(query, new { codModulo });
		}

		public async Task<ModuloModel> CriarModulo(ModuloModel modulo)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadModulos @Acao = 'INSERIR', @nomModulo = @nomModulo, @nomIcone = @nomIcone, @ordem = @ordem, @staAtivo = @staAtivo";
			return await connection.QuerySingleAsync<ModuloModel>(query, modulo);
		}

		public async Task<ModuloModel> AtualizarModulo(ModuloModel modulo)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadModulos @Acao = 'ATUALIZAR', @codModulo = @codModulo, @nomModulo = @nomModulo, @nomIcone = @nomIcone, @ordem = @ordem, @staAtivo = @staAtivo";
			return await connection.QuerySingleAsync<ModuloModel>(query, modulo);
		}

		public async Task DesativarModulo(int codModulo)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadModulos @Acao = 'DESATIVAR', @codModulo = @codModulo";
			await connection.ExecuteAsync(query, new { codModulo });
		}
	}
}