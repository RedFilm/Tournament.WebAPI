namespace Tournaments.Domain.Models
{
	public class RegisterModel
	{
		public string UserName { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
