namespace Tournaments.Domain.ViewModels
{
	public class TournamentViewModel
	{
		public int Id { get; set; }
		public string OrganizerId { get; set; }

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
