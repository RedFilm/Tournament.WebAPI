using Microsoft.AspNetCore.Mvc;
using Tournaments.Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using Tournaments.Domain.Interfaces.Services;

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
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResultModel))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> Login(LoginModel model)
		{
			await _loginValidator.ValidateAndThrowAsync(model);

			var result = await _authService.LoginAsync(model);

			return Ok(result);
		}

		[HttpPost("Register")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> Register(RegisterModel model)
		{
			await _registerValidator.ValidateAndThrowAsync(model);

			await _authService.RegisterAsync(model);

			return Ok();
		}
	}
}
