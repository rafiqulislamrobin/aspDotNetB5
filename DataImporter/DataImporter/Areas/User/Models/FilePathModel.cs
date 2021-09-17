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
        public int GroupName { get; set; }

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
        internal /*async Task*/void SaveFilePathAsync(string filename ,int groupId,List<Group> list)
        {

                FilePath filePath = new FilePath();
               var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\Excel"}" + "\\" + filename;
                filePath.FilePathName = path;
               filePath.FileName = Path.GetFileName(path);
                filePath.DateTime = _datetimeUtility.Now;
                filePath.GroupId = groupId;               
                foreach (var item in list)
                {
                    if (item.Id==groupId)
                    {
                        filePath.GroupName = item.Name;
                    }
                }     
               _iDataImporterService.SaveFilePath(filePath);  
        }

        internal List<Group> LoadAllGroups()
        {
            return _iDataImporterService.LoadAllGroups();
        }
    }
}
