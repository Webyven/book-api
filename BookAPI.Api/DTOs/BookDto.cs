namespace BookAPI.Api.DTOs
{
	public class BookDto
	{
		public string Title { get; set; } = string.Empty;
		public string Author { get; set; } = string.Empty;
		public int PublishedYear { get; set; } = 0;
	}
}
