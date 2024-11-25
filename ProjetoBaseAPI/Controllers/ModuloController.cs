using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoBaseAPI.Models.Menu;
using ProjetoBaseAPI.Services;

namespace ProjetoBaseAPI.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class ModuloController : ControllerBase
	{
		private readonly ModuloService _moduloService;

		public ModuloController(ModuloService moduloService)
		{
			_moduloService = moduloService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ModuloModel>>> ObterTodosModulos()
		{
			var modulos = await _moduloService.ObterTodosModulos();
			return Ok(modulos);
		}

		[HttpGet("{codModulo}")]
		public async Task<ActionResult<ModuloModel>> ObterModuloPorId(int codModulo)
		{
			var modulo = await _moduloService.ObterModuloPorId(codModulo);
			if (modulo == null)
			{
				return NotFound();
			}
			return Ok(modulo);
		}

		[HttpPost]
		public async Task<ActionResult<ModuloModel>> CriarModulo([FromBody] ModuloModel modulo)
		{
			var novoModulo = await _moduloService.CriarModulo(modulo);
			return CreatedAtAction(nameof(ObterModuloPorId), new { codModulo = novoModulo.codModulo }, novoModulo);
		}

		[HttpPut("{codModulo}")]
		public async Task<ActionResult<ModuloModel>> AtualizarModulo(int codModulo, [FromBody] ModuloModel modulo)
		{
			if (codModulo != modulo.codModulo)
			{
				return BadRequest();
			}
			var moduloAtualizado = await _moduloService.AtualizarModulo(modulo);
			return Ok(moduloAtualizado);
		}

		[HttpDelete("{codModulo}")]
		public async Task<ActionResult> DesativarModulo(int codModulo)
		{
			await _moduloService.DesativarModulo(codModulo);
			return NoContent();
		}
	}
}