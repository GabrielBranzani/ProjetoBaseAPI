using ProjetoBaseAPI.Models.Menu;

namespace ProjetoBaseAPI.Data.Repositories
{
	public interface IModuloRepository
	{
		Task<IEnumerable<ModuloModel>> ObterTodosModulos();
		Task<ModuloModel> ObterModuloPorId(int codModulo);
		Task<ModuloModel> CriarModulo(ModuloModel modulo);
		Task<ModuloModel> AtualizarModulo(ModuloModel modulo);
		Task DesativarModulo(int codModulo);
	}
}