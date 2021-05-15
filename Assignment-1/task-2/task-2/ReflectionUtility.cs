using System;
using System.Reflection;

namespace task_2
{
    public class ReflectionUtility
    {
        public void CallPrivate(object targetObject, string methodName, object[] args)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var objectType = targetObject.GetType();

            foreach (var item in types)
            {
                if (item.Name == objectType.Name)
                {
                    var method = item.GetMethod(
                              methodName,
                              BindingFlags.NonPublic
                            | BindingFlags.Instance
                            | BindingFlags.DeclaredOnly);

                    method.Invoke(targetObject, args);
                }

            }



        }
    }

}
