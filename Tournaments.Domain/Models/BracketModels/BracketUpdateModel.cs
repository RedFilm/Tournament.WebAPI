namespace Tournaments.Domain.Models.BracketModels
{
	public class BracketUpdateModel
	{
        public long TournamentId { get; set; }

        // Dictionary<long,long> - MatchId, WinnerId
        public Dictionary<long, long>? Results { get; set; }
    }
}
