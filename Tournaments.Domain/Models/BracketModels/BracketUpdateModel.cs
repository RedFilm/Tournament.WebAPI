namespace Tournaments.Domain.Models.BracketModels
{
	public class BracketUpdateModel
	{
        public long Id { get; set; }

		// Dictionary<long,long> - MatchId, WinnerId
		public Dictionary<long, long>? Results { get; set; }
    }
}
