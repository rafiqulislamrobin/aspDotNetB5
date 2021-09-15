using DataImporter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Info.Entities
{
    public class FilePath : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string FileName { get; set; }
        public string FilePathName { get; set; }
    }
}
