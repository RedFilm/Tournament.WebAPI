using Tournaments.Domain.Statuses;

namespace Tournaments.Domain.Models
{
	public class TeamModel
	{
        public long Id { get; set; }
        public long OwnerId { get; set; }

		public string TeamName { get; set; } = null!;
        public TournamentTeamStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
	}
}
