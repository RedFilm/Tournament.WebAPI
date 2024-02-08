namespace Tournaments.Domain.Models
{
	public class ExceptionResponseModel
	{
        public IEnumerable<string> Errors { get; set; } = null!;
    }
}
