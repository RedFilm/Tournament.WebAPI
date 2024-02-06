using Microsoft.AspNetCore.Mvc;
using Tournaments.Domain.Services;
using Tournaments.Domain.Models;
using FluentValidation;
using Tournaments.Domain.Validators;
using FluentValidation.Results;

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
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Login(LoginModel model)
		{
			var validationResult = await _loginValidator.ValidateAsync(model);

			if (!validationResult.IsValid)
				return BadRequest(validationResult.Errors);

			var result = await _authService.LoginAsync(model);

			if (result.NotFound)
				return NotFound();

			if (result.Success)
			{
				var jwtToken = _authService.GenerateToken(model);
				return Ok(jwtToken);
			}

			return BadRequest();
		}

		[HttpPost("Register")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ValidationFailure>))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Register(RegisterModel model)
		{
			var validationResult = await _registerValidator.ValidateAsync(model);

			if (!validationResult.IsValid)
				return BadRequest(validationResult.Errors);

			var result = await _authService.RegisterAsync(model);

			if(result)
				return Created();
			return StatusCode(500, "Couldn't register");
		}
	}
}
