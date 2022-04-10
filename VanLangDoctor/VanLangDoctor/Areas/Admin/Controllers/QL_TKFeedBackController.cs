using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    public class QL_TKFeedBackController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/QL_TKFeedBack
        public ActionResult Index(string dg = "")
        {
            var danhgia = db.FEEDBACKs.Where(tk => tk.DANH_GIA == dg).ToList();
            ViewBag.dg = dg;
            return View(danhgia);
        }

        [HttpPost]
        public JsonResult ChartData()
        {
            var noiDung = db.FEEDBACKs.GroupBy(e => e.DANH_GIA)
                .Select(e => new { e.Key, Count = e.Count() });
            return Json(new { dbchart = noiDung, code = 200 }, JsonRequestBehavior.AllowGet);
        }
    }
}
