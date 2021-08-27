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

    public class MemberEditModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Address { get; set; }

        private readonly IGalleryService _iGalleryService;
        private readonly IMapper _mapper;

        public MemberEditModel()
        {
            _iGalleryService = Startup.AutofacContainer.Resolve<IGalleryService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }
        public MemberEditModel(IGalleryService iGalleryService, IMapper mapper)
        {
            _mapper = mapper;
            _iGalleryService = iGalleryService;
        }
        internal void LoadModelData(int id)
        {
            var member = _iGalleryService.GetMember(id);

            Id = member.Id;
            Name = member.Name;
            Address = member.Address;
            Age = member.Age;
        }

        internal void Update()
        {
            var member = _mapper.Map<Member>(this);
            _iGalleryService.UpdateMember(member);
        }
    }
}
