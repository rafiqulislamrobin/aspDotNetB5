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
    public class AuthorService : IAuthorService
    {
        private readonly ILibraryUnitOfWork _ilibraryUnitOfWork;
        private readonly IMapper _mapper;

        public AuthorService(ILibraryUnitOfWork libraryUnitOfWork, IMapper mapper)
        {
            _ilibraryUnitOfWork = libraryUnitOfWork;
            _mapper = mapper;

        }

        public void CreateAuthor(Author author)
        {
            if (author == null)
                throw new InvalidParameterException("author was not found");

            if (IsNameAlreadyUsed(author.Name))
                throw new DuplicateException("author Name");

            _ilibraryUnitOfWork.Author.Add(
         _mapper.Map<Entities.Author>(author)
        );
            _ilibraryUnitOfWork.Save();
        }

        public void DeleteAuthor(int id)
        {
            _ilibraryUnitOfWork.Author.Remove(id);
            _ilibraryUnitOfWork.Save(); ;
        }

        public Author Getauthor(int id)
        {
            var author = _ilibraryUnitOfWork.Author.GetById(id);
            if (author == null)
            {
                return null;
            }

            return _mapper.Map<Author>(author);
        }

        public (IList<Author> records, int total, int totalDisplay) GetAuthors
            (int pageIndex, int pageSize, string searchText, string sortText)
        {
            var authorData = _ilibraryUnitOfWork.Author.GetDynamic(
           string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name.Contains(searchText),
           sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from author in authorData.data
                              select _mapper.Map<Author>(author)).ToList();


            return (resultData, authorData.total, authorData.totalDisplay);
        }

        public void UpdateAuthor(Author author)
        {
            if (author == null)
            {
                throw new InvalidOperationException("author is missing");
            }
            if (IsNameAlreadyUsed(author.Name, author.Id))
            {
                throw new DuplicateException("author name is already used");
            }
            var authorInfo = _ilibraryUnitOfWork.Author.GetById(author.Id);
            if (authorInfo != null)
            {
                _mapper.Map(author, authorInfo);

                _ilibraryUnitOfWork.Save();
            }
            else
            {
                throw new InvalidOperationException("author is not available");
            }
        }

        private bool IsNameAlreadyUsed(string name) =>
         _ilibraryUnitOfWork.Author.GetCount(n => n.Name == name) > 0;

        private bool IsNameAlreadyUsed(string name, int id) =>
              _ilibraryUnitOfWork.Author.GetCount(n => n.Name == name && n.Id != id) > 0;
    }
}
