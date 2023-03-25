using BookAPI.Model;
using BookAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _bookRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            return await _bookRepository.GetBookById(id);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBooks([FromBody] Book book)
        {
            var newBook = await _bookRepository.Create(book);
            return CreatedAtAction(nameof(GetAll), new { id = newBook.Id }, newBook);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> Delete(int id)
        {
            var bookToDelete = await _bookRepository.GetBookById(id);
            if (bookToDelete != null)
            {
                await _bookRepository.Delete(bookToDelete.Id);
                return NoContent();
            }
            return NotFound();

        }

        [HttpPut]
        public async Task<ActionResult<Book>> PutBooks(int id, [FromBody] Book book)
        {
            if (id == book.Id)
            {
                await _bookRepository.Update(book);
                return NoContent();
            }
            return BadRequest();
        }

    }
}
