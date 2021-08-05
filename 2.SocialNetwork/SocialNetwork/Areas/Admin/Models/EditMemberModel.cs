using Autofac;
using SocialNetwork.Gallery.Busieness_Object;
using SocialNetwork.Gallery.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Areas.Admin.Models
{
    public class EditMemberModel
    {
        public int? Id { get; set; }


        [Required, MaxLength(100, ErrorMessage = "Nameshould be less than 100 characters")]
        public string Name { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }


        [Required, MaxLength(300, ErrorMessage = "Nameshould be less than 300 characters")]
        public string Address { get; set; }



        private readonly IGalleryServices _iGalleryServices;
        public EditMemberModel()
        {
            _iGalleryServices = Startup.AutofacContainer.Resolve<IGalleryServices>();
        }
        public EditMemberModel(IGalleryServices bookingService)
        {
            _iGalleryServices = bookingService;
        }

        public void LoadModelData(int id)
        {
            var member = _iGalleryServices.GetMember(id);

            Id = member?.Id;
            Name = member?.Name;
            DateOfBirth = member?.DateOfBirth;
            Address = member?.Address;
        }

        internal void Update()
        {
            var member = new MemberBusinessObject
            {
                Id = Id.HasValue ? Id.Value : 0,
                Name = Name,
                DateOfBirth = DateOfBirth.HasValue ? DateOfBirth.Value : DateTime.MinValue,
                Address = Address

            };
            _iGalleryServices.UpdateMember(member);
        }
    }
}
