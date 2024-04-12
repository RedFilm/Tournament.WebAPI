using Tournaments.Domain.Entities;
using Tournaments.Domain.Models;

namespace Tournaments.Domain.Interfaces.Repositories
{
	public interface ITeamJoinRequestRepository
	{
		/// <summary>
		/// Asynchronously adds join request to database.
		/// </summary>
		/// <returns>Result of adding. True if request successfully added</returns>
		public Task<bool> AddJoinRequestAsync(TeamJoinRequest joinRequest);

		/// <summary>
		/// Asynchronously update existing request in the database.
		/// </summary>
		/// <param name="request">TeamJoinRequest entity</param>
		/// <returns>Result of updating. True if entity successfully updated in the database.</returns>
		Task<bool> UpdateRequestAsync(TeamJoinRequest request);

		/// <summary>
		/// Asynchronously gets join request by id.
		/// </summary>
		/// <param name="requestId">Join request id</param>
		/// <returns>Join request entity which has this id</returns>
		Task<TeamJoinRequest?> GetRequestByIdAsync(long requestId);

		/// <summary>
		/// Asynchronously gets all join requests.
		/// </summary>
		/// <param name="model">Model that contains page number and size</param>
		/// <returns>List of TeamJoinRequest entities</returns>
		Task<List<TeamJoinRequest>> GetAllAsync(PaginationModel model);

		/// <summary>
		/// Gets total join requests count.
		/// </summary>
		/// <returns>Join requests count</returns>
		Task<int> GetTotalCountAsync();

		/// <summary>
		/// Asynchronously checks if join request exists.
		/// </summary>
		/// <param name="senderId">User id</param>
		/// <param name="teamId">Team id</param>
		/// <returns>Result of checking. True if request exists</returns>
		public Task<bool> AnyAsync(long senderId, long teamId);
	}
}
