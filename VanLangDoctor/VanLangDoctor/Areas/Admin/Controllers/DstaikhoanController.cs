using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    public class DstaikhoanController : Controller
    {
        CP24Team02Entities db = new CP24Team02Entities();
        // GET: Admin/Dstaikhoan
        public ActionResult Dstaikhoan()
        {
            var danhsach = db.AspNetUsers.ToList();
            return View(danhsach);
        }
    }
}