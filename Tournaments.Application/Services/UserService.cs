using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Exceptions;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;

namespace Tournaments.Application.Services
{
	public class UserService : IUserService
	{
		private readonly INotificationRepository _notificationRepository;
		private readonly UserManager<AppUser> _userManager;
		private readonly IMapper _mapper;

		public UserService(INotificationRepository notificationRepository, UserManager<AppUser> userManager, IMapper mapper)
		{
			_notificationRepository = notificationRepository;
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

		public async Task<PageModel<UserModel>> GetUsersAsync(PaginationModel model)
		{
			var skipCount = (model.PageNumber - 1) * model.PageSize;

			var users = await _userManager.Users
				.Skip(skipCount).Take(model.PageSize).ToListAsync();
			var userModels = _mapper.Map<IEnumerable<UserModel>>(users);

			return new PageModel<UserModel>
			{
				PageNumber = model.PageNumber,
				PageSize = model.PageSize,
				Items = userModels,
				ItemsCount = await _userManager.Users.CountAsync()
			};
		}

		public async Task<bool> DeleteUserByIdAsync(long userId)
		{
			var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);

			if (user is null)
				throw new NoContentException("User's already been removed");

			var result = await _userManager.DeleteAsync(user);

			return result.Succeeded;
		}

		public async Task<PageModel<NotificationModel>> GetAllNotificationsAsync(PaginationModel model, long userId)
		{
			var notifications = await _notificationRepository.GetAllAsync(model, userId);

			var notificationModels = _mapper.Map<IEnumerable<NotificationModel>>(notifications);

			return new PageModel<NotificationModel>
			{
				PageNumber = model.PageNumber,
				PageSize = model.PageSize,
				Items = notificationModels,
				ItemsCount = await _notificationRepository.GetCountForUserAsync(userId, isReadOnly: false)
			};
		}

		public async Task<PageModel<NotificationModel>> GetUnreadNotificationsAsync(PaginationModel model, long userId)
		{
			var notifications = await _notificationRepository.GetUnreadAsync(model, userId);

			var notificationModels = _mapper.Map<IEnumerable<NotificationModel>>(notifications);

			return new PageModel<NotificationModel>
			{
				PageNumber = model.PageNumber,
				PageSize = model.PageSize,
				Items = notificationModels,
				ItemsCount = await _notificationRepository.GetCountForUserAsync(userId, isReadOnly: true)
			};
		}
	}
}
