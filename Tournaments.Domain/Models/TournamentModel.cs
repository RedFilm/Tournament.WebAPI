using System.ComponentModel.DataAnnotations;

namespace Tournaments.Domain.Models
{
	public class TournamentModel
	{
		public int Id { get; set; }
		public long OrganizerId { get; set; }

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
