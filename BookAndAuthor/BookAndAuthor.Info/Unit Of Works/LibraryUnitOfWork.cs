using BookAndAuthor.Data;
using BookAndAuthor.Info.Context;
using BookAndAuthor.Info.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAndAuthor.Info.Unit_Of_Works
{

    public class LibraryUnitOfWork : UnitOfWork, ILibraryUnitOfWork
    {


        public IBookRepository Book { get; private set; }
        public IAuthorRepository Author { get; private set; }


        public LibraryUnitOfWork(ILibraryDbContext context,
             IBookRepository book,
             IAuthorRepository author)
              : base((DbContext)context)
        {
            Book = book;
            Author = author;
        }
    }
}
