using AutoMapper;
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
    }
}
