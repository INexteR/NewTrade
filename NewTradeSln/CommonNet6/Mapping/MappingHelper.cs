using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mapping
{
    public static class MappingHelper
    {
        public static T[] LinesToArray<T>(this string text, params string[] propertyName)
            where T : new()
        {
            Type type = typeof(T);
            if (!typeProperties.TryGetValue(type, out var properties))
            {
                PropertyInfo[] propertiesInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                properties = new(propertiesInfo.Length);
                var propertyDescriptors = TypeDescriptor.GetProperties(type);
                foreach (var property in propertiesInfo)
                {
                    if (property.CanWrite)
                    {
                        var converter = propertyDescriptors[property.Name]!.Converter;
                        properties.Add(property.Name, (converter, property.GetSetMethod(true)!));
                    }
                }
                typeProperties.Add(type, properties);
            }

            var lines = text.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            T[] array = new T[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] props = line.Split('\t', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                T item = new();
                for (int j = 0; j < props.Length; j++)
                {
                    var name = propertyName[j];
                    var property = properties[name];
                    var prop = props[j];
                    var value = property.converter.ConvertFromInvariantString(prop)!;
                    property.setter.Invoke(item, new object[] { value });
                }
                array[i] = item;
            }

            return array;
        }

        private static readonly Dictionary<Type, Dictionary<string, (TypeConverter converter, MethodInfo setter)>> typeProperties = new();
    }
}
