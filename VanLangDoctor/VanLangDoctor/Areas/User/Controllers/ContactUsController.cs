using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.User.Controllers
{
    public class ContactUsController : Controller
    {
        //private CP24Team02Entities db = new CP24Team02Entities();

        //GET: User/ContactUs
        public ActionResult Contact()
        {
            return View();
        }
    }
}