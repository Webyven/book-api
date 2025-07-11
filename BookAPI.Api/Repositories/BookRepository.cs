using BookApi.Api.Repositories;
using BookAPI.Api.Data;
using BookAPI.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Api.Repositories
{
	public class BookRepository : IBookRepository
	{
		private readonly BookDbContext _dbContext;

		public BookRepository(BookDbContext dbContext) { 
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<Book>> GetAllAsync()
		{
			return await _dbContext.Books.ToListAsync();
		}

		public async Task<Book?> GetByIdAsync(int id)
		{
			return await _dbContext.Books.FindAsync(id);
		}

		public async Task AddAsync(Book book)
		{
			await _dbContext.Books.AddAsync(book);
			await _dbContext.SaveChangesAsync();
		}

		public async Task UpdateAsync(Book book)
		{
			_dbContext.Books.Update(book);
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(Book book)
		{
			_dbContext.Books.Remove(book);
			await _dbContext.SaveChangesAsync();
		}
	}
}
