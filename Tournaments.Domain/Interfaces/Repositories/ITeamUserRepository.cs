namespace Tournaments.Domain.Interfaces.Repositories
{
	public interface ITeamUserRepository
	{
		/// <summary>
		/// Asynchronously checks if user's in team.
		/// </summary>
		/// <param name="teamId">Team id</param>
		/// <param name="userId">AppUser id</param>
		/// <returns>Result of checking. True if user's already in team.</returns>
		Task<bool> AnyAsync(long teamId, long userId);
	}
}
