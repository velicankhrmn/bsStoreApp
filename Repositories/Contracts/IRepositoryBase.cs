using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryBase<T>
    {
        //Crud operations have defined.
        IQueryable<T> FindAll(bool trackChanges); //Nesne izleme özelliğinin kapatılıp açılması.
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges); // Belirli bir özelliğe göre arama işlemi içerisine bir fonksiyon alacak.
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
