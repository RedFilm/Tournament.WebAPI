namespace Tournaments.Domain.Entities
{
	public class Bracket
	{
        public long Id { get; set; }
        public long TournamentId { get; set; }
        public Tournament Tournament { get; set; } = null!;
        public ICollection<Stage> Stages { get; set; } = null!;
    }
}
