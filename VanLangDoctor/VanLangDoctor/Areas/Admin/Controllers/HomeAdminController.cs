﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    [Authorize(Roles = "Quản trị viên, Bác sĩ, Quản lý")]
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        public ActionResult HomeAdmin()
        {
            return View();
        }
    }
}