using BookAndAuthor.Info.Business_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAndAuthor.Info.Services
{
    public interface IAuthorService
    {
        void DeleteAuthor(int id);
        (IList<Author> records, int total, int totalDisplay) GetAuthors(int pageIndex, int pageSize,
                                                                   string searchText, string sortText);
        void CreateAuthor(Author author);
        void UpdateAuthor(Author author);
        Author Getauthor(int id);
    }
}
