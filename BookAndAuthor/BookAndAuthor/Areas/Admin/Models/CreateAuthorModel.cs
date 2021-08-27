using Autofac;
using AutoMapper;
using BookAndAuthor.Info.Business_Object;
using BookAndAuthor.Info.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookAndAuthor.Areas.Admin.Models
{
    public class CreateAuthorModel
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
      
        public DateTime DateOfBirth { get; set; }

        private readonly IAuthorService _iAuthorService;
        private readonly IMapper _mapper;
        public CreateAuthorModel()
        {
            _iAuthorService = Startup.AutofacContainer.Resolve<IAuthorService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }
        public CreateAuthorModel(IAuthorService iAuthorService, IMapper mapper)
        {
            _mapper = mapper;
            _iAuthorService = iAuthorService;
        }
        internal void CreateAuthors()
        {

            var Author = _mapper.Map<Author>(this);


            _iAuthorService.CreateAuthor(Author);
        }

        internal void Delete(int id)
        {
            _iAuthorService.DeleteAuthor(id);
        }
    }
}
