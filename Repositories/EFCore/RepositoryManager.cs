﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoriesContext _context;
        private readonly Lazy<IBookRepository> _bookRepository;
        public RepositoryManager(RepositoriesContext context)
        {
            _context = context;
            _bookRepository = new Lazy<IBookRepository>(() => new BookRepository(_context));
        }

        public IBookRepository Book => _bookRepository.Value;
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
