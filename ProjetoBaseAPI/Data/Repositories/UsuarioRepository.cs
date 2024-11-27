using Dapper;
using ProjetoBaseAPI.Models;

namespace ProjetoBaseAPI.Data.Repositories
{
	public class UsuarioRepository : IUsuarioRepository
	{
		private readonly DapperContext _context;

		public UsuarioRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<UsuariosModel> Inserir(UsuariosModel usuario)
		{
			using (var connection = _context.CreateConnection())
			{
				var parameters = new DynamicParameters();
				parameters.Add("Acao", "INSERIR");
				parameters.Add("numCPF", usuario.numCPF);
				parameters.Add("nomUsuario", usuario.nomUsuario);
				parameters.Add("senhaHash", usuario.SenhaHash);
				parameters.Add("staAtivo", usuario.staAtivo);

				return await connection.QueryFirstOrDefaultAsync<UsuariosModel>("stpCalUsuarios", parameters, commandType: System.Data.CommandType.StoredProcedure);
			}
		}

		public async Task<UsuariosModel> ConsultarPorId(int codUsuario)
		{
			using (var connection = _context.CreateConnection())
			{
				var parameters = new DynamicParameters();
				parameters.Add("Acao", "CONSULTAPORID");
				parameters.Add("codUsuario", codUsuario);

				return await connection.QueryFirstOrDefaultAsync<UsuariosModel>("stpCalUsuarios", parameters, commandType: System.Data.CommandType.StoredProcedure);
			}
		}

		public async Task<IEnumerable<UsuariosModel>> ConsultarTodos()
		{
			using (var connection = _context.CreateConnection())
			{
				var parameters = new DynamicParameters();
				parameters.Add("Acao", "CONSULTARTODOS");

				return await connection.QueryAsync<UsuariosModel>("stpCalUsuarios", parameters, commandType: System.Data.CommandType.StoredProcedure);
			}
		}

		public async Task<UsuariosModel> Atualizar(UsuariosModel usuario)
		{
			using (var connection = _context.CreateConnection())
			{
				var parameters = new DynamicParameters();
				parameters.Add("Acao", "ATUALIZAR");
				parameters.Add("codUsuario", usuario.codUsuario);
				parameters.Add("numCPF", usuario.numCPF);
				parameters.Add("nomUsuario", usuario.nomUsuario);
				parameters.Add("senhaHash", usuario.SenhaHash);
				parameters.Add("staAtivo", usuario.staAtivo);

				return await connection.QueryFirstOrDefaultAsync<UsuariosModel>("stpCalUsuarios", parameters, commandType: System.Data.CommandType.StoredProcedure);
			}
		}

		public async Task<UsuariosModel> Desativar(int codUsuario)
		{
			using (var connection = _context.CreateConnection())
			{
				var parameters = new DynamicParameters();
				parameters.Add("Acao", "DESATIVAR");
				parameters.Add("codUsuario", codUsuario);

				return await connection.QueryFirstOrDefaultAsync<UsuariosModel>("stpCalUsuarios", parameters, commandType: System.Data.CommandType.StoredProcedure);
			}
		}

		public async Task<UsuariosModel> ConsultarPorCPF(string numCPF)
		{
			using (var connection = _context.CreateConnection())
			{
				var parameters = new DynamicParameters();
				parameters.Add("Acao", "CONSULTARPORCPF");
				parameters.Add("numCPF", numCPF);

				return await connection.QueryFirstOrDefaultAsync<UsuariosModel>("stpCalUsuarios", parameters, commandType: System.Data.CommandType.StoredProcedure);
			}
		}

		public async Task<UsuariosModel> VerificarLogin(string numCPF, string senhaHash)
		{
			using (var connection = _context.CreateConnection())
			{
				var parameters = new DynamicParameters();
				parameters.Add("Acao", "VERIFICARLOGIN");
				parameters.Add("numCPF", numCPF);
				parameters.Add("senhaHash", senhaHash);

				return await connection.QueryFirstOrDefaultAsync<UsuariosModel>("stpCalUsuarios", parameters, commandType: System.Data.CommandType.StoredProcedure);
			}
		}
	}
}