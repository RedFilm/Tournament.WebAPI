namespace Tournaments.Domain.Models
{
	public class Tournament
	{
        public int Id { get; set; }

        public string OrganizerId { get; set; }
        public AppUser Organizer { get; set; }

        public List<TournamentTeam> TournamentTeams { get; set; }

        public int PrizePool { get; set; }
		public int MaxParticipantCount { get; set; }
		public string TournamentName { get; set; }
		public string GameName { get; set; }
		public string TournamentDescription { get; set; }

        public DateTime RegistrationStartDate { get; set; }
		public DateTime RegistrationEndDate { get; set; }
		public DateTime TournamentStartDate { get; set; }
		public DateTime TournamentEndDate { get; set; }
    }
}
