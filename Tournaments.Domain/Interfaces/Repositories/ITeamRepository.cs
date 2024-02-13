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
		/// Asynchronously gets a list of all tournaments which team participating.
		/// </summary>
		/// <param name="teamId">Team id</param>
		/// <returns>IEnumerable of Tournament entities</returns>
		Task<IEnumerable<Tournament?>> GetTournamentsAsync(long teamId);

		/// <summary>
		/// Asynchronously gets a list of all players in team.
		/// </summary>
		/// <param name="teamId">Team id</param>
		/// <returns>IEnumerable of AppUser entities</returns>
		Task<Team?> GetTeamPlayersAsync(long teamId);

		/// <summary>
		/// Asynchronously adds player to team.
		/// </summary>
		/// <param name="player">AppUser user</param>
		/// <returns>Result of adding. True if user successfully added to the team</returns>
		Task<bool> AddPlayerToTeamAsync(AppUser player, Team team);

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
