using BookApi.Api.Repositories;
using BookApi.Api.Services;
using BookAPI.Api.DTOs;
using BookAPI.Api.Models;

namespace BookAPI.Api.Services
{
	public class BookService : IBookService
	{
		private readonly IBookRepository _bookRepository;

		public BookService(IBookRepository bookRepository)
		{
			_bookRepository = bookRepository;
		}

		public async Task<IEnumerable<Book>> GetAllAsync()
		{
			return await _bookRepository.GetAllAsync();
		}

		public async Task<Book?> GetByIdAsync(int id)
		{
			return await _bookRepository.GetByIdAsync(id);
		}

		public async Task AddAsync(BookDto dto)
		{
			var book = new Book
			{
				Title = dto.Title,
				Author = dto.Author,
				PublishedYear = dto.PublishedYear
			};
			await _bookRepository.AddAsync(book);
		}

		public async Task UpdateAsync(int id, BookDto dto)
		{
			var book = await _bookRepository.GetByIdAsync(id);
			if (book == null) return;
			book.Title = dto.Title;
			book.Author = dto.Author;
			book.PublishedYear = dto.PublishedYear;
			await _bookRepository.UpdateAsync(book); // This line is commented out to check how failed tests works
		}

		public async Task DeleteAsync(int id)
		{
			var book = await _bookRepository.GetByIdAsync(id);
			if (book == null) return;
			await _bookRepository.DeleteAsync(book);
		}
	}
}
