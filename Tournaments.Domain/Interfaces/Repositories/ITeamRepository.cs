using Tournaments.Domain.Entities;
using Tournaments.Domain.Models;

namespace Tournaments.Domain.Interfaces.Repositories
{
	public interface ITeamRepository
	{
		/// <summary>
		/// Asynchronously gets team from the database by id.
		/// </summary>
		/// <param name="id">Team id</param>
		/// <returns>Team entity from the database which has this id</returns>
		Task<Team?> GetTeamByIdAsync(long id);

		/// <summary>
		/// Asynchronously gets a list of all tournaments which team participating.
		/// </summary>
		/// <param name="teamId">Team id</param>
		/// <returns>IEnumerable of Tournament entities</returns>
		Task<IEnumerable<Tournament>> GetTournamentsAsync(long teamId);

		/// <summary>
		/// Asynchronously gets players from the team.
		/// </summary>
		/// <param name="teamId">Team id</param>
		/// <returns>All players from the team</returns>
		Task<IEnumerable<AppUser>> GetTeamPlayersAsync(long teamId);

		/// <summary>
		/// Asynchronously gets all teams.
		/// </summary>
		/// <returns>IEnumerable of Team entities</returns>
		Task<IEnumerable<Team>> GetTeamsAsync();

		/// <summary>
		/// Asynchronously adds player to team.
		/// </summary>
		/// <param name="player">AppUser user</param>
		/// <param name="team">Team entity</param>
		/// <returns>Result of adding. True if user successfully added to the team</returns>
		Task<bool> AddPlayerToTeamAsync(AppUser player, Team team);

		/// <summary>
		/// Asynchronously removes player from team.
		/// </summary>
		/// <param name="player">AppUser user</param>
		/// <param name="teamId">Team id</param>
		/// <returns>Result of removing. True if user successfully removed from the team</returns>
		Task<bool> RemovePlayerFromTeamAsync(AppUser player, long teamId);

		/// <summary>
		/// Asynchronously adds tournament to team.
		/// </summary>
		/// <param name="tournament">Tournament entity</param>
		/// <param name="team">Team entity</param>
		/// <returns>Result of adding. True if user successfully added to the team</returns>
		Task<bool> AddTeamToTournamentAsync(Tournament tournament, Team team);

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

		/// <summary>
		/// Asynchronously checks if team exists.
		/// </summary>
		/// <param name="teamId">Team id</param>
		/// <returns>Result of checking. True if team exists</returns>
		Task<bool> AnyAsync(long teamId);
	}
}
