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
    [Authorize(Roles = "Quản trị viên, Bác sĩ, Quản lý")]
    public class QL_BenhNhanController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/QL_BenhNhan
        public ActionResult DanhSach_BN()
        {
            var bENH_NHAN = db.BENH_NHAN.Include(b => b.AspNetUser).Include(b => b.SO_KHAM_BENH);
            return View(bENH_NHAN.ToList());
        }

        // GET: Admin/QL_BenhNhan/ChiTiet_BN/5
        public ActionResult ChiTiet_BN(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BENH_NHAN bENH_NHAN = db.BENH_NHAN.Find(id);
            if (bENH_NHAN == null)
            {
                return HttpNotFound();
            }
            return View(bENH_NHAN);
        }

        public ActionResult So_Kham_Benh(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(400);
            }
            var skb = db.SO_KHAM_BENH.FirstOrDefault(s => s.ID_BENH_NHAN == id);

            if (skb == null)
            {
                return HttpNotFound();
            }
            ViewBag.Prescriptions = GetAllPrescriptions(id);
            return View(skb);
        }

        [HttpGet]
        public List<DON_THUOC> GetAllPrescriptions(int? id)
        {
            return db.DON_THUOC.OrderByDescending(t => t.NGAY_LAP).Where(t => t.SO_KHAM_BENH.BENH_NHAN.ID_BENH_NHAN == id).ToList();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
