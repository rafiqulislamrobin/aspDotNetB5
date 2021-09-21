using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Info.Business_Object
{
   public class ExportStatus
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string DownloadStatus { get; set; }
        public string EmailStatus { get; set; }
        public string GroupName { get; set; }
        public DateTime DateTime { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        //public List<FilePath> FilePaths { get; set; }
    }
}
