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
    public class QL_DonThuocController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/QL_DonThuoc
        public ActionResult Index()
        {
            var dON_THUOC = db.DON_THUOC.Include(d => d.BACSI).Include(d => d.SO_KHAM_BENH);
            return View(dON_THUOC.ToList());
        }

        // GET: Admin/QL_DonThuoc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DON_THUOC dON_THUOC = db.DON_THUOC.Find(id);
            if (dON_THUOC == null)
            {
                return HttpNotFound();
            }
            return View(dON_THUOC);
        }

        // GET: Admin/QL_DonThuoc/Create
        public ActionResult Create()
        {
            ViewBag.Patient = GetAllPatient();
            ViewBag.Medicine = GetAllMedicine();
            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI");
            ViewBag.ID_SO_KHAM_BENH = new SelectList(db.SO_KHAM_BENH, "ID_SOKHAMBENH", "ID_SOKHAMBENH");
            return View();
        }

        // POST: Admin/QL_DonThuoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_DON_THUOC,KET_QUA,CHUAN_DOAN,CHI_DINH,LOI_DAN,NGAY_LAP,ID_BACSI,ID_SO_KHAM_BENH")] DON_THUOC dON_THUOC)
        {
            if (ModelState.IsValid)
            {
                db.DON_THUOC.Add(dON_THUOC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dON_THUOC.ID_BACSI);
            ViewBag.ID_SO_KHAM_BENH = new SelectList(db.SO_KHAM_BENH, "ID_SOKHAMBENH", "ID_SOKHAMBENH", dON_THUOC.ID_SO_KHAM_BENH);
            return View(dON_THUOC);
        }

        // GET: Admin/QL_DonThuoc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DON_THUOC dON_THUOC = db.DON_THUOC.Find(id);
            if (dON_THUOC == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dON_THUOC.ID_BACSI);
            ViewBag.ID_SO_KHAM_BENH = new SelectList(db.SO_KHAM_BENH, "ID_SOKHAMBENH", "ID_SOKHAMBENH", dON_THUOC.ID_SO_KHAM_BENH);
            return View(dON_THUOC);
        }

        // POST: Admin/QL_DonThuoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_DON_THUOC,KET_QUA,CHUAN_DOAN,CHI_DINH,LOI_DAN,NGAY_LAP,ID_BACSI,ID_SO_KHAM_BENH")] DON_THUOC dON_THUOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dON_THUOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dON_THUOC.ID_BACSI);
            ViewBag.ID_SO_KHAM_BENH = new SelectList(db.SO_KHAM_BENH, "ID_SOKHAMBENH", "ID_SOKHAMBENH", dON_THUOC.ID_SO_KHAM_BENH);
            return View(dON_THUOC);
        }

        // GET: Admin/QL_DonThuoc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DON_THUOC dON_THUOC = db.DON_THUOC.Find(id);
            if (dON_THUOC == null)
            {
                return HttpNotFound();
            }
            return View(dON_THUOC);
        }

        // POST: Admin/QL_DonThuoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DON_THUOC dON_THUOC = db.DON_THUOC.Find(id);
            db.DON_THUOC.Remove(dON_THUOC);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Get All Medicine
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<THUOC> GetAllMedicine() => db.THUOCs.ToList();

        /// <summary>
        /// Get All Patient
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<BENH_NHAN> GetAllPatient() => db.BENH_NHAN.ToList();

        [HttpGet]
        public JsonResult GetPatient(int id)
        {
            var benhNhan = db.BENH_NHAN.Find(id);
            return Json(new BenhNhanModel
            {
                ID = benhNhan.ID_BENH_NHAN,
                Ten = benhNhan.TEN_BN,
                GioiTinh = benhNhan.GIOI_TINH,
                Tuoi = DateTime.Now.Year - benhNhan.NGAY_SINH.Value.Year,
                DiaChi = benhNhan.DIA_CHI
            }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public class BenhNhanModel
        {
            public int ID { get; set; }

            public string Ten { get; set; }

            public int Tuoi { get; set; }

            public string GioiTinh { get; set; }

            public string DiaChi { get; set; }
        }
    }
}
