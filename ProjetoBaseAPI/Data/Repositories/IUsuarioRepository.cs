using ProjetoBaseAPI.Models;

namespace ProjetoBaseAPI.Data.Repositories
{
	public interface IUsuarioRepository
	{
		Task<UsuariosModel> Inserir(UsuariosModel usuario);
		Task<UsuariosModel> ConsultarPorId(int codUsuario);
		Task<IEnumerable<UsuariosModel>> ConsultarTodos();
		Task<UsuariosModel> Atualizar(UsuariosModel usuario);
		Task<UsuariosModel> Desativar(int codUsuario);
		Task<UsuariosModel> ConsultarPorCPF(string numCPF);
		Task<UsuariosModel> VerificarLogin(string numCPF, string senhaHash);
	}
}