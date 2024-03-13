using Tournaments.Domain.Entities;

namespace Tournaments.Domain.Models.BracketModels
{
	public class BracketModel
	{
		public long Id { get; set; }
		public long TournamentId { get; set; }
		public List<StageModel> Stages { get; set; } = null!;
	}
}
