using Tournaments.Domain.Models;
using Tournaments.Domain.Exceptions;

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
		Task<TournamentModel?> GetTournamentByIdAsync(long id);

		/// <summary>
		/// Asynchronously gets list of all tournament models.
		/// </summary>
		/// <returns>IEnumerable of TournamentModel</returns>
		Task<IEnumerable<TournamentModel>> GetTournamentsAsync();

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
		/// <param name="tournamentId">Tournament id</param>
		/// <returns>Result of adding. True if entity successfully updated in the database.</returns>
		Task<bool> UpdateTournamentAsync(TournamentModel tournament, long tournamentId);

		/// <summary>
		/// Asynchronously remove tournament from the database by id.
		/// </summary>
		/// <param name="id">Tournament id</param>
		/// <returns>Result of removing. True if entity successfully removed from the database.</returns>
		Task<bool> DeleteTournamentAsync(long id);
	}
}
