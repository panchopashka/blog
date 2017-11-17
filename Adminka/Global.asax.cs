using Adminka.Util;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Blog.Core.Models;
using Blog.Core.Stores;
using Unity;

namespace Adminka
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = (UnityContainer)Bootstrapper.Initialise();
            //var blogRepository = container.Resolve<IBlogRepository>();
            //ModelBinders.Binders.Add(typeof(Post), new PostModelBinder(blogRepository));
        }
    }
}
