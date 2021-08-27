using AutoMapper;
using BookAndAuthor.Areas.Admin.Models;
using BookAndAuthor.Info.Business_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAndAuthor.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<CreateBookModel, Book>().ReverseMap();
            CreateMap<EditBookModel, Book>().ReverseMap();
            CreateMap<CreateAuthorModel, Author>().ReverseMap();
            CreateMap<EditAuthorModel, Author>().ReverseMap();

        }
    }
}
