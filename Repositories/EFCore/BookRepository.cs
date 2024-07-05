using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context)
        {

        }

        public IQueryable<Book> GetAllBook(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(b => b.Id);

        public Book GetBook(int id, bool trackChanges) =>
            FindByCondition(b => b.Id.Equals(id), trackChanges).SingleOrDefault();

        public void CreateBook(Book book) => Create(book);

        public void UpdateBook(Book book) => Update(book);

        public void DeleteBook(Book book) => Delete(book);
    }
}
