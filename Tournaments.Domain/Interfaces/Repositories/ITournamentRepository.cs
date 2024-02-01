using Tournaments.Domain.Entities;
using Tournaments.Domain.Models;

namespace Tournaments.Domain.Interfaces.Repositories
{
    public interface ITournamentRepository
    {
		/// <summary>
		/// Asynchronously gets tournament from the database by id.
		/// </summary>
		/// <param name="id">Tournament id</param>
		/// <returns>Tournament entity from the database wich contains the this id</returns>
		Task<Tournament?> GetTournamentAsync(int id);

		/// <summary>
		/// Asynchronously gets list of all tournaments.
		/// </summary>
		/// <returns>IEnumerable of Tournament entities</returns>
		Task<IEnumerable<Tournament>> GetTournamentsAsync();

		/// <summary>
		/// Asynchronously adds tournament entity to the database.
		/// </summary>
		/// <param name="tournament">Tournament entity</param>
		/// <returns>Result of adding. True if entity successfully added to the database.</returns>
		Task<bool> AddTournamentAsync(TournamentModel tournament);

		/// <summary>
		/// Asynchronously update existing tournament in the database.
		/// </summary>
		/// <param name="tournament">Tournament entity</param>
		/// <returns>Result of adding. True if entity successfully updated in the database.</returns>
		Task<bool> UpdateTournamentAsync(TournamentModel tournament);

		/// <summary>
		/// Asynchronously remove tournament from the database by id.
		/// </summary>
		/// <param name="id">Tournament id</param>
		/// <returns>Result of removing. True if entity successfully removed from the database.</returns>
		Task<bool> DeleteTournamentAsync(int id);
    }
}
