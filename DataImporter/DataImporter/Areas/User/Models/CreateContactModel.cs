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

    public class CreateContactModel
    {

        public int Id { get; set; }
        [Required, MaxLength(100, ErrorMessage = "Nameshould be less than 100 characters")]


        public string Name { get; set; }
        public string Address { get; set; }
       

      
        private readonly IDataImporterService _iDataImporterService;
        public CreateContactModel()
        {
            _iDataImporterService = Startup.AutofacContainer.Resolve<IDataImporterService>();
        }
        public CreateContactModel(IDataImporterService iDataImporterService)
        {
            _iDataImporterService = iDataImporterService;
        }

        internal void CreateContact()
        {
            var contact = new Contact()
            {
                Id = Id,
                Name = Name,
               
                Address = Address

            };
            _iDataImporterService.CreateContact(contact);
        }
    }

}
