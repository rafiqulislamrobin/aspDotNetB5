using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK_1
{
    public class FileProcessor
    {
        public IList<string> ReadFiles(IList<string> fileNames)
        {
            IList<string> pathlist =new List<string>() ;

            var path1 = fileNames[0];
            pathlist.Add(path1);
            var path2 = fileNames[1];
            pathlist.Add(path2);
            return pathlist;
         
        }
    }
}
