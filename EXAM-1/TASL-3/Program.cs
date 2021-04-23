using System;
using System.Reflection;

namespace TASL_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Type y = typeof(BaseModel);
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach ( var type in types)
            {
                if (type != typeof(BaseModel))
                {
                    Console.WriteLine(type.Name);
                }
                
            }
        }
    }
    public abstract class BaseModel
    {
    }
    public class AnotherClass :BaseModel
    {

    }
    public class AnotherClass2 : BaseModel
    {

    }


}
