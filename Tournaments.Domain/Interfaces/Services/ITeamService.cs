using Tournaments.Domain.Entities;
using Tournaments.Domain.Exceptions;
using Tournaments.Domain.Models;

namespace Tournaments.Domain.Interfaces.Services
{
	public interface ITeamService
	{
		/// <summary>
		/// Asynchronously gets team by id.
		/// </summary>
		/// <param name="id">Team id</param>
		/// <returns>Team model which has this id</returns>
		/// <exception cref="NotFoundException"></exception>
		Task<TeamModel?> GetTeamByIdAsync(long id);

		/// <summary>
		/// Asynchronously gets a list of all tournaments which team participating.
		/// </summary>
		/// <param name="teamId">Team id</param>
		/// <returns>IEnumerable of TournamentModel entities</returns>
		Task<IEnumerable<TournamentModel>> GetTournamentsAsync(long teamId);

		/// <summary>
		/// Asynchronously create team.
		/// </summary>
		/// <param name="team">Team model</param>
		/// <returns>Result of creating. True if team successfully created.</returns>
		Task<bool> CreateTeamAsync(TeamModel team);

		/// <summary>
		/// Asynchronously update existing team.
		/// </summary>
		/// <param name="team">Team model</param>
		/// <returns>Result of updating. True if team successfully updated.</returns>
		/// <exception cref="NotFoundException"></exception>
		Task<bool> UpdateTeamAsync(TeamModel team);

		/// <summary>
		/// Asynchronously remove team by id.
		/// </summary>
		/// <param name="id">Team id</param>
		/// <returns>Result of removing. True if team successfully removed.</returns>
		/// <exception cref="NoContentException"></exception>
		Task<bool> DeleteTeamAsync(long id);

		/// <summary>
		/// Asynchronously add player to team.
		/// </summary>
		/// <param name="teamId">Team id</param>
		/// <param name="playerId">Player id</param>
		/// <returns>Result adding. True if player successfully added to the team.</returns>
		/// <exception cref="NotFoundException"></exception>
		Task<bool> AddPlayerToTeamAsync(long teamId, long playerId);

		/// <summary>
		/// Asynchronously remove player from team.
		/// </summary>
		/// <param name="teamId">Team id</param>
		/// <param name="playerId">Player id</param>
		/// <returns>Result removing. True if player successfully removed from the team.</returns>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="NoContentException"></exception>
		Task<bool> RemovePlayerFromTeamAsync(long teamId, long playerId);

		/// <summary>
		/// Asynchronously register team for tournament.
		/// </summary>
		/// <param name="teamId">Team id</param>
		/// <param name="tournamentId">Tournament id</param>
		/// <returns>Result registring. True if team successfully registred for tournament.</returns>
		/// <exception cref="NotFoundException"></exception>
		Task<bool> RegisterTeamForTournamentAsync(long teamId, long tournamentId);
	}
}
