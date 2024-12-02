using Microsoft.AspNetCore.Mvc;
using ProjetoBaseAPI.Data.Repositories;
using ProjetoBaseAPI.Models.Gerenciador;

namespace ProjetoBaseAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsuarioController : ControllerBase
	{
		private readonly UsuarioRepository _repository;

		public UsuarioController(UsuarioRepository repository)
		{
			_repository = repository;
		}

		[HttpPost]
		public async Task<IActionResult> Inserir(UsuarioModel usuario)
		{
			// Gera o hash da senha antes de inserir no banco
			usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuario.SenhaHash);

			var novoUsuario = await _repository.Inserir(usuario);
			return CreatedAtAction(nameof(ConsultarPorId), new { codUsuario = novoUsuario.codUsuario }, novoUsuario);
		}

		[HttpGet("{codUsuario}")]
		public async Task<IActionResult> ConsultarPorId(int codUsuario)
		{
			var usuario = await _repository.ConsultarPorId(codUsuario);
			return usuario == null ? NotFound() : Ok(usuario);
		}

		[HttpGet]
		public async Task<IActionResult> ConsultarTodos()
		{
			var usuarios = await _repository.ConsultarTodos();
			return Ok(usuarios);
		}

		[HttpPut("{codUsuario}")]
		public async Task<IActionResult> Atualizar(int codUsuario, UsuarioModel usuario)
		{
			if (codUsuario != usuario.codUsuario)
			{
				return BadRequest();
			}

			// Gera o hash da senha antes de atualizar no banco
			usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuario.SenhaHash);

			var usuarioAtualizado = await _repository.Atualizar(usuario);
			return usuarioAtualizado == null ? NotFound() : Ok(usuarioAtualizado);
		}

		[HttpDelete("{codUsuario}")]
		public async Task<IActionResult> Desativar(int codUsuario)
		{
			var usuarioDesativado = await _repository.Desativar(codUsuario);
			return usuarioDesativado == null ? NotFound() : Ok(usuarioDesativado);
		}
	}
}