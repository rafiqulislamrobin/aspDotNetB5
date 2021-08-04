using SocialNetwork.Gallery.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using SocialNetwork.Gallery.Busieness_Object;
using SocialNetwork.Models;

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
        internal object GetMembers(DataTablesAjaxRequestModel dataTableAjaxRequestModel)
        {

            var data = _galleryServices.GetMembers(
                dataTableAjaxRequestModel.PageIndex,
                dataTableAjaxRequestModel.PageSize,
                dataTableAjaxRequestModel.SearchText,
                dataTableAjaxRequestModel.GetSortText(new string[] { "Name", "DateOfBirth ", "Address" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.DateOfBirth.ToString(),
                                record.Address.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };

        }
    }
}
