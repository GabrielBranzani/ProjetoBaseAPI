using Microsoft.AspNetCore.Mvc;
using ProjetoBaseAPI.Models.Auth;
using ProjetoBaseAPI.Services;

namespace ProjetoBaseAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly AuthService _authService;

		public AuthController(AuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("login")]
		public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
		{
			try
			{
				var loginResponse = await _authService.Login(loginRequest);
				return Ok(loginResponse);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("refresh-token")]
		public async Task<ActionResult<LoginResponse>> RefreshToken([FromBody] string refreshToken)
		{
			try
			{
				var loginResponse = await _authService.RefreshToken(refreshToken);
				return Ok(loginResponse);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}