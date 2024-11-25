using Dapper;
using ProjetoBaseAPI.Models.Menu;

namespace ProjetoBaseAPI.Data.Repositories
{
	public class MenuRepository : IMenuRepository
	{
		private readonly DapperContext _context;

		public MenuRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<MenuModel>> ObterTodosMenus()
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadMenus @Acao = 'CONSULTARTODOS'";
			return await connection.QueryAsync<MenuModel>(query);
		}

		public async Task<MenuModel> ObterMenuPorId(int codMenu)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadMenus @Acao = 'CONSULTAPORID', @codMenu = @codMenu";
			return await connection.QueryFirstOrDefaultAsync<MenuModel>(query, new { codMenu });
		}

		public async Task<MenuModel> CriarMenu(MenuModel menu)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadMenus @Acao = 'INSERIR', @nomMenu = @nomMenu, @codModulo = @codModulo, @nomIcone = @nomIcone, @ordem = @ordem, @staAtivo = @staAtivo";
			return await connection.QuerySingleAsync<MenuModel>(query, menu);
		}

		public async Task<MenuModel> AtualizarMenu(MenuModel menu)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadMenus @Acao = 'ATUALIZAR', @codMenu = @codMenu, @nomMenu = @nomMenu, @codModulo = @codModulo, @nomIcone = @nomIcone, @ordem = @ordem, @staAtivo = @staAtivo";
			return await connection.QuerySingleAsync<MenuModel>(query, menu);
		}

		public async Task DesativarMenu(int codMenu)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadMenus @Acao = 'DESATIVAR', @codMenu = @codMenu";
			await connection.ExecuteAsync(query, new { codMenu });
		}
	}
}