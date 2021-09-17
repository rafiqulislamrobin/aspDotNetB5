using Autofac;
using DataImporter.Info.Business_Object;
using DataImporter.Info.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.User.Models
{
    public class ExportFileModel
    {
        private readonly IDataImporterService _iDataImporterService;
        public List<Contact> Contacts { get; set; }
        public ExportFileModel()
        {
            _iDataImporterService = Startup.AutofacContainer.Resolve<IDataImporterService>();
        }
        public ExportFileModel(IDataImporterService iDataImporterService)
        {
            _iDataImporterService = iDataImporterService;
        }


        internal MemoryStream GetExportFiles()
        {
            Contacts = _iDataImporterService.GetContactList();
            //start exporting to excel
            var stream = new MemoryStream();
            
            using (var excelPackage = new ExcelPackage(stream))
            {
                //define a worksheet
                var worksheet = excelPackage.Workbook.Worksheets.Add("Users");

                var StartRow = 5;
                var row = StartRow;

                worksheet.Cells["A1"].Value = "User Export File";
                using (var r = worksheet.Cells["A1:C1"])
                {
                    r.Merge = true;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(78, 194, 109));
                }
                worksheet.Cells["A4"].Value = "Name";
                worksheet.Cells["B4"].Value = "Address";
                worksheet.Cells["C4"].Value = "Group Id";
                worksheet.Cells["A4:C4"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A4:C4"].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

                //filling information
                row = 5;
                foreach (var contact in Contacts)
                {
                    worksheet.Cells[row, 1].Value = contact.Name;
                    worksheet.Cells[row, 2].Value = contact.Address;
                    worksheet.Cells[row, 3].Value = contact.GroupId;
                    row++;
                }
                excelPackage.Workbook.Properties.Title = "User list";
                excelPackage.Workbook.Properties.Author = "Robin";
                excelPackage.Save();

            }
            stream.Position = 0;
            return(stream);
        }
    }
}

