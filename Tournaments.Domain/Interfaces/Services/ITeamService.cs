using Tournaments.Domain.Entities;
using Tournaments.Domain.Exceptions;
using Tournaments.Domain.Models.TeamModels;
using Tournaments.Domain.Models.TournamentModels;

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
		Task<TeamModel> GetTeamByIdAsync(long id);

		/// <summary>
		/// Asynchronously gets all teams.
		/// </summary>
		/// <returns>IEnumerable of TeamModel entities</returns>
		Task<IEnumerable<TeamWithIdModel>> GetTeamsAsync();

		/// <summary>
		/// Asynchronously gets players from the team.
		/// </summary>
		/// <param name="teamId">Team id</param>
		/// <returns>IEnumerable of AppUser entities</returns>
		Task<IEnumerable<AppUser>> GetTeamPlayersAsync(long teamId);

		/// <summary>
		/// Asynchronously gets a list of all tournaments which team participating.
		/// </summary>
		/// <param name="teamId">Team id</param>
		/// <returns>IEnumerable of TournamentModel entities</returns>
		Task<IEnumerable<TournamentWithIdModel>> GetTournamentsAsync(long teamId);

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
		/// <exception cref="NotFoundException"></exception>
		Task UpdateTeamAsync(TeamWithIdModel team);

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
		/// <param name="model">AddPlayerModel model</param>
		/// <returns>Result adding. True if player successfully added to the team.</returns>
		/// <exception cref="NotFoundException"></exception>
		Task<bool> AddPlayerToTeamAsync(TeamMemberUpdateModel model);

		/// <summary>
		/// Asynchronously remove player from team.
		/// </summary>
		/// <param name="model">TeamMemberUpdateModel model</param>
		/// <returns>Result removing. True if player successfully removed from the team.</returns>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="NoContentException"></exception>
		Task<bool> RemovePlayerFromTeamAsync(TeamMemberUpdateModel model);

		/// <summary>
		/// Asynchronously register team for tournament.
		/// </summary>
		/// <param name="model">RegisterForTournamentModel model</param>
		/// <returns>Result registering. True if team successfully registered for tournament.</returns>
		/// <exception cref="NotFoundException"></exception>
		Task<bool> RegisterTeamForTournamentAsync(RegisterForTournamentModel model);
	}
}
