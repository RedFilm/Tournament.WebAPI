using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
		public async Task<UserModel> GetUser(long userId)
		{
			var user = await _userService.GetUserAsync(userId);
			return Ok(user);
		}

		[HttpGet]
		public async Task<IEnumerable<UserModel>> GetUsers([FromQuery] int pageNumber, [FromQuery] int pageSize)
		{
			return await _userService.GetUsersAsync(pageNumber, pageSize);
		}

		[HttpDelete("{userId}")]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> DeleteUser(string userId)
		{
			await _userService.DeleteUserAsync(userId);
			return Ok();
		}
	}
}
