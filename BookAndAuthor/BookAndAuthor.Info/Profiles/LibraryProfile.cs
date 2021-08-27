using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO = BookAndAuthor.Info.Business_Object;
using EO = BookAndAuthor.Info.Entities;

namespace BookAndAuthor.Info.Profiles
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile()
        {
            CreateMap<EO.Book, BO.Book>().ReverseMap();
            CreateMap<EO.Author, BO.Author>().ReverseMap();

        }
    }
}
