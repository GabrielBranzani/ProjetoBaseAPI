using ProjetoBaseAPI.Models;

namespace ProjetoBaseAPI.Data.Repositories
{
	public interface IGrupoRepository
	{
		Task<IEnumerable<GrupoModel>> ObterTodosGrupos();
		Task<GrupoModel> ObterGrupoPorId(int codGrupoUsuario);
		Task<GrupoModel> CriarGrupo(GrupoModel grupo);
		Task<GrupoModel> AtualizarGrupo(GrupoModel grupo);
		Task DesativarGrupo(int codGrupoUsuario);
	}
}