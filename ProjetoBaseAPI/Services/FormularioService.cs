using ProjetoBaseAPI.Data.Repositories;
using ProjetoBaseAPI.Models.Menu;

namespace ProjetoBaseAPI.Services
{
	public class FormularioService
	{
		private readonly IFormularioRepository _formularioRepository;

		public FormularioService(IFormularioRepository formularioRepository)
		{
			_formularioRepository = formularioRepository;
		}

		public async Task<IEnumerable<FormularioModel>> ObterTodosFormularios()
		{
			return await _formularioRepository.ObterTodosFormularios();
		}

		public async Task<FormularioModel> ObterFormularioPorId(int codFormulario)
		{
			return await _formularioRepository.ObterFormularioPorId(codFormulario);
		}

		public async Task<FormularioModel> CriarFormulario(FormularioModel formulario)
		{
			// Aqui você pode adicionar validações e regras de negócio antes de criar o formulário
			return await _formularioRepository.CriarFormulario(formulario);
		}

		public async Task<FormularioModel> AtualizarFormulario(FormularioModel formulario)
		{
			// Aqui você pode adicionar validações e regras de negócio antes de atualizar o formulário
			return await _formularioRepository.AtualizarFormulario(formulario);
		}

		public async Task DesativarFormulario(int codFormulario)
		{
			await _formularioRepository.DesativarFormulario(codFormulario);
		}
	}
}