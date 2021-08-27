using Autofac;
using AutoMapper;
using Gallery.MemberAndImages.Services;
using Gallery.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery.Areas.Admin.Models
{

    public class MemberListModel
    {
        private readonly IGalleryService _iGalleryService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MemberListModel()
        {
            _iGalleryService = Startup.AutofacContainer.Resolve<IGalleryService>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
        }
        public MemberListModel(IGalleryService iGalleryService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _iGalleryService = iGalleryService;
        }

        internal object GetMember(DataTablesAjaxRequestModel dataTableAjaxRequestModel)
        {

            var data = _iGalleryService.GetMember(
                dataTableAjaxRequestModel.PageIndex,
                dataTableAjaxRequestModel.PageSize,
                dataTableAjaxRequestModel.SearchText,
                dataTableAjaxRequestModel.GetSortText(new string[] { "Name", "Address", "Age" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Address,
                                record.Age.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };

        }

        internal void Delete(int id)
        {
            _iGalleryService.DeleteMember(id);
        }
    }
}
