using ProjetoBaseAPI.Data.Repositories;
using ProjetoBaseAPI.Models.Menu;

namespace ProjetoBaseAPI.Services
{
	public class MenuUsuarioService
	{
		private readonly IMenuUsuarioRepository _menuUsuarioRepository;

		public MenuUsuarioService(IMenuUsuarioRepository menuUsuarioRepository)
		{
			_menuUsuarioRepository = menuUsuarioRepository;
		}

		public async Task<IEnumerable<MenuUsuarioModel>> ObterMenusDoUsuario(int codUsuario)
		{
			return await _menuUsuarioRepository.ObterMenusDoUsuario(codUsuario);
		}
	}
}