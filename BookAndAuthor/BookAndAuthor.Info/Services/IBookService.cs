﻿using BookAndAuthor.Info.Business_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAndAuthor.Info.Services
{
    public interface IBookService
    {
        (IList<Book> records, int total, int totalDisplay) GetBooks(int pageIndex, int pageSize,
                                                                   string searchText, string sortText);
        void DeleteBook(int id);
        void CreateBook(Book book);
        void UpdateBook(Book book);
        Book GetBook(int id);
    }
}
