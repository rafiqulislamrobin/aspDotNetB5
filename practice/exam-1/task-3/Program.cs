using System;
using System.Reflection;
namespace task_3
{
    class Program
    {
        static void Main(string[] args)
        {

            Assembly assembly = Assembly.GetExecutingAssembly();

            Type type = typeof(BaseModel);

            foreach (var item in assembly.GetTypes())
            {

                if (item.BaseType.Name == type.Name)
                {
                    Console.WriteLine(item.Name);
                }
            }
            //    Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            //    foreach (Type type in types)
            //    {


            //        if ((type.IsSubclassOf(typeof(BaseModel))))
            //        {
            //            Console.WriteLine(type.Name);
            //        }

            //    }
        }
    }
    public class BaseModel
    {

    }
    public class AnotherClass : BaseModel
    {

    }
    public class AnotherClass2 : BaseModel
    {

    }
}
