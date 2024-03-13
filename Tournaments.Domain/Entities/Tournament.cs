namespace Tournaments.Domain.Entities
{
	public class Tournament
	{
        public long Id { get; set; }

        public long? BracketId { get; set; }
        public Bracket? Bracket { get; set; }

        public long OrganizerId { get; set; }
		public AppUser Organizer { get; set; } = null!;

        public List<TournamentTeam> TournamentTeams { get; set; } = null!;

        public int PrizePool { get; set; }
		public int MaxParticipantCount { get; set; }
		public string TournamentName { get; set; } = null!;
		public string GameName { get; set; } = null!;
		public string TournamentDescription { get; set; } = null!;

        public DateTime RegistrationStartDate { get; set; }
		public DateTime RegistrationEndDate { get; set; }
		public DateTime TournamentStartDate { get; set; }
		public DateTime TournamentEndDate { get; set; }
    }
}
