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
        public JsonResult ChartData(string dg = "")
        {
            var noidung = db.FEEDBACKs.Where(item => item.DANH_GIA == dg)
                .Select(item => new { name = item.DANH_GIA, count = item.DANH_GIA.Count() }).ToList();
            return Json(new { dbchart = noidung, code = 200 }, JsonRequestBehavior.AllowGet);
        }
    }
}
