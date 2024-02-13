namespace Tournaments.Domain.Models
{
	public class TeamModel
	{
        public long Id { get; set; }
        public long OwnerId { get; set; }

		public string TeamName { get; set; } = null!;
		public DateTime CreationDate { get; set; }
	}
}
