namespace Tournaments.Domain.Models.TeamModels
{
	public class RegisterForTournamentModel
	{
        public List<long> PlayerIDs { get; set; } = null!;
        public long TeamId { get; set; }
        public long TournamentId { get; set; }
        public long OwnerId { get; set; }
    }
}
