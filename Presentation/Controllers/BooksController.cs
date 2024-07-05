using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {

        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }


        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _manager.BookService.GetAllBooks(false);

                if (!books.Any())
                    return NotFound();

                return Ok(books);
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var book = _manager.BookService.GetBook(id, false);

                if (book is null)
                    return NotFound();

                return Ok(book);
            } catch (Exception e)
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
                _manager.BookService.CreateBook(book);

                return StatusCode(201, book);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            try
            {
                //Check book
                if (book is null)
                    return BadRequest();

                _manager.BookService.UpdateBook(id, book, true);

                return NoContent(); // 204
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                _manager.BookService.DeleteBook(id, false);

                return NoContent();
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // TODO: Write patch version
        //.NetCore.Asp.JsonPatch olacak kütüphane
    }
}
