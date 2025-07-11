using Microsoft.EntityFrameworkCore;
using BookAPI.Api.Models;

namespace BookAPI.Api.Data
{
	public class BookDbContext : DbContext
	{
		public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
		{
		}

		public DbSet<Book> Books { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Book>().HasData(
				new Book { Id = 1, Title = "1984", Author = "George Orwell", PublishedYear = 1949 },
				new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", PublishedYear = 1960 }
			);
			base.OnModelCreating(modelBuilder);
		}
	}
}
