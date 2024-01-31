using Microsoft.AspNetCore.Mvc;
using Tournaments.Domain.Services;
using Tournaments.Domain.Models;

namespace Tournaments.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
		public async Task<IActionResult> Login(LoginModel model)
		{
			var result = await _authService.Login(model);

			if (result)
			{
				var jwtToken = _authService.GenerateToken(model);
				return Ok(jwtToken);
			}
			return BadRequest("Something went wrong");
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register(RegisterModel model)
		{
			var result = await _authService.Register(model);

			if(result)
				return Ok("Done");
			return BadRequest("Something went wrong");
		}
	}
}
