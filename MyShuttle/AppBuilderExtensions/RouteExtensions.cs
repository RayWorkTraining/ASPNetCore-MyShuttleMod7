namespace MyShuttle.Web.AppBuilderExtensions
{
    using Microsoft.AspNetCore.Builder;

    public static class RouteExtensions
    {

        public static IApplicationBuilder ConfigureRoutes(this IApplicationBuilder app)
        {
            return app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "DriverCountWithFilter", 
                //    template: "api/drivers/{filter}", 
                //    defaults: new { controller = "Drivers", action = "count" });

                //routes.MapRoute(
                //    name: "defaultWithPrefix", 
                //    template: "api/{controller}/{action}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}

