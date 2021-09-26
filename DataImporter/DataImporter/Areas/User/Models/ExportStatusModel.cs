using Autofac;
using DataImporter.Common.Utility;
using DataImporter.Info.Business_Object;
using DataImporter.Info.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.User.Models
{
    public class ExportStatusModel
    {

        public string DownloadStatus { get; set; }
        public string EmailStatus { get; set; }
        public DateTime Date { get; set; }
        public int GroupId { get; set; }
        public int Id { get; set; }

        public IDataImporterService _Importservice;
        public IDatetimeUtility _dateTimeUtility;
        public ExportStatusModel()
        {
            _dateTimeUtility = Startup.AutofacContainer.Resolve<IDatetimeUtility>();
            _Importservice = Startup.AutofacContainer.Resolve<IDataImporterService>();
        }
        public ExportStatusModel(IDataImporterService iDataImporterService, IDatetimeUtility dateTimeUtility)
        {
            _Importservice = iDataImporterService;
            _dateTimeUtility = dateTimeUtility;
        }
        internal void MakeStatus(int groupId , string statusUpdate)
        {
            var exportHistory = _Importservice.GetExportHistory(groupId);
            


                
                    ExportStatus exportStatus = new();
                    exportStatus.EmailStatus = "sent";                  
                    exportStatus.DateTime = _dateTimeUtility.Now;
                    exportStatus.GroupId = groupId;
                    _Importservice.SaveExportHistory(exportStatus);
                

           
        }
    }
}
