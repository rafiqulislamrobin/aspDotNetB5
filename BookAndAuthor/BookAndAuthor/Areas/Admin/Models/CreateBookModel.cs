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
    public class CreateBookModel
    {
      
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Barcode { get; set; }
        [Required]
        public double Price { get; set; }


        private readonly IBookService _iBookService;
        private readonly IMapper _mapper;

        public CreateBookModel()
        {
            _iBookService = Startup.AutofacContainer.Resolve<IBookService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }
        public CreateBookModel(IBookService iBookService, IMapper mapper)
        {
            _mapper = mapper;
            _iBookService = iBookService;
        }
        internal void CreateBook()
        {

            var book = _mapper.Map<Book>(this);


            _iBookService.CreateBook(book);
        }

        internal void Delete(int id)
        {
            _iBookService.DeleteBook(id);
        }
    }

}
