using System.Net.Http.Formatting;
using System.Web.Http;
using Owin;

namespace Vapour.API.Owin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureWebApi(app);
        }

        private void ConfigureWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Formatters.Add(new BsonMediaTypeFormatter());
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);
        }
    }
}