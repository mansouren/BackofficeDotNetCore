using AutoMapper;
using Dto.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static void InitializeAutomapper(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddAutoMapper(config =>
            {
                config.AddCustomMappingProfile();
            }, assemblies);
        }

        public static void AddCustomMappingProfile(this IMapperConfigurationExpression configuration)
        {
            var assembly = typeof(IHaveCustomMapping).Assembly;
            configuration.AddCustomMappingProfile(assembly);
        }

        public static void AddCustomMappingProfile(this IMapperConfigurationExpression configuration, params Assembly[] assemblies)
        {
            var allTypes = assemblies.SelectMany(a => a.ExportedTypes);
            var list = allTypes.Where(type => type.IsClass && !type.IsAbstract && type.GetInterfaces().Contains(typeof(IHaveCustomMapping)))
                .Select(type => (IHaveCustomMapping)Activator.CreateInstance(type));
            var profile = new CustomMappingProfile(list);
            configuration.AddProfile(profile);
        }
    }
}
