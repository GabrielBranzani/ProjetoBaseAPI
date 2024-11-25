using ProjetoBaseAPI.Data.Repositories;
using ProjetoBaseAPI.Models.Menu;

namespace ProjetoBaseAPI.Services
{
	public class MenuService
	{
		private readonly IMenuRepository _menuRepository;

		public MenuService(IMenuRepository menuRepository)
		{
			_menuRepository = menuRepository;
		}

		public async Task<IEnumerable<MenuModel>> ObterTodosMenus()
		{
			return await _menuRepository.ObterTodosMenus();
		}

		public async Task<MenuModel> ObterMenuPorId(int codMenu)
		{
			return await _menuRepository.ObterMenuPorId(codMenu);
		}

		public async Task<MenuModel> CriarMenu(MenuModel menu)
		{
			// Aqui você pode adicionar validações e regras de negócio antes de criar o menu
			return await _menuRepository.CriarMenu(menu);
		}

		public async Task<MenuModel> AtualizarMenu(MenuModel menu)
		{
			// Aqui você pode adicionar validações e regras de negócio antes de atualizar o menu
			return await _menuRepository.AtualizarMenu(menu);
		}

		public async Task DesativarMenu(int codMenu)
		{
			await _menuRepository.DesativarMenu(codMenu);
		}
	}
}