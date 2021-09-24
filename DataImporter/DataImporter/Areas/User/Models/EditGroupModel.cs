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
    public class EditGroupModel

     {

        public int Id { get; set; }
        [Required, MaxLength(100, ErrorMessage = "Nameshould be less than 100 characters")]
        public string Name { get; set; }

        private readonly IDataImporterService _iDataImporterService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EditGroupModel()
        {
            _iDataImporterService = Startup.AutofacContainer.Resolve<IDataImporterService>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
        }
        public EditGroupModel(IDataImporterService dataImporterService , IHttpContextAccessor httpContextAccessor)
        {
            _iDataImporterService = dataImporterService;
            _httpContextAccessor = httpContextAccessor;
        }

        public void LoadModelData(int id)
        {
            var group = _iDataImporterService.LoadGroup(id);

            Id = group.Id;
            Name = group.Name;
          

        }

        internal void Update()
        {
            var id = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var group = new Group
            {
                Id = Id,
                Name = Name 

            };
            _iDataImporterService.UpdateGroup(group , id);
        }
    }
}
