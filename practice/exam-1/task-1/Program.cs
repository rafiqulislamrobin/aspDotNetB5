using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Threading.Tasks;

namespace TASK_1
{
    class Program
    {
        static void Main(string[] args)
        {

            IList<string> pathlist = new List<string>();

            var path1 = @"D:\ASPDOTNET\aspDotNetB5\t1.txt";
            pathlist.Add(path1);
            var path2 = @"D:\ASPDOTNET\aspDotNetB5\t2.txt";
            pathlist.Add(path2);



            var fileprocess = new FileProcessor();
            fileprocess.ReadFiles(pathlist);

            for (int i = 0; i < pathlist.Count; i += 2)
            {
                var thread1 = new Thread(new ThreadStart(() => fileprocess.readFileMethod(i, pathlist)));
                var thread2 = new Thread(new ThreadStart(() => fileprocess.readFileMethod(i + 1, pathlist)));
                thread1.Start();
                thread2.Start();
                if (i + 1 == pathlist.Count - 1)
                {
                    break;
                }
            }


            //foreach (var t in pathlist)
            //{
            //    StreamReader sr = new StreamReader(t);
            //    string data = sr.ReadLine();
            //    while (data!=null)
            //    {
            //        Console.WriteLine(data);
            //        //string[] splits = data.Split(',');
            //        //int frequency = int.Parse(splits[0]);
            //        //int duration = int.Parse(splits[1]);
            //        //Console.Beep(frequency, duration);
            //        data = sr.ReadLine();
            //    }
            //}

        }
    }
}
