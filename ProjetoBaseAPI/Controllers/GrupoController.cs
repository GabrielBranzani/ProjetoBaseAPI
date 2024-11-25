using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoBaseAPI.Models;
using ProjetoBaseAPI.Services;

namespace ProjetoBaseAPI.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class GrupoController : ControllerBase
	{
		private readonly GrupoService _grupoService;

		public GrupoController(GrupoService grupoService)
		{
			_grupoService = grupoService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GrupoModel>>> ObterTodosGrupos()
		{
			var grupos = await _grupoService.ObterTodosGrupos();
			return Ok(grupos);
		}

		[HttpGet("{codGrupoUsuario}")]
		public async Task<ActionResult<GrupoModel>> ObterGrupoPorId(int codGrupoUsuario)
		{
			var grupo = await _grupoService.ObterGrupoPorId(codGrupoUsuario);
			if (grupo == null)
			{
				return NotFound();
			}
			return Ok(grupo);
		}

		[HttpPost]
		public async Task<ActionResult<GrupoModel>> CriarGrupo([FromBody] GrupoModel grupo)
		{
			var novoGrupo = await _grupoService.CriarGrupo(grupo);
			return CreatedAtAction(nameof(ObterGrupoPorId), new { codGrupoUsuario = novoGrupo.codGrupoUsuario }, novoGrupo);
		}

		[HttpPut("{codGrupoUsuario}")]
		public async Task<ActionResult<GrupoModel>> AtualizarGrupo(int codGrupoUsuario, [FromBody] GrupoModel grupo)
		{
			if (codGrupoUsuario != grupo.codGrupoUsuario)
			{
				return BadRequest();
			}
			var grupoAtualizado = await _grupoService.AtualizarGrupo(grupo);
			return Ok(grupoAtualizado);
		}

		[HttpDelete("{codGrupoUsuario}")]
		public async Task<ActionResult> DesativarGrupo(int codGrupoUsuario)
		{
			await _grupoService.DesativarGrupo(codGrupoUsuario);
			return NoContent();
		}
	}
}