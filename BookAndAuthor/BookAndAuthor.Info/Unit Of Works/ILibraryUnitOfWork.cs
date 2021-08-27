using BookAndAuthor.Data;
using BookAndAuthor.Info.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAndAuthor.Info.Unit_Of_Works
{
  
    public interface ILibraryUnitOfWork : IUnitOfWork
    {
        IBookRepository Book { get; }
        IAuthorRepository Author { get; }
    }
}
