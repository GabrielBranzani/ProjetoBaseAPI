using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoBaseAPI.Models.Menu;
using ProjetoBaseAPI.Services;

namespace ProjetoBaseAPI.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class MenuUsuarioController : ControllerBase
	{
		private readonly MenuUsuarioService _menuUsuarioService;

		public MenuUsuarioController(MenuUsuarioService menuUsuarioService)
		{
			_menuUsuarioService = menuUsuarioService;
		}

		[HttpGet("{codUsuario}")]
		public async Task<ActionResult<IEnumerable<MenuUsuarioModel>>> ObterMenusDoUsuario(int codUsuario)
		{
			var menus = await _menuUsuarioService.ObterMenusDoUsuario(codUsuario);
			return Ok(menus);
		}
	}
}