using Tournaments.Domain.Exceptions;
using Tournaments.Domain.Models.BracketModels;
using Tournaments.Domain.Models.TeamModels;
using Tournaments.Domain.Models.TournamentModels;

namespace Tournaments.Domain.Interfaces.Services
{
	public interface ITournamentService
	{
		/// <summary>
		/// Asynchronously gets model of tournament by id.
		/// </summary>
		/// <param name="id">Tournament id</param>
		/// <returns>Tournament model which has this id</returns>
		/// <exception cref="NotFoundException"></exception>
		Task<TournamentModel> GetTournamentByIdAsync(long id);

		/// <summary>
		/// Asynchronously gets list of all tournament models.
		/// </summary>
		/// <returns>IEnumerable of TournamentModel</returns>
		Task<IEnumerable<TournamentWithIdModel>> GetTournamentsAsync();

		/// <summary>
		/// Asynchronously adds tournament to the database.
		/// </summary>
		/// <param name="tournament">Tournament model</param>
		/// <returns>Result of adding. True if entity successfully added to the database.</returns>
		Task<bool> AddTournamentAsync(TournamentModel tournament);

		/// <summary>
		/// Asynchronously update existing tournament in the database.
		/// </summary>
		/// <param name="tournament">Tournament model</param>
		/// <exception cref="NotFoundException"></exception>
		Task UpdateTournamentAsync(TournamentWithIdModel tournament);

		/// <summary>
		/// Asynchronously remove tournament from the database by id.
		/// </summary>
		/// <param name="id">Tournament id</param>
		/// <returns>Result of removing. True if entity successfully removed from the database.</returns>
		/// <exception cref="NoContentException"></exception>
		Task<bool> DeleteTournamentAsync(long id);

		/// <summary>
		/// Asynchronously gets a list of all teams participating in the tournament.
		/// </summary>
		/// <param name="tournamentId">Tournament id</param>
		/// <returns>IEnumerable of Team models</returns>
		/// <exception cref="NotFoundException"></exception>
		Task<IEnumerable<TeamWithIdModel>> GetTeamsAsync(long tournamentId);

		/// <summary>
		/// Asynchronously get bracket by id.
		/// </summary>
		/// <param name="bracketId">Bracket id</param>
		/// <returns>BracketModel which has this id</returns>
		/// <exception cref="NotFoundException"></exception>
		Task<BracketModel> GetBracketAsync(long tournamentId);

		/// <summary>
		/// Asynchronously generate new bracket. If the bracket already exists, replace it with a new one.
		/// </summary>
		/// <param name="tournamentId">Tournament id</param>
		/// <returns>New bracket model</returns>
		Task<BracketModel> GenerateNewBracketAsync(long tournamentId);

		/// <summary>
		/// Asynchronously update existing bracket based on the results of the matches.
		/// </summary>
		/// <param name="bracketModel">BracketModel</param>
		/// <returns>Updated bracket model</returns>
		/// <exception cref="NotFoundException"></exception>
		Task<BracketModel> UpdateBracketAsync(BracketUpdateModel bracketModel);
	}
}
