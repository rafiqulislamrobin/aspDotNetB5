﻿using Autofac;
using ClosedXML.Excel;
using DataImporter.Info.Services;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace DataImporter.Areas.User.Models
{
    public class EmailSenderModel
    {
        [Required]
        public string Email { get; set; }
        public int GroupId { get; set; }

        public List<string> Headers { get; set; }
        public List<List<string>> Items { get; set; }
        private IDataImporterService _iDataImporterService;
        private ILogger<EmailSenderModel> _logger;
        private ILifetimeScope _scope;
        public EmailSenderModel()
        {
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _iDataImporterService = _scope.Resolve<IDataImporterService>();
            _logger = _scope.Resolve<ILogger<EmailSenderModel>>();
        }
        public EmailSenderModel(IDataImporterService iDataImporterService, ILogger<EmailSenderModel> logger)
        {
            _iDataImporterService = iDataImporterService;
            _logger = logger;
        }


        public void SendEmail(string Email)
        {
            string filepath = ($"{Directory.GetCurrentDirectory()}{@"\wwwroot\ExcelFiles"}" + "\\" + "User.xlsx");
            MailMessage mail = new MailMessage();
            try
            {
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("kratosrobin467@gmail.com");
                mail.To.Add(Email); // Sending MailTo  
  
                mail.Subject = "Exported User Excel File";
                mail.Body = "Excel File *This is an automatically generated email, please do not reply*";
                System.Net.Mail.Attachment attachment;
                attachment = new Attachment(filepath); //Attaching File to Mail  
                mail.Attachments.Add(attachment);
                SmtpServer.Port = Convert.ToInt32("587"); //PORT  
                SmtpServer.EnableSsl = true;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new NetworkCredential("kratosrobin467@gmail.com", "letthewarbegin946");
                SmtpServer.Send(mail);

                if (mail.Attachments != null)
                {
                    for (var i = mail.Attachments.Count - 1; i >= 0; i--)
                    {
                        mail.Attachments[i].Dispose();
                    }
                    mail.Attachments.Clear();
                    mail.Attachments.Dispose();
                }
                mail.Dispose();
                mail = null;
                File.Delete($"{Directory.GetCurrentDirectory()}{@"\wwwroot\ExcelFiles"}" + "\\" + "User.xlsx");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to sent email");
            }
            
        }


        internal void GetData( int groupId)
        {
           
            var contacts = _iDataImporterService.ContactList(groupId);
            Headers = contacts.Item1;
            Items = contacts.Item2;
            GetExportFiles();
        }
        internal MemoryStream GetExportFiles()
        {

            //start exporting to excel
            var stream = new MemoryStream();

            using (var excelPackage = new ExcelPackage(stream))
            {
                //define a worksheet
                var worksheet = excelPackage.Workbook.Worksheets.Add("Users");

                //putting headers
                for (int i = 1; i <= Headers.Count; i++)
                {
                    var r = 1;
                    worksheet.Cells[r, i].Value = Headers[i - 1];
                    worksheet.Cells[r, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[r, i].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

                }

                //putting item in a rows
                for (int row = 0; row < Items.Count; row++)
                {
                    List<string> values = new();
                    for (int col = 0; col < Headers.Count; col++)
                    {
                        worksheet.Cells[row + 2, col + 1].Value = Items[row][col];
                    }
                }
                 excelPackage.Workbook.Properties.Title = "User list";
                 excelPackage.Workbook.Properties.Author = "Robin";

                //saving excel
                var filepath = ($"{Directory.GetCurrentDirectory()}{@"\wwwroot\ExcelFiles"}" + "\\" + "User.xlsx");
                 excelPackage.SaveAs(new FileInfo (filepath));

            }
            
            return (stream);
        }
    }
}
