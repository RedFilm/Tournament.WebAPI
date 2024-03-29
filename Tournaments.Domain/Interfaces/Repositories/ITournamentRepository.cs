﻿using Tournaments.Domain.Entities;

namespace Tournaments.Domain.Interfaces.Repositories
{
	public interface ITournamentRepository
    {
		/// <summary>
		/// Asynchronously gets tournament from the database by id.
		/// </summary>
		/// <param name="id">Tournament id</param>
		/// <returns>Tournament entity from the database which has this id</returns>
		Task<Tournament?> GetTournamentByIdAsync(long id);

		/// <summary>
		/// Asynchronously gets list of all tournaments.
		/// </summary>
		/// <returns>IEnumerable of Tournament entities</returns>
		Task<IEnumerable<Tournament>> GetTournamentsAsync();

		/// <summary>
		/// Asynchronously gets a list of all teams participating in the tournament.
		/// </summary>
		/// <param name="tournamentId">Tournament id</param>
		/// <returns>IEnumerable of Team entities</returns>
		Task<IEnumerable<Team>> GetTeamsAsync(long tournamentId);

		/// <summary>
		/// Asynchronously adds tournament entity to the database.
		/// </summary>
		/// <param name="tournament">Tournament entity</param>
		/// <returns>Result of adding. True if entity successfully added to the database.</returns>
		Task<bool> AddTournamentAsync(Tournament tournament);

		/// <summary>
		/// Asynchronously update existing tournament in the database.
		/// </summary>
		/// <param name="tournament">Tournament entity</param>
		/// <returns>Result of updating. True if entity successfully updated in the database.</returns>
		Task<bool> UpdateTournamentAsync(Tournament tournament);

		/// <summary>
		/// Asynchronously remove tournament from the database by id.
		/// </summary>
		/// <param name="id">Tournament id</param>
		/// <returns>Result of removing. True if entity successfully removed from the database.</returns>
		Task<bool> DeleteTournamentAsync(long id);

		/// <summary>
		/// Asynchronously checks if tournament exists.
		/// </summary>
		/// <param name="tournamentId">Tournament id</param>
		/// <returns>Result of checking. True if tournament exists</returns>
		Task<bool> AnyAsync(long tournamentId);
	}
}
