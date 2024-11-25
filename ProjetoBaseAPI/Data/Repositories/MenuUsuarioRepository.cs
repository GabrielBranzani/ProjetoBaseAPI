// MenuUsuarioRepository.cs
using Dapper;
using ProjetoBaseAPI.Models.Menu;

namespace ProjetoBaseAPI.Data.Repositories
{
	public class MenuUsuarioRepository : IMenuUsuarioRepository
	{
		private readonly DapperContext _context;

		public MenuUsuarioRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<MenuUsuarioModel>> ObterMenusDoUsuario(int codUsuario)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalModulosMenusFormulariosUsuario @codUsuario = @codUsuario";
			return await connection.QueryAsync<MenuUsuarioModel>(query, new { codUsuario });
		}
	}
}