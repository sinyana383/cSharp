using System;
using System.Reflection;

namespace d07_ex03
{
    public class TypeFactory<T> where T: class
    {
        public static T CreateWithConstructor()
        {
            var type = typeof(T);
            ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, 
                null, Type.EmptyTypes, null);
            if (constructor is null)
                return null;
            return (T)constructor.Invoke(null);
        }

        public static T CreateWithActivator()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        public static T CreateWithParameters(object[] parameters)
        {
            return (T)Activator.CreateInstance(typeof(T), parameters);
        }
    }
}