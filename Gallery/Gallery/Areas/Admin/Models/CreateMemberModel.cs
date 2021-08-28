using Autofac;
using AutoMapper;
using Gallery.MemberAndImages.Business_Object;
using Gallery.MemberAndImages.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery.Areas.Admin.Models
{
    public class CreateMemberModel
    {
       
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Address { get; set; }


        public IGalleryService _iGalleryService;
        private readonly IMapper _mapper;

        public CreateMemberModel()
        {
            _iGalleryService = Startup.AutofacContainer.Resolve<IGalleryService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }
        public CreateMemberModel(IGalleryService iGalleryService, IMapper mapper)
        {
            _mapper = mapper;
            _iGalleryService = iGalleryService;
        }
        internal void CreateMember()
        {

            var member = _mapper.Map<Member>(this);


            _iGalleryService.CreateMember(member);
        }
    }
}
