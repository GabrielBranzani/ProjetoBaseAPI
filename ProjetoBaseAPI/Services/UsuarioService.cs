using ProjetoBaseAPI.Data.Repositories;
using ProjetoBaseAPI.Models;

namespace ProjetoBaseAPI.Services
{
	public class UsuarioService : IUsuarioService
	{
		private readonly IUsuarioRepository _usuarioRepository;

		public UsuarioService(IUsuarioRepository usuarioRepository)
		{
			_usuarioRepository = usuarioRepository;
		}

		public async Task<UsuariosModel> CriarUsuario(UsuariosModel usuario)
		{
			// Aqui você pode adicionar lógica de negócio, como validações, 
			// antes de inserir o usuário no banco de dados
			return await _usuarioRepository.Inserir(usuario);
		}

		public async Task<UsuariosModel> ObterUsuarioPorId(int codUsuario)
		{
			return await _usuarioRepository.ConsultarPorId(codUsuario);
		}

		public async Task<IEnumerable<UsuariosModel>> ObterTodosUsuarios()
		{
			return await _usuarioRepository.ConsultarTodos();
		}

		public async Task<UsuariosModel> AtualizarUsuario(UsuariosModel usuario)
		{
			// Aqui você pode adicionar lógica de negócio, como validações, 
			// antes de atualizar o usuário no banco de dados
			return await _usuarioRepository.Atualizar(usuario);
		}

		public async Task<UsuariosModel> DesativarUsuario(int codUsuario)
		{
			return await _usuarioRepository.Desativar(codUsuario);
		}

		public async Task<UsuariosModel> ObterUsuarioPorCPF(string numCPF)
		{
			return await _usuarioRepository.ConsultarPorCPF(numCPF);
		}

		public async Task<UsuariosModel> AutenticarUsuario(string numCPF, string senhaHash)
		{
			// Como as senhas já estão em hash, não precisa gerar o hash novamente
			return await _usuarioRepository.VerificarLogin(numCPF, senhaHash);
		}
	}
}