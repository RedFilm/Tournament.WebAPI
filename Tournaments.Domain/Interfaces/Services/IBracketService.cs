using Tournaments.Domain.Models;
using Tournaments.Domain.Models.BracketModels;

namespace Tournaments.Domain.Interfaces.Services
{
	public interface IBracketService
	{
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
		/// <param name="shuffleTeams">Flag indicating whether the commands need to be shuffled</param>
		/// <returns>New bracket model</returns>
		Task<BracketModel> GenerateNewBracketAsync(long tournamentId, bool shuffleTeams);

		/// <summary>
		/// Asynchronously update existing bracket based on the results of the matches.
		/// </summary>
		/// <param name="results">List of matches results</param>
		/// <param name="tournamentId">Tournament id</param>
		/// <returns>Updated bracket model</returns>
		/// <exception cref="NotFoundException"></exception>
		Task<BracketModel> UpdateBracketAsync(IEnumerable<MatchResultModel> results, long tournamentId);
	}
}
