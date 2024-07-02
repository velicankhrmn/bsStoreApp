using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        //Dependency Injection
        private readonly RepositoriesContext _context;

        public BookController(RepositoriesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _context.Books.ToList();

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
                var book = _context.Books.ToList().Find(b => b.Id.Equals(id));

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

                //The new item was added to the dbSet in the _context object.
                _context.Books.Add(book);
                //The changes are saved.
                _context.SaveChanges();

                return StatusCode(201, book);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdeteBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            try
            {
                //Get the entity from database
                var entity = _context.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();

                //Check the entity is null or not.
                if (entity is null)
                    return NotFound();

                if (id != book.Id)
                    return BadRequest("Given book has not same id.");

                entity.Title = book.Title;
                entity.Price = book.Price;
                _context.SaveChanges();

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
                var entity = _context.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();

                //Check the book is exist.
                if (entity is null)
                    return NotFound();

                _context.Books.Remove(entity);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult isExistsBooksById([FromRoute(Name= "id")] int id)
        {
            var entity = _context.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();

            if (entity is null)
                return NoContent();
            else
                return Ok("selamın aleyküm");
        }
    }
}
