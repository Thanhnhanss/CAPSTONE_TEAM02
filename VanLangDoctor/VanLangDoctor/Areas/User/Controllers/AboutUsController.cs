using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.User.Controllers
{
	public class AboutUsController : Controller
    {
        //GET: User/AboutUs
        public ActionResult About()
        {
            return View();
        }
    }
}