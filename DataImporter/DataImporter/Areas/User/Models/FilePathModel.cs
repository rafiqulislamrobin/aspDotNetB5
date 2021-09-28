﻿using Autofac;
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
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataImporter.Areas.User.Models
{
    public class FilePathModel
    {
        public string file { get; set; }

        //public DateTime DateTime { get; set; }
      
        public int GroupId { get; set; }
        public int GroupName { get; set; }

        public List<Group> groups { get; set; }
        public IDataImporterService _iDataImporterService;
        public  IGroupServices _groupServices;
        public IDatetimeUtility _datetimeUtility;
        public IHttpContextAccessor _httpContextAccessor;
        private ILifetimeScope _scope;
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _datetimeUtility = _scope.Resolve<IDatetimeUtility>();
            _httpContextAccessor = _scope.Resolve<IHttpContextAccessor>();
            _iDataImporterService = _scope.Resolve<IDataImporterService>();
            _groupServices = _scope.Resolve<IGroupServices>();
        }
        public FilePathModel()
        {
        }
        public FilePathModel(IDataImporterService iDataImporterService, IDatetimeUtility datetimeUtility ,
            IHttpContextAccessor httpContextAccessor ,IGroupServices groupServices)
        {
            
            _datetimeUtility = datetimeUtility;
            _iDataImporterService = iDataImporterService;
            _httpContextAccessor = httpContextAccessor;
            _groupServices = groupServices;
        }
        internal /*async Task*/void SaveFilePath(string fileName, int groupId, List<Group> list)
        {
           
            FilePath filePath = new FilePath();
            var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\ExcelFiles"}" + "\\" + fileName;
            filePath.FilePathName = path;
            filePath.FileName = Path.GetFileName(path);
            filePath.DateTime = _datetimeUtility.Now;
            filePath.GroupId = groupId;
            filePath.FileStatus = "Pending";
            foreach (var item in list)
            {
                if (item.Id == groupId)
                {
                    filePath.GroupName = item.Name;
                }
            }
            _iDataImporterService.SaveFilePath(filePath);
        }

        internal List<Group> LoadAllGroups()
        {
            
            var id = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            return _groupServices.LoadAllGroups(id);
        }

        internal void CancelImport(string fileName)
        {
            File.Delete($"{Directory.GetCurrentDirectory()}{@"\wwwroot\ExcelFiles"}" + "\\" + fileName);
        }
    }
}
