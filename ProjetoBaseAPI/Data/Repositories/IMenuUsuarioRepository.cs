// IMenuUsuarioRepository.cs
using ProjetoBaseAPI.Models.Menu;

namespace ProjetoBaseAPI.Data.Repositories
{
	public interface IMenuUsuarioRepository
	{
		Task<IEnumerable<MenuUsuarioModel>> ObterMenusDoUsuario(int codUsuario);
	}
}