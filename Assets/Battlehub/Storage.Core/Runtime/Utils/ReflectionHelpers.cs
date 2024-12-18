using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Battlehub.Storage
{
    public static class ReflectionHelpers
    {
        public static string GetAssemblyQualifiedName(Type type)
        {
            return CleanAssemblyName(type.AssemblyQualifiedName);
        }

        public static string CleanAssemblyName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }

            name = Regex.Replace(name, @", Version=\d+.\d+.\d+.\d+", string.Empty);
            name = Regex.Replace(name, @", Culture=\w+", string.Empty);
            name = Regex.Replace(name, @", PublicKeyToken=\w+", string.Empty);
            return name;
        }

        public static string GetFullNameWithoutGenericArity(Type t)
        {
            string name = t.FullName;
            int index = name.IndexOf('`');
            return index == -1 ? name : name.Substring(0, index);
        }

        public static Type[] GetAllTypesImplementingInterface(Type interfaceType)
        {
            var types = new List<Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                Type[] allAssemblyTypes;
                try
                {
                    allAssemblyTypes = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException e)
                {
                    allAssemblyTypes = e.Types;
                }

                var myTypes = allAssemblyTypes.Where(t => !t.IsAbstract && interfaceType.IsAssignableFrom(t));
                types.AddRange(myTypes);
            }
            return types.ToArray();
        }

        public static IEnumerable<Type> GetAllTypesImplementingOpenGenericType(Type openGenericType)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach(Type type in GetAllTypesImplementingOpenGenericType(openGenericType, assembly))
                {
                    yield return type;
                }
            }
        }

        public static IEnumerable<Type> GetAllTypesImplementingOpenGenericType(Type openGenericType, Assembly assembly)
        {
            return from x in assembly.GetTypes()
                   from z in x.GetInterfaces()
                   let y = x.BaseType
                   where
                   (y != null && y.IsGenericType &&
                   openGenericType.IsAssignableFrom(y.GetGenericTypeDefinition())) ||
                   (z.IsGenericType &&
                   openGenericType.IsAssignableFrom(z.GetGenericTypeDefinition()))
                   select x;
        }

        public static bool IsSurrogateOrValueTypeSurrogate(Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            var genericTypeDef = type.GetGenericTypeDefinition();
            return genericTypeDef == typeof(ISurrogate<>) ||
                   genericTypeDef == typeof(IValueTypeSurrogate<,>);
        }

        public static bool IsValueTypeSurrogate(Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            var genericTypeDef = type.GetGenericTypeDefinition();
            return genericTypeDef == typeof(IValueTypeSurrogate<,>);
        }
    }
}

