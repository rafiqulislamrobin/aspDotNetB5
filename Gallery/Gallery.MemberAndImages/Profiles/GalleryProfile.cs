using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EO = Gallery.MemberAndImages.Entities;
using BO = Gallery.MemberAndImages.Business_Object;

namespace Gallery.MemberAndImages.Profiles
{
    class GalleryProfile : Profile
    {
        public GalleryProfile()
        {
            CreateMap<EO.Member, BO.Member>().ReverseMap();

        }
    }
}
