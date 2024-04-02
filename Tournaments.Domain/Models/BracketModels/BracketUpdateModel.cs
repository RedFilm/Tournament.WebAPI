namespace Tournaments.Domain.Models.BracketModels
{
	public class BracketUpdateModel
	{
		public long TournamentId { get; set; }

		public List<MatchResultModel> Results { get; set; } = null!;
    }
}
