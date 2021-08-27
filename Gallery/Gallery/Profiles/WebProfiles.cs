using AutoMapper;
using Gallery.Areas.Admin.Models;
using Gallery.MemberAndImages.Business_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery.Profiles
{
    public class WebProfiles : Profile
    {
        public WebProfiles()
        {
            CreateMap<CreateMemberModel, Member>().ReverseMap();
            CreateMap<MemberEditModel, Member>().ReverseMap();

        }
    }
}
