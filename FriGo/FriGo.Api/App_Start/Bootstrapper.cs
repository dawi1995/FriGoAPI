using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using FriGo.Db.Models;
using FriGo.Interfaces.Dependencies;
using FriGo.Services;
using FriGo.Db.Models.Social;
using FriGo.Db.DTO.Social;
using FriGo.Db.Models.Recipes;

namespace FriGo.Api
{
    public class Bootstrapper
    {        
        public static void Run()
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            IEnumerable<Assembly> otherAssemblies = GetFriGoAssemblies();            
        
            var builder = new ContainerBuilder();
            HttpConfiguration config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(executingAssembly);
            builder.RegisterWebApiFilterProvider(config);

            RegisterTypes(otherAssemblies, builder);

            RegisterAutoMapper(builder);

            IContainer container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IEnumerable<Assembly> GetFriGoAssemblies()
        {
            List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => assembly.FullName.StartsWith(Properties.Resources.AppName))
                .ToList();

            return assemblies;
        }

        private static void RegisterTypes(IEnumerable<Assembly> assemblies, ContainerBuilder builder)
        {
            foreach (Assembly assembly in assemblies)
            {
                RegisterRequestDependencies(builder, assembly);
                RegisterLifeTimeDependencies(builder, assembly);
                RegisterSingleInstanceDependencies(builder, assembly);
                RegisterMatchingLifeTimeDependency(builder, assembly);
                RegisterSelfDependency(builder, assembly);
            }
        }

        private static void RegisterRequestDependencies(ContainerBuilder builder, Assembly assembly)
        {
            IEnumerable<Type> types = assembly.GetTypes().Where(t => typeof(IRequestDependency).IsAssignableFrom(t)).ToList();

            builder.RegisterTypes(types.ToArray()).AsImplementedInterfaces().InstancePerRequest();
        }

        private static void RegisterLifeTimeDependencies(ContainerBuilder builder, Assembly assembly)
        {
            IEnumerable<Type> types = assembly.GetTypes().Where(t => typeof(ILifeTimeDependency).IsAssignableFrom(t)).ToList();

            builder.RegisterTypes(types.ToArray()).AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        private static void RegisterSingleInstanceDependencies(ContainerBuilder builder, Assembly assembly)
        {
            IEnumerable<Type> types = assembly.GetTypes().Where(t => typeof(ISingleInstanceDependency).IsAssignableFrom(t)).ToList();

            builder.RegisterTypes(types.ToArray()).AsImplementedInterfaces().SingleInstance();
        }

        private static void RegisterMatchingLifeTimeDependency(ContainerBuilder builder, Assembly assembly)
        {
            IEnumerable<Type> types = assembly.GetTypes().Where(t => typeof(IMatchingLifeTimeDependency).IsAssignableFrom(t)).ToList();

            builder.RegisterTypes(types.ToArray()).AsImplementedInterfaces().InstancePerMatchingLifetimeScope();
        }

        private static void RegisterSelfDependency(ContainerBuilder builder, Assembly assembly)
        {
            IEnumerable<Type> types = assembly.GetTypes().Where(t => typeof(ISelfRequestDependency).IsAssignableFrom(t)).ToList();

            builder.RegisterTypes(types.ToArray()).AsSelf().InstancePerRequest();
        }

        private static void RegisterAutoMapper(ContainerBuilder builder)
        {
            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.CreateMissingTypeMaps = true;

                configuration.CreateMap<KeyValuePair<Recipe, decimal>, Recipe>()
                    .ConvertUsing(src => src.Key);

            });
            IMapper mapper = mapperConfiguration.CreateMapper();

            builder.RegisterInstance(mapper).As<IMapper>();
        }
    }
}