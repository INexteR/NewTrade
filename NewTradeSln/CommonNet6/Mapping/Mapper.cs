using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Mapping
{
    public static class Mapper
    {
        public static T Parse<T>(this string line, params string[] propertyNames)
            where T : new()
            => Parse<T>(line, '\t', propertyNames);

        public static T Parse<T>(this string line, char fieldSeparator, params string[] propertyNames)
            where T : new()
        {
            Type type = typeof(T);
            var converterSetters = GetConverterSetters(type);

            string[] props = line.Split(fieldSeparator, StringSplitOptions.TrimEntries);

            T item = new();
            for (int j = 0; j < props.Length; j++)
            {
                var name = propertyNames[j];
                var (converter, setter) = converterSetters[name];
                var prop = props[j];
                var value = converter.ConvertFromInvariantString(prop)!;
                setter.Invoke(item, new object[] { value });
            }

            return item;
        }


        public static IEnumerable<T> LinesToItems<T>(this IEnumerable<string> lines, params string[] propertyNames)
            where T : new()
        {
            foreach (var line in lines)
            {
                yield return Parse<T>(line, propertyNames);
            }
        }

        public static IEnumerable<T> LinesToItems<T>(this IEnumerable<string> lines, char fieldSeparator, params string[] propertyNames)
            where T : new()
        {
            foreach (var line in lines)
            {
                yield return Parse<T>(line, fieldSeparator, propertyNames);
            }
        }

        public static T[] ParseToArray<T>(this string text, char fieldSeparator, char[] lineSeparators, params string[] propertyNames)
           where T : new()
        {
            var lines = text.Split(lineSeparators, StringSplitOptions.RemoveEmptyEntries);
            return LinesToItems<T>(lines, fieldSeparator, propertyNames).ToArray();
        }

        public static T[] ParseToArray<T>(this string text, char[] lineSeparators, params string[] propertyNames)
           where T : new()
        {
            var lines = text.Split(lineSeparators, StringSplitOptions.RemoveEmptyEntries);
            return LinesToItems<T>(lines, propertyNames).ToArray();
        }

        public static T[] ParseToArray<T>(this string text, char fleldSeparator, params string[] propertyNames)
            where T : new()
            => ParseToArray<T>(text, fleldSeparator, "\r\n".ToCharArray(), propertyNames);

        public static T[] ParseToArray<T>(this string text, params string[] propertyNames)
            where T : new()
            => ParseToArray<T>(text, "\r\n".ToCharArray(), propertyNames);

        public static IReadOnlyDictionary<string, (TypeConverter converter, MethodInfo setter)> GetConverterSetters(Type type)
        {
            if (!typeSetters.TryGetValue(type, out ReadOnlyDictionary<string, (TypeConverter converter, MethodInfo setter)>? converterSetters))
            {
                PropertyInfo[] propertiesInfo = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                Dictionary<string, (TypeConverter converter, MethodInfo setter)> _converterSetters = new(propertiesInfo.Length);
                converterSetters = new ReadOnlyDictionary<string, (TypeConverter converter, MethodInfo setter)>(_converterSetters);

                var propertyDescriptors = TypeDescriptor.GetProperties(type);
                foreach (var property in propertiesInfo)
                {
                    if (property.CanWrite)
                    {
                        var converter = propertyDescriptors[property.Name]!.Converter;
                        _converterSetters.Add(property.Name, (converter, property.GetSetMethod(true)!));
                    }
                }
                typeSetters.Add(type, converterSetters);
            }
            return converterSetters;
        }

        private static readonly Dictionary<Type, ReadOnlyDictionary<string, (TypeConverter converter, MethodInfo setter)>> typeSetters = new();
        private static readonly Dictionary<Type, ReadOnlyDictionary<string, (TypeConverter converter, MethodInfo setter)>> typeGetters
            = new();

        public static IReadOnlyDictionary<string, (TypeConverter converter, MethodInfo setter)> GetConverterGetters(Type type)
        {
            if (!typeGetters.TryGetValue(type, out ReadOnlyDictionary<string, (TypeConverter converter, MethodInfo setter)>? converterGetters))
            {
                PropertyInfo[] propertiesInfo = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                Dictionary<string, (TypeConverter converter, MethodInfo setter)> _converterSetters = new(propertiesInfo.Length);
                converterGetters = new ReadOnlyDictionary<string, (TypeConverter converter, MethodInfo setter)>(_converterSetters);

                PropertyDescriptorCollection propertyDescriptors = TypeDescriptor.GetProperties(type);
                foreach (var property in propertiesInfo)
                {
                    if (property.CanRead && !(Type.GetTypeCode(property.PropertyType) is TypeCode.DBNull or TypeCode.Object))
                    {
                        var converter = propertyDescriptors[property.Name]!.Converter;
                        _converterSetters.Add(property.Name, (converter, property.GetSetMethod(true)!));
                    }
                }
                typeSetters.Add(type, converterGetters);
            }
            return converterGetters;
        }


        //public static TResult CreateFrom<Tsource, TResult>(this in Tsource source)
        //    where TResult : new()
        //{

        //}


    }
}
