﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VanLangDoctor.Areas.User.Controllers
{
    public class HomeUserController : Controller
    {
        //[Route("trang-chu")]
        // GET: User/HomeUser
        public ActionResult HomeUser()
        {
            return View();
        }
    }
}