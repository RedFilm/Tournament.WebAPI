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
	}
}
