namespace Tournaments.Domain.Entities
{
	public class Team
	{
        public int Id { get; set; }

        public List<AppUser> Players { get; set; } = null!;

        public List<TournamentTeam> TournamentTeams { get; set; } = null!;

		public string TeamName { get; set; } = null!;
        public int ParticipantCount { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
