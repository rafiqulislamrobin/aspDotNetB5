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
    public class EditAuthorModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        private readonly IAuthorService _iAuthorService;
        private readonly IMapper _mapper;
        public EditAuthorModel()
        {
            _iAuthorService = Startup.AutofacContainer.Resolve<IAuthorService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }
        public EditAuthorModel(IAuthorService iAuthorService, IMapper mapper)
        {
            _mapper = mapper;
            _iAuthorService = iAuthorService;
        }
        internal void LoadModelData(int id)
        {
            var author = _iAuthorService.Getauthor(id);

            Id = author.Id;
            Name = author.Name;
            DateOfBirth = author.DateOfBirth;
            
        }

        internal void Update()
        {
            var author = _mapper.Map<Author>(this);
            _iAuthorService.UpdateAuthor(author);
        }
    }
}
