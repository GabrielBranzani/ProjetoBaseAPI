using ProjetoBaseAPI.Models.Permissoes;

namespace ProjetoBaseAPI.Data.Repositories
{
	public interface IPermissaoRepository
	{
		Task<IEnumerable<PermissaoModel>> ObterTodasPermissoes();
		Task<PermissaoModel> ObterPermissaoPorId(int codPermissao);
		Task<PermissaoModel> CriarPermissao(PermissaoModel permissao);
		Task<PermissaoModel> AtualizarPermissao(PermissaoModel permissao);
		Task DesativarPermissao(int codPermissao);
	}
}