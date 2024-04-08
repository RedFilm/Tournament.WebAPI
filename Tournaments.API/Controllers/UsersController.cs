using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournaments.Domain.Enums;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;

namespace Tournaments.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;

		public UsersController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet("{userId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResponseModel))]
		public async Task<UserModel> GetUser(long userId)
		{
			return await _userService.GetUserByIdAsync(userId);
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageModel<UserModel>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		public async Task<PageModel<UserModel>> GetUsers([FromQuery] PaginationModel model)
		{
			return await _userService.GetUsersAsync(model);
		}

		[Authorize(Roles = nameof(IdentityRoles.Administrator))]
		[HttpDelete("{userId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> DeleteUser(long userId)
		{
			await _userService.DeleteUserByIdAsync(userId);
			return Ok();
		}
	}
}
