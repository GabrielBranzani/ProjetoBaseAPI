using ProjetoBaseAPI.Models;

namespace ProjetoBaseAPI.Services
{
	public interface IUsuarioService
	{
		Task<UsuariosModel> CriarUsuario(UsuariosModel usuario);
		Task<UsuariosModel> ObterUsuarioPorId(int codUsuario);
		Task<IEnumerable<UsuariosModel>> ObterTodosUsuarios();
		Task<UsuariosModel> AtualizarUsuario(UsuariosModel usuario);
		Task<UsuariosModel> DesativarUsuario(int codUsuario);
		Task<UsuariosModel> ObterUsuarioPorCPF(string numCPF);
		Task<UsuariosModel> AutenticarUsuario(string numCPF, string senhaHash);
	}
}