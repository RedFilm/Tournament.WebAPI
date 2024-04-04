namespace Tournaments.Domain.Models
{
	public class PageModel<T>
	{
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int ItemsCount { get; set; }

        public IEnumerable<T> Items { get; set; } = null!;
    }
}
