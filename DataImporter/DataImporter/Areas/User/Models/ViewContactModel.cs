using Autofac;
using DataImporter.Info.Business_Object;
using DataImporter.Info.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.User.Models
{
    public class ViewContactModel
    {
        private readonly IDataImporterService _iDataImporterService;
        public List<Contact> Contacts { get; set; }
        public ViewContactModel()
        {
            _iDataImporterService = Startup.AutofacContainer.Resolve<IDataImporterService>();
        }
        public ViewContactModel(IDataImporterService iDataImporterService)
        {
            _iDataImporterService = iDataImporterService;
        }
    
        internal List<Contact> GetContactList()
        {
            Contacts = _iDataImporterService.GetContactList();
           
           
            return Contacts;
        }
    }
}
