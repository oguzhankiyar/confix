using Microsoft.AspNetCore.Http;
using OK.Confix.WebUI.Api.Controllers;
using OK.Confix.WebUI.Helpers;
using System.IO;
using System.Threading.Tasks;

namespace OK.Confix.WebUI
{
    public class WebUIHandler
    {
        public WebUIHandlerSettings Settings { get; set; }

        public WebUIHandler()
        {
            Settings = new WebUIHandlerSettings();
        }

        internal void Build()
        {

        }

        internal async Task ProcessRequestAsync(HttpContext context)
        {
            string path = context.Request.Path.Value.Trim('/');
            string[] sections = path.Split('/');

            string first = sections.Length > 0 ? sections[0].ToString().ToLower() : null;
            string second = sections.Length > 1 ? sections[1].ToString().ToLower() : null;
            string third = sections.Length > 2 ? sections[2].ToString().ToLower() : null;

            string content = string.Empty;
            string contentType = string.Empty;

            string assetNamespace = "OK.Confix.WebUI.Assets";
            string viewNamespace = "OK.Confix.WebUI.Views";

            if (first == "api")
            {
                string body = new StreamReader(context.Request.Body).ReadToEnd();

                switch (second)
                {
                    case "applications":
                        content = new ApplicationsController(Settings.DataManager).Process(third, body);
                        break;
                    case "environments":
                        content = new EnvironmentsController(Settings.DataManager).Process(third, body);
                        break;
                    case "configurations":
                        content = new ConfigurationsController(Settings.DataManager).Process(third, body);
                        break;
                }

                contentType = "application/json";
            }
            else if (first == "scripts")
            {
                content = ResourceHelper.Read(assetNamespace + "." + second + ".js");

                contentType = "application/javascript";
            }
            else if (first == "styles")
            {
                content = ResourceHelper.Read(assetNamespace + "." + second + ".css");

                contentType = "text/css";
            }
            else if (!string.IsNullOrEmpty(first))
            {
                if (string.IsNullOrEmpty(second))
                {
                    content = ResourceHelper.Read(viewNamespace + "." + first + ".html");
                }
                else
                {
                    content = ResourceHelper.Read(viewNamespace + "." + first + "." + second + ".html");
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
                await context.Response.WriteAsync(content);
            }
            else
            {
                context.Response.StatusCode = 404;
            }

            return;
        }
    }
}