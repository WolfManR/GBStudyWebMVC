using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace DataBase.EF
{
    // TODO: Change behavior to use Expression Tree
    public static class EntityFrameworkExtensions
    {
        public static ModelBuilder ApplyConfigurationForCurrentContext(this ModelBuilder builder, Assembly assembly, DbContext context)
        {
            builder.ApplyConfigurationsFromAssembly(assembly, type => NeededType(type, GetSupportedTypes(context)));
            return builder;
        }

        private static HashSet<Type> GetSupportedTypes(DbContext context)
        {
            var properties = context.GetType().GetProperties();
            var result = new HashSet<Type>();
            foreach (var property in properties)
            {
                if (property.PropertyType is { IsGenericType: true } prop && prop.GetGenericTypeDefinition() == typeof(DbSet<>))
                {
                    result.Add(prop.GenericTypeArguments[0]);
                }
            }
            return result;
        }

        private static bool NeededType(Type type, HashSet<Type> supportedTypes)
        {
            var interfaces = type.GetInterfaces();
            for (var i = 0; i < interfaces.Length; i++)
            {
                var @interface = type.GetInterfaces()[i];
                if (@interface is { IsGenericType: true } && @interface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) && supportedTypes.Contains(@interface.GenericTypeArguments[0]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}