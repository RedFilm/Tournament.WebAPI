namespace Tournaments.Domain.Models.TeamModels
{
	public class TeamMemberUpdateModel
	{
        public long ExecutorId { get; set; }
        public long TeamId { get; set; }
        public long PlayerId { get; set; }
    }
}
