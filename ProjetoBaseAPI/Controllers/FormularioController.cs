using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoBaseAPI.Models.Menu;
using ProjetoBaseAPI.Services;

namespace ProjetoBaseAPI.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class FormularioController : ControllerBase
	{
		private readonly FormularioService _formularioService;

		public FormularioController(FormularioService formularioService)
		{
			_formularioService = formularioService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<FormularioModel>>> ObterTodosFormularios()
		{
			var formularios = await _formularioService.ObterTodosFormularios();
			return Ok(formularios);
		}

		[HttpGet("{codFormulario}")]
		public async Task<ActionResult<FormularioModel>> ObterFormularioPorId(int codFormulario)
		{
			var formulario = await _formularioService.ObterFormularioPorId(codFormulario);
			if (formulario == null)
			{
				return NotFound();
			}
			return Ok(formulario);
		}

		[HttpPost]
		public async Task<ActionResult<FormularioModel>> CriarFormulario([FromBody] FormularioModel formulario)
		{
			var novoFormulario = await _formularioService.CriarFormulario(formulario);
			return CreatedAtAction(nameof(ObterFormularioPorId), new { codFormulario = novoFormulario.codFormulario }, novoFormulario);
		}

		[HttpPut("{codFormulario}")]
		public async Task<ActionResult<FormularioModel>> AtualizarFormulario(int codFormulario, [FromBody] FormularioModel formulario)
		{
			if (codFormulario != formulario.codFormulario)
			{
				return BadRequest();
			}
			var formularioAtualizado = await _formularioService.AtualizarFormulario(formulario);
			return Ok(formularioAtualizado);
		}

		[HttpDelete("{codFormulario}")]
		public async Task<ActionResult> DesativarFormulario(int codFormulario)
		{
			await _formularioService.DesativarFormulario(codFormulario);
			return NoContent();
		}
	}
}