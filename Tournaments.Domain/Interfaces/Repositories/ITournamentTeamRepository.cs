using Tournaments.Domain.Entities;

namespace Tournaments.Domain.Interfaces.Repositories
{
	public interface ITournamentTeamRepository
	{

		/// <summary>
		/// Asynchronously gets players from the team participating in the tournament.
		/// </summary>
		/// <param name="tournamentId">Tournament id</param>
		/// <param name="teamId">Team id</param>
		/// <returns>List of AppUser entities</returns>
		Task<List<long>> GetTournamentTeamPlayerIdsAsync(long tournamentId, long teamId);

		/// <summary>
		/// Asynchronously checks if tournament contains this team.
		/// </summary>
		/// <param name="tournamentId">Tournament id</param>
		/// <param name="teamId">Team id</param>
		/// <param name="playerIds">List of participants</param>
		/// <returns>Result of checking. True if tournament already contains team.</returns>
		Task<bool> AnyAsync(long tournamentId, long teamId, IEnumerable<long> playerIds);
	}
}
