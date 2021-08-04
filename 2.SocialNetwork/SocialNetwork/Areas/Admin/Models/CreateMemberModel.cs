using SocialNetwork.Gallery.Busieness_Object;
using SocialNetwork.Gallery.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Areas.Admin.Models
{
    public class CreateMemberModel
    {

        public int? Id { get; set; }


        [Required, MaxLength(100, ErrorMessage = "Nameshould be less than 100 characters")]
        public string Name { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }


        [Required, MaxLength(300, ErrorMessage = "Nameshould be less than 300 characters")]
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
                DateOfBirth = DateOfBirth.Value,
                Address = Address,

            };
            _iGalleryServices.CreateMember(Member);
        }
    }
}
