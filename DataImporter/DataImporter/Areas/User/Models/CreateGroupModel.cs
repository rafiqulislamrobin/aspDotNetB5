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

        private readonly IGroupServices _groupServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateGroupModel()
        {
            _groupServices = Startup.AutofacContainer.Resolve<IGroupServices>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
        }
        public CreateGroupModel(IGroupServices groupServices, IHttpContextAccessor httpContextAccessor)
        {
            _groupServices = groupServices;
            _httpContextAccessor = httpContextAccessor;
        }

        internal void CreateGroup()
        {
            var id = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var group = new Group()
            {
                Id = Id,
                Name = Name,
                ApplicationUserId = id


            };
            _groupServices.CreateGroup(group , id);
        }

        internal void DeleteGroup(int id)
        {
            _groupServices.DeleteGroup(id);
        }
    }
}
