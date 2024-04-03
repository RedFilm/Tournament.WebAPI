using Tournaments.Domain.Exceptions;
using Tournaments.Domain.Models;

namespace Tournaments.Domain.Interfaces.Services
{
	public interface IMatchService
	{
		/// <summary>
		/// Asynchronously gets match by id.
		/// </summary>
		/// <param name="matchId">Match id</param>
		/// <returns>Match model which has this id</returns>
		/// <exception cref="NotFoundException"></exception>
		Task<MatchModel> GetMatchByIdAsync(long matchId);
	}
}
