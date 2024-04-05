namespace Tournaments.Domain.Interfaces.Repositories
{
	public interface ITournamentTeamRepository
	{
		/// <summary>
		/// Asynchronously checks if tournament contains this team.
		/// </summary>
		/// <param name="tournamentId">Tournament id</param>
		/// <param name="teamId">Team id</param>
		/// <returns>Result of checking. True if tournament already contains team.</returns>
		Task<bool> AnyAsync(long tournamentId, long teamId);
	}
}
