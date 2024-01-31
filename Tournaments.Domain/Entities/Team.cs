namespace Tournaments.Domain.Entities
{
	public class Team
	{
        public int Id { get; set; }

        public List<AppUser> Players { get; set; }

		public List<TournamentTeam> TournamentTeams { get; set; }

		public string TeamName { get; set; }
        public int ParticipantCount { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
