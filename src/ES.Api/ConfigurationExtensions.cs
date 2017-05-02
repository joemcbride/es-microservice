using System;
using Microsoft.Extensions.Configuration;

namespace ES.Api
{
    public static class ConfigurationExtensions
    {
        public static T Get<T>(this IConfigurationRoot root, string sectionName = null)
            where T : class, new()
        {
            var type = typeof(T);
            return Get(root, type, sectionName ?? type.Name) as T;
        }

        public static object Get(this IConfigurationRoot configuration, Type type, string sectionName = null)
        {
            var section = configuration.GetSection(sectionName ?? type.Name);
            var instance = Activator.CreateInstance(type);
            section.Bind(instance);
            return instance;
        }
    }
}
