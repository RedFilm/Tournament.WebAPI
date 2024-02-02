using System.ComponentModel.DataAnnotations;

namespace Tournaments.Domain.Options
{
	public class JwtOptions
	{
		public const string ConfigurationPath = "JwtOptions";

		[Required]
		[MinLength(64, ErrorMessage = "The key must be 64 or more characters.")]
		public string Key { get; set; } = null!;
		[Required]
		public string Issuer { get; set; } = null!;
		[Required]
		public string Audience { get; set; } = null!;
    }
}
