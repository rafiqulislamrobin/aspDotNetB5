using BookAndAuthor.Data;
using BookAndAuthor.Info.Context;
using BookAndAuthor.Info.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAndAuthor.Info.Repositories
{
    public  class AuthorRepository : Repository<Author, int>, IAuthorRepository
    {
        public AuthorRepository(ILibraryDbContext context)
    : base((DbContext)context)
        {

        }
    }
}
