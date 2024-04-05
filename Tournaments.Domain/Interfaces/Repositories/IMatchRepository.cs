using Tournaments.Domain.Entities;

namespace Tournaments.Domain.Interfaces.Repositories
{
	public interface IMatchRepository
	{
		/// <summary>
		/// Asynchronously gets match by id.
		/// </summary>
		/// <param name="matchId">Match id</param>
		/// <returns>Match entity which has this id</returns>
		Task<Match?> GetMatchByIdAsync(long matchId);

		/// <summary>
		/// Asynchronously gets all matches are in the stage.
		/// </summary>
		/// <param name="stageId">Stage id</param>
		/// <returns>IEnumerable of Match entities</returns>
		Task<List<Match>> GetMatchesAsync(long stageId);
	}
}
