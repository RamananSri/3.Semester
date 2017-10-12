using System.Web.Mvc;

namespace MVCClient.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string zip)
        {
            string zipode = zip;
            return View();
        }
    }
}