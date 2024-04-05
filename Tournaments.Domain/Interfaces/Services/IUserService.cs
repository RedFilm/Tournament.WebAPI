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
		/// <param name="pageNumber">Number of the required page</param>
		/// <param name="pageSize">Users count on the page</param>
		/// <returns>IEnumerable of UserModel entities</returns>
		public Task<IEnumerable<UserModel>> GetUsersAsync(int pageNumber, int pageSize);

		/// <summary>
		/// Asynchronously remove user by id.
		/// </summary>
		/// <param name="userId">User id</param>
		/// <returns>Result of removing. True if user successfully removed.</returns>
		public Task<bool> DeleteUserByIdAsync(long userId);

	}
}
