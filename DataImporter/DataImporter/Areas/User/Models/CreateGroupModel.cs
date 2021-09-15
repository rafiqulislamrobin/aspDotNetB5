using Autofac;
using DataImporter.Info.Business_Object;
using DataImporter.Info.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.User.Models
{

    public class CreateGroupModel
    {

        public int Id { get; set; }
        [Required, MaxLength(100, ErrorMessage = "Nameshould be less than 100 characters")]
        public string Name { get; set; }

        private readonly IDataImporterService _iDataImporterService;
        public CreateGroupModel()
        {
            _iDataImporterService = Startup.AutofacContainer.Resolve<IDataImporterService>();
        }
        public CreateGroupModel(IDataImporterService iDataImporterService)
        {
            _iDataImporterService = iDataImporterService;
        }

        internal void CreateGroup()
        {
            var group = new Group()
            {
                Id = Id,
                Name = Name
           

            };
            _iDataImporterService.CreateGroup(group);
        }

        internal void DeleteGroup(int id)
        {
            _iDataImporterService.DeleteGroup(id);
        }
    }
}
