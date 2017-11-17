using Admin.Core.EntityFramework;
using Blog.Core.EntityFramework;
using Blog.Core.Stores;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Resolution;

namespace Adminka.Util
{
    public class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here  
            //This is the important line to edit  

            container.RegisterType<AdminDbContext>();
            container.RegisterType<IBlogRepository, BlogRepository>(new InjectionFactory(c=>new BlogDbContext()));

            RegisterTypes(container);
            return container;
        }
        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}