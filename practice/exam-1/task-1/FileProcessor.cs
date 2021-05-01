using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
namespace TASK_1
{
    public class FileProcessor
    {
        public IList<string> ReadFiles(IList<string> fileNames)
        {
            IList<string> pathlist = new List<string>();

            for (int i = 0; i < fileNames.Count; i++)
            {




                pathlist.Add(fileNames[i]);



            }



            //var path1 = fileNames[0];
            //pathlist.Add(path1);
            //var path2 = fileNames[1];
            //pathlist.Add(path2);
            return pathlist;

        }
        public void readFileMethod(int a, IList<string> numberofPath)
        {
            int b = a;
            IList<string> pathlist = numberofPath;
            for (int i = b; i == b; i++)
            {


                lock (pathlist)
                {

                    var z = File.ReadAllText(pathlist[i]);
                    var t = pathlist[i];
                    {
                        StreamReader sr = new StreamReader(t);
                        string data = sr.ReadLine();
                        while (data != null)
                        {
                            Console.WriteLine(data);
                            string[] splits = data.Split(',');
                            int frequency = int.Parse(splits[0]);
                            int duration = int.Parse(splits[1]);
                            Console.Beep(frequency, duration);
                            data = sr.ReadLine();
                        }
                    }

                }

            }
        }
    }
}
