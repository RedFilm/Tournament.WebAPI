using Tournaments.Domain.Entities;

namespace Tournaments.Domain.Interfaces.Repositories
{
	public interface ITeamRepository
	{
		/// <summary>
		/// Asynchronously gets team from the database by id.
		/// </summary>
		/// <param name="id">Team id</param>
		/// <returns>Team entity from the database which has this id</returns>
		Task<Team?> GetTeamAsync(long id);

		/// <summary>
		/// Asynchronously gets a list of all teams participating in the tournament.
		/// </summary>
		/// <param name="tournamentId">Tournament id</param>
		/// <returns>IEnumerable of Team entities</returns>
		Task<IEnumerable<Team>> GetTeamsAsync(long tournamentId);

		/// <summary>
		/// Asynchronously adds team entity to the database.
		/// </summary>
		/// <param name="team">Team entity</param>
		/// <returns>Result of adding. True if entity successfully added to the database.</returns>
		Task<bool> AddTeamAsync(Team team);

		/// <summary>
		/// Asynchronously update existing team in the database.
		/// </summary>
		/// <param name="team">Team entity</param>
		/// <returns>Result of updating. True if entity successfully updated in the database.</returns>
		Task<bool> UpdateTeamAsync(Team team);

		/// <summary>
		/// Asynchronously remove team from the database by id.
		/// </summary>
		/// <param name="id">Team id</param>
		/// <returns>Result of removing. True if entity successfully removed from the database.</returns>
		Task<bool> DeleteTeamAsync(long id);
	}
}
