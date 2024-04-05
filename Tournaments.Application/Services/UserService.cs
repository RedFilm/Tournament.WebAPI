using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Exceptions;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;

namespace Tournaments.Application.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IMapper _mapper;

		public UserService(UserManager<AppUser> userManager, IMapper mapper)
		{
			_userManager = userManager;
			_mapper = mapper;
		}

		public async Task<UserModel> GetUserByIdAsync(long userId)
		{
			var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

			if (user is null)
				throw new NotFoundException("User doesn't exist");

			return _mapper.Map<UserModel>(user);
		}

		public async Task<IEnumerable<UserModel>> GetUsersAsync(int pageNumber, int pageSize)
		{
			if (pageNumber < 0 || pageSize < 0)
				throw new BadRequestException("Page number and size must be non negative");
			int skipCount = (pageNumber - 1) * pageSize;

			var users = await _userManager.Users.Skip(skipCount).Take(pageSize).ToListAsync();
			return _mapper.Map<IEnumerable<UserModel>>(users);
		}

		public async Task<bool> DeleteUserByIdAsync(long userId)
		{
			var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);

			if (user is null)
				throw new NoContentException("User's already been removed");

			var result = await _userManager.DeleteAsync(user);

			return result.Succeeded;
		}
	}
}
