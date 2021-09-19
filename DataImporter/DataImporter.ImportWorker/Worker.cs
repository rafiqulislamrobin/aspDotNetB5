using DataImporter.Info.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataImporter.ImportWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IDataImporterService _iDataImporterService;
        public Worker(ILogger<Worker> logger, IDataImporterService iDataImporterService)
        {
            _iDataImporterService = iDataImporterService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var fileinfo = _iDataImporterService.CheckImportStatus();
               var message = _iDataImporterService.SaveExcelDatatoDb(fileinfo.Item1, fileinfo.Item2, fileinfo.Item3, fileinfo.Item4);
                _logger.LogInformation(message);
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(60000, stoppingToken);
            }

        }
    }
}
