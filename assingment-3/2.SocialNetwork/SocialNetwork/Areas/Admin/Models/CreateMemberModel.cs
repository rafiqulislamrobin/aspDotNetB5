using SocialNetwork.Gallery.Busieness_Object;
using SocialNetwork.Gallery.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace SocialNetwork.Areas.Admin.Models
{
    public class CreateMemberModel
    {

        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        private readonly IGalleryServices _iGalleryServices;
        public CreateMemberModel()
        {
            _iGalleryServices = Startup.AutofacContainer.Resolve<IGalleryServices>();
        }
        public CreateMemberModel(IGalleryServices bookingService)
        {
            _iGalleryServices = bookingService;
        }

        internal void CreateMember()
        {
            var Member = new MemberBusinessObject()
            {
                Name = Name,
                DateOfBirth = DateOfBirth,
                Address = Address,

            };
            _iGalleryServices.CreateMember(Member);
        }
    }
}
