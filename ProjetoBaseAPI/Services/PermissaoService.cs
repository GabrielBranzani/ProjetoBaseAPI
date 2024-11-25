using ProjetoBaseAPI.Data.Repositories;
using ProjetoBaseAPI.Models.Permissoes;

namespace ProjetoBaseAPI.Services
{
	public class PermissaoService
	{
		private readonly IPermissaoRepository _permissaoRepository;

		public PermissaoService(IPermissaoRepository permissaoRepository)
		{
			_permissaoRepository = permissaoRepository; 

		}

		public async Task<IEnumerable<PermissaoModel>> ObterTodasPermissoes()
		{
			return await _permissaoRepository.ObterTodasPermissoes();
		}

		public async Task<PermissaoModel> ObterPermissaoPorId(int codPermissao)
		{
			return await _permissaoRepository.ObterPermissaoPorId(codPermissao);
		}

		public async Task<PermissaoModel> CriarPermissao(PermissaoModel permissao)
		{
			// Aqui você pode adicionar validações e regras de negócio antes de criar a permissão
			return await _permissaoRepository.CriarPermissao(permissao);
		}

		public async Task<PermissaoModel> AtualizarPermissao(PermissaoModel permissao)
		{
			// Aqui você pode adicionar validações e regras de negócio antes de atualizar a permissão
			return await _permissaoRepository.AtualizarPermissao(permissao);
		}

		public async Task DesativarPermissao(int codPermissao)
		{
			await _permissaoRepository.DesativarPermissao(codPermissao);
		}
	}
}