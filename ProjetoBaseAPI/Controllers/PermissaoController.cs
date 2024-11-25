using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoBaseAPI.Models.Permissoes;
using ProjetoBaseAPI.Services;

namespace ProjetoBaseAPI.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class PermissaoController : ControllerBase
	{
		private readonly PermissaoService _permissaoService;

		public PermissaoController(PermissaoService permissaoService)
		{
			_permissaoService = permissaoService; 1

		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<PermissaoModel>>> ObterTodasPermissoes()
		{
			var permissoes = await _permissaoService.ObterTodasPermissoes();
			return Ok(permissoes);
		}

		[HttpGet("{codPermissao}")]
		public async Task<ActionResult<PermissaoModel>> ObterPermissaoPorId(int codPermissao)
		{
			var permissao = await _permissaoService.ObterPermissaoPorId(codPermissao);
			if (permissao == null)
			{
				return NotFound();
			}
			return Ok(permissao);
		}

		[HttpPost]
		public async Task<ActionResult<PermissaoModel>> CriarPermissao([FromBody] PermissaoModel permissao)
		{
			var novaPermissao = await _permissaoService.CriarPermissao(permissao);
			return CreatedAtAction(nameof(ObterPermissaoPorId), new { codPermissao = novaPermissao.codPermissao }, novaPermissao);
		}

		[HttpPut("{codPermissao}")]
		public async Task<ActionResult<PermissaoModel>> AtualizarPermissao(int codPermissao, [FromBody] PermissaoModel permissao)
		{
			if (codPermissao != permissao.codPermissao)
			{
				return BadRequest();
			}
			var permissaoAtualizada = await _permissaoService.AtualizarPermissao(permissao);
			return Ok(permissaoAtualizada);
		}

		[HttpDelete("{codPermissao}")]
		public async Task<ActionResult> DesativarPermissao(int codPermissao)
		{
			await _permissaoService.DesativarPermissao(codPermissao);
			return NoContent();
		}
	}
}