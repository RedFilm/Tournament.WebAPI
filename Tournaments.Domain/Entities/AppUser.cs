using Microsoft.AspNetCore.Identity;

namespace Tournaments.Domain.Entities
{
	public class AppUser : IdentityUser<long>
	{
        public List<Team> Teams { get; set; } = null!;
        public List<Tournament> Tournaments { get; set; } = null!;
		public List<TournamentTeam> TournamentTeams { get; set; } = null!;

		public double Rating { get; set; }

        public DateTime Birthday { get; set; }
    }
}
