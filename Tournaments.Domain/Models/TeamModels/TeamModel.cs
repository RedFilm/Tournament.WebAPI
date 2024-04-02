namespace Tournaments.Domain.Models.TeamModels
{
	public class TeamModel
    {
        public long OwnerId { get; set; }

        public string TeamName { get; set; } = null!;
        public DateTime CreationDate { get; set; }
    }
}
