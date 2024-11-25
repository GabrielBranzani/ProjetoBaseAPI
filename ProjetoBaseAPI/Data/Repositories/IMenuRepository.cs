using ProjetoBaseAPI.Models.Menu;

namespace ProjetoBaseAPI.Data.Repositories
{
	public interface IMenuRepository
	{
		Task<IEnumerable<MenuModel>> ObterTodosMenus();
		Task<MenuModel> ObterMenuPorId(int codMenu);
		Task<MenuModel> CriarMenu(MenuModel menu);
		Task<MenuModel> AtualizarMenu(MenuModel menu);
		Task DesativarMenu(int codMenu);
	}
}