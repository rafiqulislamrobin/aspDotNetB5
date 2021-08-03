using SocialNetwork.Gallery.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using SocialNetwork.Gallery.Busieness_Object;

namespace SocialNetwork.Areas.Admin.Models
{
    public class GalleryModel
    {
        public IList<MemberBusinessObject> Members{ get; set; }

        private readonly IGalleryServices _galleryServices;
        public GalleryModel()
        {
            _galleryServices = Startup.AutofacContainer.Resolve<IGalleryServices>();
        }
        public GalleryModel(IGalleryServices galleryServices)
        {
            _galleryServices = galleryServices;
        }

        public void LoadModelData()
        {
            Members = _galleryServices.GetAllMembers();
        }
    }
}
