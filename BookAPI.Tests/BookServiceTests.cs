using BookApi.Api.Repositories;
using BookAPI.Api.DTOs;
using BookAPI.Api.Models;
using BookAPI.Api.Services;
using Moq;
using System.Threading.Tasks;

namespace BookAPI.Tests;

public class BookServiceTests
{
    [Fact]
    public async Task AddAsync_Should_Call_Repository_With_Correct_Data()
    {
        var mockRepository = new Mock<IBookRepository>();
		var bookService = new BookService(mockRepository.Object);

		var dto = new BookDto()
		{
			Author = "Test Author",
			Title = "Test Title",
			PublishedYear = DateTime.Now.Year,
		};

		await bookService.AddAsync(dto);

		mockRepository.As<IBookRepository>().Verify(
			r => r.AddAsync(It.Is<Book>(b =>
				b.Author == dto.Author &&
				b.Title == dto.Title &&
				b.PublishedYear == dto.PublishedYear)),
			Times.Once);
	}

	[Fact]
	public async Task UpdateAsync_Should_Get_Then_Update_Book()
	{
		// Arrange
		var existingBook = new Book { Id = 1, Title = "Viejo título", Author = "Viejo autor", PublishedYear = 2020 };
		var mockRepo = new Mock<IBookRepository>();
		mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingBook);

		var service = new BookService(mockRepo.Object);
		var dto = new BookDto { Title = "Nuevo título", Author = "Nuevo autor" };

		// Act
		await service.UpdateAsync(1, dto);

		// Assert
		mockRepo.Verify(r => r.GetByIdAsync(1), Times.Once); // se llamó a GetByIdAsync
		mockRepo.Verify(r => r.UpdateAsync(It.Is<Book>(b =>
			b.Id == 1 &&
			b.Title == "Nuevo título" &&
			b.Author == "Nuevo autor"
		)), Times.Once); // se llamó a UpdateAsync con los nuevos datos
	}

	[Fact]
	public async Task UpdateAsync_Should_Call_UpdateAsync_But_It_Does_Not()
	{
		// Arrange
		var book = new Book { Id = 1, Title = "Antiguo", Author = "Autor" };
		var mockRepo = new Mock<IBookRepository>();
		mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(book);
		var service = new BookService(mockRepo.Object);
		var dto = new BookDto { Title = "Nuevo", Author = "Nuevo Autor" };

		// Act
		await service.UpdateAsync(1, dto);

		// Assert
		mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Book>()), Times.Once); // ❌ Falla!
	}
}
