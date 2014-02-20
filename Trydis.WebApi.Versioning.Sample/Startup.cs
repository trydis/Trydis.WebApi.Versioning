using System.Web.Http;
using Owin;

namespace Trydis.WebApi.Versioning.Sample
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            VersionedMediaTypeConfig.RegisterTypedFormatters(config);
            VersionConstraint.MediaTypePattern = "^application/vnd.versioningsample.([a-zA-Z]+)-v([0-9]+)\\+json$";

            appBuilder.UseWebApi(config);
        }
    } 
}