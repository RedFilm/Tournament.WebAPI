using Microsoft.AspNetCore.Identity;

namespace Tournaments.Domain.Entities
{
	public class AppUser : IdentityUser
	{
        public List<Team> Teams { get; set; } = null!;
        public List<Tournament> Tournaments { get; set; } = null!;

        public DateTime Birthday { get; set; }
    }
}
