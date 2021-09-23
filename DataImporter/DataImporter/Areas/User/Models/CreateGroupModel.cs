using Autofac;
using DataImporter.Info.Business_Object;
using DataImporter.Info.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataImporter.Areas.User.Models
{

    public class CreateGroupModel
    {

        public int Id { get; set; }
        [Required, MaxLength(100, ErrorMessage = "Nameshould be less than 100 characters")]
        public string Name { get; set; }

        private readonly IDataImporterService _iDataImporterService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateGroupModel()
        {
            _iDataImporterService = Startup.AutofacContainer.Resolve<IDataImporterService>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
        }
        public CreateGroupModel(IDataImporterService iDataImporterService , IHttpContextAccessor httpContextAccessor)
        {
            _iDataImporterService = iDataImporterService;
            _httpContextAccessor = httpContextAccessor;
        }

        internal void CreateGroup()
        {
            var group = new Group()
            {
                Id = Id,
                Name = Name,
                ApplicationUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))


        };
            _iDataImporterService.CreateGroup(group);
        }

        internal void DeleteGroup(int id)
        {
            _iDataImporterService.DeleteGroup(id);
        }
    }
}
