using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoBaseAPI.Models.Menu;
using ProjetoBaseAPI.Services;

namespace ProjetoBaseAPI.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class MenuController : ControllerBase
	{
		private readonly MenuService _menuService;

		public MenuController(MenuService menuService)
		{
			_menuService = menuService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<MenuModel>>> ObterTodosMenus()
		{
			var menus = await _menuService.ObterTodosMenus();
			return Ok(menus);
		}

		[HttpGet("{codMenu}")]
		public async Task<ActionResult<MenuModel>> ObterMenuPorId(int codMenu)
		{
			var menu = await _menuService.ObterMenuPorId(codMenu);
			if (menu == null)
			{
				return NotFound();
			}
			return Ok(menu);
		}

		[HttpPost]
		public async Task<ActionResult<MenuModel>> CriarMenu([FromBody] MenuModel menu)
		{
			var novoMenu = await _menuService.CriarMenu(menu);
			return CreatedAtAction(nameof(ObterMenuPorId), new { codMenu = novoMenu.codMenu }, novoMenu);
		}

		[HttpPut("{codMenu}")]
		public async Task<ActionResult<MenuModel>> AtualizarMenu(int codMenu, [FromBody] MenuModel menu)
		{
			if (codMenu != menu.codMenu)
			{
				return BadRequest();
			}
			var menuAtualizado = await _menuService.AtualizarMenu(menu);
			return Ok(menuAtualizado);
		}

		[HttpDelete("{codMenu}")]
		public async Task<ActionResult> DesativarMenu(int codMenu)
		{
			await _menuService.DesativarMenu(codMenu);
			return NoContent();
		}
	}
}