using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Tournaments.Domain.Models;
using Tournaments.Domain.Services;
using Tournaments.Domain.ViewModels;

namespace Tournaments.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private IAuthService _authService;

		public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Login")]
		public async Task<IActionResult> Login(LoginViewModel vm)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _authService.Login(vm);

			if (result)
			{
				var jwtToken = _authService.GenerateToken(vm);
				return Ok(jwtToken);
			}
			return BadRequest("Something went wrong");
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register(LoginViewModel vm)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _authService.Register(vm);

			if(result)
				return Ok("Done");
			return BadRequest("Something went wrong");
		}
	}
}
