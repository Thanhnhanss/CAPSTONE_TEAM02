using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VanLangDoctor.Areas.User.Controllers
{
    public class AboutUsController : Controller
    {
        // GET: User/AboutUs
        public ActionResult About()
        {
            return View();
        }
    }
}