using Autofac;
using DataImporter.Info.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataImporter.Areas.User.Models
{
    public class IndexModel
    {
        public int TotalGroups { get; set; }
        public int TotalExports { get; set; }
        public int TotalImports { get; set; }
        public IDataImporterService _iDataImporterService { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel()
        {
            _iDataImporterService = Startup.AutofacContainer.Resolve<IDataImporterService>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();


        }
        public IndexModel(IDataImporterService iDataImporterService, IHttpContextAccessor httpContextAccessor)
        {
            _iDataImporterService = iDataImporterService;
            _httpContextAccessor = httpContextAccessor;
        }
        public void GetTotal()
        {
            var id = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            TotalGroups = _iDataImporterService.LoadAllGroups(id).Count;
            TotalExports = _iDataImporterService.LoadAllExportHistory(id).Count;
            TotalImports= _iDataImporterService.LoadAllImportHistory(id).Count;



        }
    }
}
