using ProjetoBaseAPI.Models.Menu;

namespace ProjetoBaseAPI.Data.Repositories
{
	public interface IFormularioRepository
	{
		Task<IEnumerable<FormularioModel>> ObterTodosFormularios();
		Task<FormularioModel> ObterFormularioPorId(int codFormulario);
		Task<FormularioModel> CriarFormulario(FormularioModel formulario);
		Task<FormularioModel> AtualizarFormulario(FormularioModel formulario);
		Task DesativarFormulario(int codFormulario);
	}
}