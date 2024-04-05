using Tournaments.Domain.Models;

namespace Tournaments.Domain.Interfaces.Services
{
	public interface IUserService
	{
		/// <summary>
		/// Get user by id.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns>User model which has this id</returns>
		public Task<UserModel> GetUserByIdAsync(long userId);

		/// <summary>
		/// Get all users.
		/// </summary>
		/// <param name="model">Model that contains page number and size</param>
		/// <returns>Page model that contains list of users, total count, page number and size</returns>
		public Task<PageModel<UserModel>> GetUsersAsync(PaginationModel model);

		/// <summary>
		/// Asynchronously remove user by id.
		/// </summary>
		/// <param name="userId">User id</param>
		/// <returns>Result of removing. True if user successfully removed.</returns>
		public Task<bool> DeleteUserByIdAsync(long userId);

	}
}
