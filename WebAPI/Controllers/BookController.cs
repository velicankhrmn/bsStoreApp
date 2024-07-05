using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Repositories.Contracts;
using Repositories.EFCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        //Dependency Injection
        private readonly IRepositoryManager _manager;

        public BookController(IRepositoryManager manager)
        {
            _manager = manager;
        }


        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _manager.Book.GetAllBook(false);

                if (!books.Any())
                    return NotFound();

                return Ok(books);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var book = _manager.Book.GetBook(id, false);

                if (book is null)
                    return NotFound();

                return Ok(book);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                    return BadRequest("Created book is invalid format.");

                //Check the book id is valid or not.
                if (book.Id != 0)
                    book.Id = 0;

                //The new item was added to the dbSet in the _context object.
                _manager.Book.CreateBook(book);
                //The changes are saved.
                _manager.Save();

                return StatusCode(201, book);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            try
            {
                //Get the entity from database
                var entity = _manager.Book.GetBook(id, true);

                //Check the entity is null or not.
                if (entity is null)
                    return NotFound();

                if (id != book.Id)
                    return BadRequest("Given book has not same id.");

                entity.Title = book.Title;
                entity.Price = book.Price;

                _manager.Save();

                return Ok(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                //Get the book from the given id
                var entity = _manager.Book.GetBook(id, false);

                //Check the book is exist.
                if (entity is null)
                    return NotFound();

                _manager.Book.DeleteBook(entity);
                _manager.Save();

                return NoContent();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // TODO: Write patch version
    }
}
