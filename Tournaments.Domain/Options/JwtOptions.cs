using System.ComponentModel.DataAnnotations;

namespace Tournaments.Domain.Options
{
	public class JwtOptions
	{
		public const string ConfigurationPath = "JwtOptions";

		[Required]
		[StringLength(maximumLength: 64 ,MinimumLength = 64, ErrorMessage = "Key length must be {1} characters")]
		public string Key { get; set; } = null!;
		[Required]
		public string Issuer { get; set; } = null!;
		[Required]
		public string Audience { get; set; } = null!;
    }
}
