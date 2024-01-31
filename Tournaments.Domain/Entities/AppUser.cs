using Microsoft.AspNetCore.Identity;

namespace Tournaments.Domain.Entities
{
	public class AppUser : IdentityUser
	{
        public List<Team> Teams { get; set; }
        public List<Tournament> Tournaments { get; set; }

        public DateTime Birthday { get; set; }
    }
}
