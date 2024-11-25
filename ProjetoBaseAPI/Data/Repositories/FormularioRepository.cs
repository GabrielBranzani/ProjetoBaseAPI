using Dapper;
using ProjetoBaseAPI.Models.Menu;

namespace ProjetoBaseAPI.Data.Repositories
{
	public class FormularioRepository : IFormularioRepository
	{
		private readonly DapperContext _context;

		public FormularioRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<FormularioModel>> ObterTodosFormularios()
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadFormularios @Acao = 'CONSULTARTODOS'";
			return await connection.QueryAsync<FormularioModel>(query);
		}

		public async Task<FormularioModel> ObterFormularioPorId(int codFormulario)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadFormularios @Acao = 'CONSULTAPORID', @codFormulario = @codFormulario";
			return await connection.QueryFirstOrDefaultAsync<FormularioModel>(query, new { codFormulario });
		}

		public async Task<FormularioModel> CriarFormulario(FormularioModel formulario)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadFormularios @Acao = 'INSERIR', @nomFormulario = @nomFormulario, @codModulo = @codModulo, @codMenu = @codMenu, @nomRota = @nomRota, @nomIcone = @nomIcone, @ordem = @ordem, @staAtivo = @staAtivo";
			return await connection.QuerySingleAsync<FormularioModel>(query, formulario);
		}

		public async Task<FormularioModel> AtualizarFormulario(FormularioModel formulario)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadFormularios @Acao = 'ATUALIZAR', @codFormulario = @codFormulario, @nomFormulario = @nomFormulario, @codModulo = @codModulo, @codMenu = @codMenu, @nomRota = @nomRota, @nomIcone = @nomIcone, @ordem = @ordem, @staAtivo = @staAtivo";
			return await connection.QuerySingleAsync<FormularioModel>(query, formulario);
		}

		public async Task DesativarFormulario(int codFormulario)
		{
			using var connection = _context.CreateConnection();
			var query = "EXEC stpCalCadFormularios @Acao = 'DESATIVAR', @codFormulario = @codFormulario";
			await connection.ExecuteAsync(query, new { codFormulario });
		}
	}
}