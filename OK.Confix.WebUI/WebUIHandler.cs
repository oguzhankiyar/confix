using OK.Confix.WebUI.Api.Controllers;
using OK.Confix.WebUI.Helpers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OK.Confix.WebUI
{
    public class FakeController : Controller { }

    public class WebUIRouteHandler : IRouteHandler
    {
        private WebUIHandler _webUIHandler;

        public WebUIRouteHandler(WebUIHandler webUIHandler)
        {
            _webUIHandler = webUIHandler;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return _webUIHandler;
        }
    }

    public class WebUIHandler : IHttpHandler
    {
        public WebUIHandlerSettings Settings { get; set; }

        public bool IsReusable => false;

        public WebUIHandler()
        {
            Settings = new WebUIHandlerSettings();
        }

        internal void Build()
        {

        }

        public void ProcessRequest(HttpContext context)
        {
            var routeValues = context.Request.RequestContext.RouteData.Values;

            string first = routeValues["first"].ToString().ToLower();
            string second = routeValues["second"].ToString().ToLower();
            string third = routeValues["third"].ToString().ToLower();

            string content = string.Empty;
            string contentType = string.Empty;

            if (Settings == null || Settings.DataProvider == null)
            {
                context.Response.StatusCode = 404;

                context.Response.Flush();
                context.Response.Close();

                return;
            }

            if (first == "api")
            {
                string body = Encoding.UTF8.GetString(context.Request.BinaryRead(context.Request.ContentLength));

                switch (second)
                {
                    case "applications":
                        content = new ApplicationsController(Settings.DataProvider).Process(third, body);
                        break;
                    case "environments":
                        content = new EnvironmentsController(Settings.DataProvider).Process(third, body);
                        break;
                    case "configurations":
                        content = new ConfigurationsController(Settings.DataProvider).Process(third, body);
                        break;
                }

                contentType = "application/json";
            }
            else if (first == "scripts")
            {
                content = ResourceHelper.Read("OK.Confix.WebUI.Assets." + second + ".js");

                contentType = "application/javascript";
            }
            else if (first == "styles")
            {
                content = ResourceHelper.Read("OK.Confix.WebUI.Assets." + second + ".css");

                contentType = "text/css";
            }
            else if (!string.IsNullOrEmpty(first))
            {
                if (string.IsNullOrEmpty(second))
                {
                    content = ResourceHelper.Read("OK.Confix.WebUI.Views." + first + ".html");
                }
                else
                {
                    content = ResourceHelper.Read("OK.Confix.WebUI.Views." + first + "." + second + ".html");
                }

                contentType = "text/html";
            }
            else
            {
                content = "<meta http-equiv=\"refresh\" content=\"0; url=" + Settings.Path + "/index\" />";

                contentType = "text/html";
            }

            if (!string.IsNullOrEmpty(content))
            {
                context.Response.StatusCode = 200;
                context.Response.ContentType = contentType;
                context.Response.Write(content);
            }
            else
            {
                context.Response.StatusCode = 404;
            }

            context.Response.Flush();
            context.Response.Close();
        }
    }
}