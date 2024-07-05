using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;

        public BookManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
            return _manager.Book.GetAllBook(trackChanges);
        }

        public Book GetBook(int id, bool trackChanges)
        {
            return _manager.Book.GetBook(id, trackChanges);
        }

        public Book CreateBook(Book book)
        {
            if (book is null)
                throw new ArgumentNullException(nameof(book));

            _manager.Book.CreateBook(book);
            _manager.Save();

            return book;
        }

        public void UpdateBook(int id, Book book, bool trackChanges)
        {
            // check entitiy
            var entity = _manager.Book.GetBook(id,true);
            if (entity is null)
                throw new Exception("$Book with id:{id} could not found");

            // check params
            if (book is null)
                throw new ArgumentNullException(nameof(book));

            entity.Title = book.Title;
            entity.Price = book.Price;

            _manager.Book.UpdateBook(entity); // izlenen nesne direk kaydedilebilir efcore buna mmüsaade eder.
            _manager.Save();
        }

        public void DeleteBook(int id, bool trackChanges)
        {
            // check entitiy
            var entity = _manager.Book.GetBook(id, trackChanges);
            if (entity is null)
                throw new Exception("$Book with id:{id} could not found");

            _manager.Book.DeleteBook(entity);
            _manager.Save();
        }
    }
}
