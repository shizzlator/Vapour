using System.Net.Http.Formatting;
using System.Web.Http;
using Owin;
using WebApiContrib.Formatting.Jsonp;

namespace Vapour.API
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
            config.AddJsonpFormatter();
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);
        }
    }
}