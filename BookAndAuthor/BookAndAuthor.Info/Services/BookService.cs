using AutoMapper;
using BookAndAuthor.Info.Business_Object;
using BookAndAuthor.Info.Exceptions;
using BookAndAuthor.Info.Unit_Of_Works;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAndAuthor.Info.Services
{
    public  class BookService : IBookService
    {
        private readonly ILibraryUnitOfWork _ilibraryUnitOfWork;
        private readonly IMapper _mapper;

        public BookService (ILibraryUnitOfWork libraryUnitOfWork, IMapper mapper)
        {
            _ilibraryUnitOfWork = libraryUnitOfWork;
            _mapper = mapper;

        }

        public void CreateBook(Book book)
        {
            if (book == null)
                throw new InvalidParameterException("Book was not found");

            if (IsTitleAlreadyUsed(book.Title))
                throw new DuplicateException("Book Name");

            _ilibraryUnitOfWork.Book.Add(
         _mapper.Map<Entities.Book>(book)
        );
            _ilibraryUnitOfWork.Save();
        }

        public void DeleteBook(int id)
        {
            _ilibraryUnitOfWork.Book.Remove(id);
            _ilibraryUnitOfWork.Save();
        }

        public Book GetBook(int id)
        {
            var book = _ilibraryUnitOfWork.Book.GetById(id);
            if (book == null)
            {
                return null;
            }

            return _mapper.Map<Book>(book);
        }

        public (IList<Book> records, int total, int totalDisplay) GetBooks
            (int pageIndex, int pageSize, string searchText, string sortText)
        {
            var bookData = _ilibraryUnitOfWork.Book.GetDynamic(
            string.IsNullOrWhiteSpace(searchText) ? null : x => x.Title.Contains(searchText),
            sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from book in bookData.data
                              select _mapper.Map<Book>(book)).ToList();


            return (resultData, bookData.total, bookData.totalDisplay);
        }

        public void UpdateBook(Book book)
        {
            if (book == null)
            {
                throw new InvalidOperationException("Book is missing");
            }
            if (IsTitleAlreadyUsed(book.Title, book.Id))
            {
                throw new DuplicateException("Book name is already used");
            }
            var doctorInfo = _ilibraryUnitOfWork.Book.GetById(book.Id);
            if (doctorInfo != null)
            {
                _mapper.Map(book, doctorInfo);

                _ilibraryUnitOfWork.Save();
            }
            else
            {
                throw new InvalidOperationException("Book is not available");
            }
        }

        private bool IsTitleAlreadyUsed(string title) =>
           _ilibraryUnitOfWork.Book.GetCount(n => n.Title == title) > 0;

        private bool IsTitleAlreadyUsed(string title, int id) =>
              _ilibraryUnitOfWork.Book.GetCount(n => n.Title == title && n.Id != id) > 0;
    }
}
