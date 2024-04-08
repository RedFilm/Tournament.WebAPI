using Tournaments.Domain.Models.TeamJoinRequestModels;

namespace Tournaments.Domain.Interfaces.Repositories
{
	public interface ITeamJoinRequestRepository
	{
		/// <summary>
		/// Asynchronously adds join request to database.
		/// </summary>
		/// <returns>Result of adding. True if request successfully added</returns>
		public Task<bool> AddJoinRequestAsync(TeamJoinRequestModel model);
	}
}
