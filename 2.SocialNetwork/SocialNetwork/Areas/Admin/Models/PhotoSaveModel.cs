using SocialNetwork.Gallery.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using SocialNetwork.Gallery.Busieness_Object;

namespace SocialNetwork.Areas.Admin.Models
{
    public class PhotoSaveModel
    {
        private readonly IGalleryServices _galleryServices;
        public string memberName { get; set; }
        public int photoId { get; set; }
        public PhotoSaveModel()
        {
            _galleryServices = Startup.AutofacContainer.Resolve<IGalleryServices>();
        }
        public PhotoSaveModel(IGalleryServices galleryServices)
        {
            _galleryServices = galleryServices;
        }
        public void SavingPhoto()
        {
            var member = _galleryServices.GetAllMembers();
            var selectecdMember = member.Where(x => x.Name == memberName).FirstOrDefault();

            var photo = new PhotoBusinessObject()
            {
                PhotoFileName = "kratos.jpg"
            };
            _galleryServices.SavingPhoto(selectecdMember, photo);
        }
    }
}
