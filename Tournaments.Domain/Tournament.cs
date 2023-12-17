namespace Tournaments.Domain
{
	public class Tournament
	{
		public Guid Id { get; set; }
		//public Guid RewardId { get; set; }
		//public List<Reward>? Rewards { get; set; }

		public string? Name { get; set; }
		public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
