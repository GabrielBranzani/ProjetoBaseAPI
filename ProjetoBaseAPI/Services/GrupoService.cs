using ProjetoBaseAPI.Data.Repositories;
using ProjetoBaseAPI.Models;

namespace ProjetoBaseAPI.Services
{
	public class GrupoService
	{
		private readonly IGrupoRepository _grupoRepository;

		public GrupoService(IGrupoRepository grupoRepository)
		{
			_grupoRepository = grupoRepository;
		}

		public async Task<IEnumerable<GrupoModel>> ObterTodosGrupos()
		{
			return await _grupoRepository.ObterTodosGrupos();
		}

		public async Task<GrupoModel> ObterGrupoPorId(int codGrupoUsuario)
		{
			return await _grupoRepository.ObterGrupoPorId(codGrupoUsuario);
		}

		public async Task<GrupoModel> CriarGrupo(GrupoModel grupo)
		{
			// Aqui você pode adicionar validações e regras de negócio antes de criar o grupo
			return await _grupoRepository.CriarGrupo(grupo);
		}

		public async Task<GrupoModel> AtualizarGrupo(GrupoModel grupo)
		{
			// Aqui você pode adicionar validações e regras de negócio antes de atualizar o grupo
			return await _grupoRepository.AtualizarGrupo(grupo);
		}

		public async Task DesativarGrupo(int codGrupoUsuario)
		{
			await _grupoRepository.DesativarGrupo(codGrupoUsuario);
		}
	}
}