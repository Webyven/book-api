using BookAPI.Api.DTOs;
using BookAPI.Api.Models;

namespace BookApi.Api.Services;

public interface IBookService
{
	Task<IEnumerable<Book>> GetAllAsync();
	Task<Book?> GetByIdAsync(int id);
	Task AddAsync(BookDto dto);
	Task UpdateAsync(int id, BookDto dto);
	Task DeleteAsync(int id);
}
