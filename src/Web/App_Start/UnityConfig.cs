using System.Configuration;
using System.Web.Http;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Unity.WebApi;

using Blog.Repository.Infrastructure;
using Blog.Service.Bridge;
using Blog.Service.Interface;
using Blog.Web.Integration;

namespace Blog.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            RegisterIntegration(container);
            RegisterRepositories(container);
            RegisterServices(container);
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void RegisterIntegration(IUnityContainer container)
        {
            container.RegisterType<ModelStateAccessor>(new HierarchicalLifetimeManager());
            container.RegisterType<IErrorState, ModelStateAdapter>(new HierarchicalLifetimeManager());
            container.RegisterType<ContextAccessor>(new HierarchicalLifetimeManager());
        }

        private static void RegisterRepositories(IUnityContainer container)
        {
            container.LoadConfiguration(ConfigurationManager.AppSettings["Repository.Strategy"]);
        }

        private static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());
        }
    }
}