using Autofac;
using DataImporter.Info.Business_Object;
using DataImporter.Info.Services;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataImporter.Areas.User.Models
{
    public class ExportFileModel
    {
        private readonly IDataImporterService _iDataImporterService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGroupServices _groupServices;
        private readonly IExportServices _exportServices;
        public int GroupId { get; set; }
        public DateTime ExportDate{ get; set; }
        public List<string> Headers { get; set; }      
        public List<List<string>> Items { get; set; }

        public ExportFileModel()
        { 
            _iDataImporterService = Startup.AutofacContainer.Resolve<IDataImporterService>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
            _groupServices = Startup.AutofacContainer.Resolve<IGroupServices>();
            _exportServices = Startup.AutofacContainer.Resolve<IExportServices>();
        }
        public ExportFileModel(IDataImporterService iDataImporterService , 
            IHttpContextAccessor httpContextAccessor, IGroupServices groupServices, IExportServices exportServices)
        {
            _iDataImporterService = iDataImporterService;
            _httpContextAccessor = httpContextAccessor;
            _groupServices = groupServices;
            _exportServices = exportServices;

        }

        internal void GetContactsList(int groupId)
        {
            if (groupId>0)
            {
                var contacts = _iDataImporterService.ContactList(groupId);
                Headers = contacts.Item1;
                Items = contacts.Item2;
                GroupId = groupId;
            }

         
        }
        internal void GetContactsListByDate(int groupId)
        {
          
                var contacts = _iDataImporterService.ContactListByDate(groupId , ExportDate);
                Headers = contacts.Item1;
                Items = contacts.Item2;
                GroupId = groupId;
            
        }
        internal MemoryStream GetExportFiles()
        {

            //start exporting to excel
            var stream = new MemoryStream();
            
            using (var excelPackage = new ExcelPackage(stream))
            {
                //define a worksheet
                var worksheet = excelPackage.Workbook.Worksheets.Add("Users");

                for (int i = 1; i <= Headers.Count; i++)
                {
                    var r = 1;
                    worksheet.Cells[r, i].Value = Headers[i-1];
                    worksheet.Cells[r, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[r, i].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

                }

                //filling information
                
                for (int row = 0; row < Items.Count ; row++)
                {
                    List<string> values = new();
                    for (int col = 0; col < Headers.Count; col++)
                    {
                        worksheet.Cells[row+2, col+1].Value =Items[row][col];
                    }
                }
                excelPackage.Workbook.Properties.Title = "User list";
                excelPackage.Workbook.Properties.Author = "Robin";
                excelPackage.Save();

            }
            stream.Position = 0;
            return(stream);
        }



        internal List<Group> LoadAllGroups()
        {
            var id = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            return _groupServices.LoadAllGroups(id);
        }

        internal void GetExportFileHistory(int id)
        {
            var items = _exportServices.GetExportHistoryForDownload(id);
            GroupId = items.Item1;
            ExportDate = items.Item2;
        }
    }
}

