using Microsoft.AspNetCore.Mvc;
using Tournaments.Domain.Services;
using Tournaments.Domain.Models;
using FluentValidation;
using Tournaments.Domain.Validators;

namespace Tournaments.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		private readonly IValidator<LoginModel> _loginValidator;
		private readonly IValidator<RegisterModel> _registerValidator;

		public AuthController(IAuthService authService, 
			IValidator<LoginModel> loginValidator,
			IValidator<RegisterModel> registerValidator)
        {
            _authService = authService;
			_loginValidator = loginValidator;
			_registerValidator = registerValidator;
        }

        [HttpPost("Login")]
		public async Task<IActionResult> Login(LoginModel model)
		{
			var validationResult = _loginValidator.Validate(model);

			if (!validationResult.IsValid)
				return BadRequest(validationResult.Errors);

			var result = await _authService.Login(model);

			if (result.NotFound)
				return NotFound("User doesn't exist");

			if (result.Success)
			{
				var jwtToken = _authService.GenerateToken(model);
				return Ok(jwtToken);
			}

			return BadRequest("Something went wrong");
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register(RegisterModel model)
		{
			var validationResult = _registerValidator.Validate(model);

			if (!validationResult.IsValid)
				return BadRequest(validationResult.Errors);

			var result = await _authService.Register(model);

			if(result)
				return Created();
			return StatusCode(500, "Couldn't register");
		}
	}
}
