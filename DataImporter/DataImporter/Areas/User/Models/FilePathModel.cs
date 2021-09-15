using Autofac;
using AutoMapper;
using DataImporter.Common.Utility;
using DataImporter.Info.Business_Object;
using DataImporter.Info.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.User.Models
{
    public class FilePathModel
    {

        public IWebHostEnvironment _WebHostEnvironment;
        //public DateTime DateTime { get; set; }
        public int GroupId { get; set; }

        public List<Group> groups  { get; set; }
       public IDataImporterService _iDataImporterService;

        public IDatetimeUtility _datetimeUtility;
        public FilePathModel()
        {
            _datetimeUtility = Startup.AutofacContainer.Resolve<IDatetimeUtility>();
            _WebHostEnvironment = Startup.AutofacContainer.Resolve<IWebHostEnvironment>();
            _iDataImporterService = Startup.AutofacContainer.Resolve<IDataImporterService>();
     
        }
        public FilePathModel(IDataImporterService iDataImporterService,  IWebHostEnvironment WebHostEnvironment, IDatetimeUtility datetimeUtility)
        {
            _WebHostEnvironment = WebHostEnvironment;
            _datetimeUtility = datetimeUtility;
            _iDataImporterService = iDataImporterService;
        }
        internal async Task SaveFilePathAsync(IFormFile file ,int groupId)
        {
            string fileTxt = Path.GetExtension(file.FileName);
            if (fileTxt == ".xls" || fileTxt == ".xlss" || fileTxt == ".application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                var savingExcel = Path.Combine(_WebHostEnvironment.WebRootPath, "Excel", file.FileName);
                var stream = new FileStream(savingExcel, FileMode.Create);
                await file.CopyToAsync(stream);


                //filePath.FilePathName = savingExcel;
                FilePath filePath = new FilePath();
                filePath.FileName = file.FileName;
                filePath.FilePathName = savingExcel;
                filePath.DateTime = _datetimeUtility.Now;
                filePath.GroupId = groupId;
                _iDataImporterService.SaveFilePath(filePath);
            }




            
        }

        internal List<Group> LoadAllGroups()
        {
            return _iDataImporterService.LoadAllGroups();
        }
    }
}
