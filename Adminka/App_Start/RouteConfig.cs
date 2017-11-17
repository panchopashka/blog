using System.Web.Mvc;
using System.Web.Routing;

namespace Adminka
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Category",
                "Category/{categorySlug}",
                new { controller = "Blog", action = "Category" }
            );

            routes.MapRoute(
                "Tag",
                "Tag/{tagSlug}",
                new { controller = "Blog", action = "Tag" }
            );

            routes.MapRoute(
                "Login",
                "Login",
                new { controller = "Admin", action = "Login" }
            );

            routes.MapRoute(
                "Logout",
                "Logout",
                new { controller = "Admin", action = "Logout" }
            );

            routes.MapRoute(
                "Manage",
                "Manage",
                new { controller = "Admin", action = "Manage" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Blog", action = "Posts", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                "Post",
                "Archive/{year}/{month}/{title}",
                new { controller = "Blog", action = "Post" }
            );
        }
    }
}
