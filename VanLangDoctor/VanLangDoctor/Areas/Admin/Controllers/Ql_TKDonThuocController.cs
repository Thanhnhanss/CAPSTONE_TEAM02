using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    [Authorize(Roles = "Quản trị viên, Bác sĩ, Quản lý")]
    public class QL_TKDonThuocController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/HomeAdmin
        public ActionResult Index(int ? month)
        {
            month = month ?? DateTime.Now.Month  ;
            ViewBag.month = month;
            return View();
        }

        [HttpPost]
        public JsonResult ChartData(int? month)
        {
            month = month ?? DateTime.Now.Month;
            if (month < 1 || month > 12)
                throw new ArgumentException();
            var data = db.DON_THUOC
                .Where(donThuoc => donThuoc.NGAY_LAP.Month == month && donThuoc.NGAY_LAP.Year == DateTime.Now.Year)
                .GroupBy(e => e.BACSI.TEN_BACSI)
                .Select(e => new { e.Key, Count = e.Count() });
            return Json(new { dbchart = data }, JsonRequestBehavior.AllowGet);
        }
    }
}