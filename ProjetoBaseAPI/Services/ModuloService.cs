using ProjetoBaseAPI.Data.Repositories;
using ProjetoBaseAPI.Models.Menu;

namespace ProjetoBaseAPI.Services
{
	public class ModuloService
	{
		private readonly IModuloRepository _moduloRepository;

		public ModuloService(IModuloRepository moduloRepository)
		{
			_moduloRepository = moduloRepository;
		}

		public async Task<IEnumerable<ModuloModel>> ObterTodosModulos()
		{
			return await _moduloRepository.ObterTodosModulos();
		}

		public async Task<ModuloModel> ObterModuloPorId(int codModulo)
		{
			return await _moduloRepository.ObterModuloPorId(codModulo);
		}

		public async Task<ModuloModel> CriarModulo(ModuloModel modulo)
		{
			// Aqui você pode adicionar validações e regras de negócio antes de criar o módulo
			return await _moduloRepository.CriarModulo(modulo);
		}

		public async Task<ModuloModel> AtualizarModulo(ModuloModel modulo)
		{
			// Aqui você pode adicionar validações e regras de negócio antes de atualizar o módulo
			return await _moduloRepository.AtualizarModulo(modulo);
		}

		public async Task DesativarModulo(int codModulo)
		{
			await _moduloRepository.DesativarModulo(codModulo);
		}
	}
}