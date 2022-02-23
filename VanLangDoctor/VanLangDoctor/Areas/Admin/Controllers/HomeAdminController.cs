using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        public ActionResult HomeAdmin()
        {
            return View();
        }
    }
}