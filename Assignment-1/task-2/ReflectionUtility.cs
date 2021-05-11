using System;
using System.Reflection;
namespace task_2
{
    public class ReflectionUtility
    {
        public void UsingReflection()
        {

            Type myType = (typeof(Car));                    //we can change the class name here to get private/protected method of any class

            MethodInfo[] methodInfos = myType.GetMethods(BindingFlags.NonPublic| BindingFlags.Instance|BindingFlags.DeclaredOnly);

            foreach (var item in methodInfos)
            {
                Console.WriteLine(item.Name);
            }

        }
    }
    
}
