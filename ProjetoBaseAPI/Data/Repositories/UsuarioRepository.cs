using Dapper;
using ProjetoBaseAPI.Data;
using ProjetoBaseAPI.Models.Gerenciador;
using System.Data;

namespace ProjetoBaseAPI.Data.Repositories
{
	public class UsuarioRepository
	{
		private readonly DapperContext _context;

		public UsuarioRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<UsuarioModel> Inserir(UsuarioModel usuario)
		{
			using var connection = _context.CreateConnection();
			var parameters = new DynamicParameters();
			parameters.Add("@Acao", "INSERIR");
			parameters.Add("@numCPF", usuario.numCPF);
			parameters.Add("@nomUsuario", usuario.nomUsuario);
			parameters.Add("@SenhaHash", usuario.SenhaHash);
			parameters.Add("@staAtivo", usuario.staAtivo);

			return await connection.QueryFirstOrDefaultAsync<UsuarioModel>("stpCalUsuarios", parameters, commandType: CommandType.StoredProcedure);
		}

		public async Task<UsuarioModel> ConsultarPorId(int codUsuario)
		{
			using var connection = _context.CreateConnection();
			var parameters = new DynamicParameters();
			parameters.Add("@Acao", "CONSULTAPORID");
			parameters.Add("@codUsuario", codUsuario);

			return await connection.QueryFirstOrDefaultAsync<UsuarioModel>("stpCalUsuarios", parameters, commandType: CommandType.StoredProcedure);
		}

		public async Task<IEnumerable<UsuarioModel>> ConsultarTodos()
		{
			using var connection = _context.CreateConnection();
			var parameters = new DynamicParameters();
			parameters.Add("@Acao", "CONSULTARTODOS");

			return await connection.QueryAsync<UsuarioModel>("stpCalUsuarios", parameters, commandType: CommandType.StoredProcedure);
		}

		public async Task<UsuarioModel> Atualizar(UsuarioModel usuario)
		{
			using var connection = _context.CreateConnection();
			var parameters = new DynamicParameters();
			parameters.Add("@Acao", "ATUALIZAR");
			parameters.Add("@codUsuario", usuario.codUsuario);
			parameters.Add("@numCPF", usuario.numCPF);
			parameters.Add("@nomUsuario", usuario.nomUsuario);
			parameters.Add("@SenhaHash", usuario.SenhaHash);
			parameters.Add("@staAtivo", usuario.staAtivo);

			return await connection.QueryFirstOrDefaultAsync<UsuarioModel>("stpCalUsuarios", parameters, commandType: CommandType.StoredProcedure);
		}

		public async Task<UsuarioModel> Desativar(int codUsuario)
		{
			using var connection = _context.CreateConnection();
			var parameters = new DynamicParameters();
			parameters.Add("@Acao", "DESATIVAR");
			parameters.Add("@codUsuario", codUsuario);

			return await connection.QueryFirstOrDefaultAsync<UsuarioModel>("stpCalUsuarios", parameters, commandType: CommandType.StoredProcedure);
		}

		public async Task<UsuarioModel> ConsultarPorCPF(string numCPF)
		{
			using var connection = _context.CreateConnection();
			var parameters = new DynamicParameters();
			parameters.Add("@Acao", "CONSULTARPORCPF");
			parameters.Add("@numCPF", numCPF);

			return await connection.QueryFirstOrDefaultAsync<UsuarioModel>("stpCalUsuarios", parameters, commandType: CommandType.StoredProcedure);
		}

		public async Task<UsuarioModel> VerificarLogin(string numCPF, string senhaHash)
		{
			using var connection = _context.CreateConnection();
			var parameters = new DynamicParameters();
			parameters.Add("@Acao", "VERIFICARLOGIN");
			parameters.Add("@numCPF", numCPF);
			parameters.Add("@SenhaHash", senhaHash);

			return await connection.QueryFirstOrDefaultAsync<UsuarioModel>("stpCalUsuarios", parameters, commandType: CommandType.StoredProcedure);
		}
	}
}