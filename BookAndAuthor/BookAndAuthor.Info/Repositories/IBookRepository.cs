using BookAndAuthor.Data;
using BookAndAuthor.Info.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAndAuthor.Info.Repositories
{
   public  interface IBookRepository : IRepository<Book, int>
    {

    }
}
