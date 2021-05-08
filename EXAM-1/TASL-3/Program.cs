using System;
using System.Reflection;

namespace TASL_3
{
    class Program
    {
        static void Main(string[] args)
        {


            Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in types)
            {


                if (type.IsSubclassOf(typeof(BaseModel)))
                {
                    Console.WriteLine(type.Name);
                }

            }
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
