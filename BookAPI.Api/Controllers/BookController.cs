using Microsoft.AspNetCore.Mvc;
using BookApi.Api.Services;
using BookAPI.Api.DTOs;

namespace BookApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
	private readonly IBookService _service;

	public BookController(IBookService service)
	{
		_service = service;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll() =>
		Ok(await _service.GetAllAsync());

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		var book = await _service.GetByIdAsync(id);
		return book == null ? NotFound() : Ok(book);
	}

	[HttpPost]
	public async Task<IActionResult> Create(BookDto dto)
	{
		await _service.AddAsync(dto);
		return CreatedAtAction(nameof(GetAll), null);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> Update(int id, BookDto dto)
	{
		await _service.UpdateAsync(id, dto);
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		await _service.DeleteAsync(id);
		return NoContent();
	}
}
