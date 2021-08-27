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
    public class EditBookModel
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

        public EditBookModel()
        {
            _iBookService = Startup.AutofacContainer.Resolve<IBookService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }
        public EditBookModel(IBookService iBookService, IMapper mapper)
        {
            _mapper = mapper;
            _iBookService = iBookService;
        }
        internal void LoadModelData(int id)
        {
            var doctor = _iBookService.GetBook(id);

            Id = doctor.Id;
            Title = doctor.Title;
            Barcode = doctor.Barcode;
            Price = doctor.Price;
        }

        internal void Update()
        {
            var book = _mapper.Map<Book>(this);
            _iBookService.UpdateBook(book);
        }
    }
}
