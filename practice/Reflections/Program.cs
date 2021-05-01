using System;
using System.IO;
using System.Reflection;

namespace Reflections
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"D:\ASPDOTNET\practice\Reflections\config.txt";
            var configText = File.ReadAllText(path);

            var initializer = configText.Split('=')[1].Trim();

            Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var type  in types)
            {
                if (type.Name ==initializer)
                {
                    ConstructorInfo constructor = type.GetConstructor(new Type[0]);
                    var initializerInstance = constructor.Invoke(new object[0]);
                }
            }
        }
    }
}
