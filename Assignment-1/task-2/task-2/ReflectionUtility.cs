using System;
using System.Reflection;

namespace task_2
{
    public class ReflectionUtility
    {
        public void CallPrivate(object targetObject, string methodName, object[] args)
        {
            var method = typeof(ClassWithPrivateMethod).GetMethod(
                           methodName,
                           BindingFlags.NonPublic
                         | BindingFlags.Instance
                         | BindingFlags.DeclaredOnly);

            method.Invoke(targetObject, args);



        }  
    }

}
