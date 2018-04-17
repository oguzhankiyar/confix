using OK.Confix.Samples.Mvc.Helpers;
using System.Web.Mvc;

namespace OK.Confix.Samples.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string key)
        {
            string value = ConfigurationHelper.Confix.Get<string>(key);

            return Content(value);
        }
    }
}