using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Services.Contracts
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks(bool trackChanges);
        Book GetBook(int id,bool trackChanges);
        Book CreateBook(Book book);
        void UpdateBook(int id,Book book, bool trackChanges);
        void DeleteBook(int id, bool trackChanges);
    }
}
