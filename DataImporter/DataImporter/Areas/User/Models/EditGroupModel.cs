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
    public class EditGroupModel

     {

        public int Id { get; set; }
        [Required, MaxLength(100, ErrorMessage = "Nameshould be less than 100 characters")]
        public string Name { get; set; }

        private readonly IDataImporterService _iDataImporterService;
        public EditGroupModel()
        {
            _iDataImporterService = Startup.AutofacContainer.Resolve<IDataImporterService>();
        }
        public EditGroupModel(IDataImporterService dataImporterService)
        {
            _iDataImporterService = dataImporterService;
        }

        public void LoadModelData(int id)
        {
            var group = _iDataImporterService.LoadGroup(id);

            Id = group.Id;
            Name = group.Name;
          

        }

        internal void Update()
        {
            var group = new Group
            {
                Id = Id,
                Name = Name,
               

            };
            _iDataImporterService.UpdateGropu(group);
        }
    }
}
