using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoBaseAPI.Models;
using ProjetoBaseAPI.Services;

namespace ProjetoBaseAPI.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class UsuariosController : ControllerBase
	{
		private readonly IUsuarioService _usuarioService;

		public UsuariosController(IUsuarioService usuarioService)
		{
			_usuarioService = usuarioService;
		}

		[HttpPost]
		public async Task<IActionResult> CriarUsuario(UsuariosModel usuario)
		{
			var novoUsuario = await _usuarioService.CriarUsuario(usuario);
			return CreatedAtAction(nameof(ObterUsuarioPorId), new { codUsuario = novoUsuario.codUsuario }, novoUsuario);
		}

		[HttpGet("{codUsuario}")]
		public async Task<IActionResult> ObterUsuarioPorId(int codUsuario)
		{
			var usuario = await _usuarioService.ObterUsuarioPorId(codUsuario);
			if (usuario == null)
			{
				return NotFound();
			}
			return Ok(usuario);
		}

		[HttpGet]
		public async Task<IActionResult> ObterTodosUsuarios()
		{
			var usuarios = await _usuarioService.ObterTodosUsuarios();
			return Ok(usuarios);
		}

		[HttpPut("{codUsuario}")]
		public async Task<IActionResult> AtualizarUsuario(int codUsuario, UsuariosModel usuario)
		{
			if (codUsuario != usuario.codUsuario)
			{
				return BadRequest();
			}

			var usuarioAtualizado = await _usuarioService.AtualizarUsuario(usuario);
			if (usuarioAtualizado == null)
			{
				return NotFound();
			}
			return Ok(usuarioAtualizado);
		}

		[HttpDelete("{codUsuario}")]
		public async Task<IActionResult> DesativarUsuario(int codUsuario)
		{
			var usuarioDesativado = await _usuarioService.DesativarUsuario(codUsuario);
			if (usuarioDesativado == null)
			{
				return NotFound();
			}
			return Ok(usuarioDesativado);
		}
	}
}