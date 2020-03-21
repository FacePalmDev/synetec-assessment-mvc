using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Serilog;

namespace InterviewTestTemplatev2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Note: Logs are written to a rolling log file that does not continually grow beyond a specified capacity. 
            // I've gone with the default values though this could be configured.

            var appSettings = ConfigurationManager.AppSettings;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();
                
            Log.Error($"Synetec Mvc Assessment Started at UTC: { DateTime.Now.ToUniversalTime() } ");
            Log.CloseAndFlush();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            //Catch Exception in Central place
            var exception = Server.GetLastError();
            
            // We'll log errors that are caught here as fatal as they caused a 500 response.
            Serilog.Log.Fatal(exception.Message);
  
        }
    }
}
