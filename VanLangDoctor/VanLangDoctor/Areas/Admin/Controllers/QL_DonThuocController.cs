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
        [HttpGet]
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
            ViewBag.Doctor = GetAllDoctor();
            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI");
            ViewBag.ID_SO_KHAM_BENH = new SelectList(db.SO_KHAM_BENH, "ID_SOKHAMBENH", "ID_SOKHAMBENH");
            return View();
        }

        // POST: Admin/QL_DonThuoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(PatientModel dON_THUOC)
        {
            if (ModelState.IsValid)
            {
                var benhNhan = db.BENH_NHAN.Find(dON_THUOC.HoTen);
                if (benhNhan.SO_KHAM_BENH.Count == 0)
                {
                    ModelState.AddModelError("Error", "Bệnh nhân chưa có sổ khám bệnh.");
                    return View();
                }
                benhNhan.SO_KHAM_BENH.ElementAt(0).DON_THUOC.Add(new DON_THUOC
                {
                    CHUAN_DOAN = dON_THUOC.ChanDoan,
                    CHI_DINH = dON_THUOC.ChiDinh,
                    LOI_DAN = dON_THUOC.LoiDan,
                    KET_QUA = dON_THUOC.KetQua,
                    NGAY_LAP = dON_THUOC.NgayKhoiTao,
                    ID_BACSI = dON_THUOC.BacSi,
                    CHI_TIET_DON_THUOC = dON_THUOC.Thuocs.Select(item => new CHI_TIET_DON_THUOC
                    {
                        ID_THUOC = item.Id,
                        SO_LUONG = item.Quantity
                    }).ToList()
                });
                db.SaveChanges();
                return RedirectToAction("Index", "QL_DonThuoc", new { area = "Admin" });
            }

            //ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dON_THUOC.ID_BACSI);
            //ViewBag.ID_SO_KHAM_BENH = new SelectList(db.SO_KHAM_BENH, "ID_SOKHAMBENH", "ID_SOKHAMBENH", dON_THUOC.ID_SO_KHAM_BENH);
            return View(dON_THUOC);
        }

        // GET: Admin/QL_DonThuoc/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Medicine = GetAllMedicine();
            ViewBag.Doctor = GetAllDoctor();
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
        public ActionResult Edit(PatientModel dON_THUOC, int id)
        {
            var donthuoc = db.DON_THUOC.Find(id);
            if (ModelState.IsValid)
            {
                donthuoc.CHUAN_DOAN = dON_THUOC.ChanDoan;
                donthuoc.CHI_DINH = dON_THUOC.ChiDinh;
                donthuoc.LOI_DAN = dON_THUOC.LoiDan;
                donthuoc.KET_QUA = dON_THUOC.KetQua;
                donthuoc.CHI_TIET_DON_THUOC = dON_THUOC.Thuocs.Select(item => new CHI_TIET_DON_THUOC
                {
                    ID_THUOC = item.Id,
                    SO_LUONG = item.Quantity
                }).ToList();
                db.Entry(donthuoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Medicine = GetAllMedicine();
            //ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dON_THUOC.ID_BACSI);
            //ViewBag.ID_SO_KHAM_BENH = new SelectList(db.SO_KHAM_BENH, "ID_SOKHAMBENH", "ID_SOKHAMBENH", dON_THUOC.ID_SO_KHAM_BENH);
            return View(donthuoc);
        }

        //// GET: Admin/QL_DonThuoc/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DON_THUOC dON_THUOC = db.DON_THUOC.Find(id);
        //    if (dON_THUOC == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dON_THUOC);
        //}

        //// POST: Admin/QL_DonThuoc/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    DON_THUOC dON_THUOC = db.DON_THUOC.Find(id);
        //    db.DON_THUOC.Remove(dON_THUOC);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
        public List<BENH_NHAN> GetAllPatient() => db.BENH_NHAN.Where(item => item.SO_KHAM_BENH.Count > 0).ToList();

        /// <summary>
        /// Get All Doctor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public List<BACSI> GetAllDoctor() => db.BACSIs.ToList();


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

        public class PatientModel
        {
            public int HoTen { get; set; }

            public int Tuoi { get; set; }

            public string GioiTinh { get; set; }

            public string DiaChi { get; set; }

            public DateTime NgayKhoiTao { get; set; }

            public string ChanDoan { get; set; }

            public string LoiDan { get; set; }

            public string ChiDinh { get; set; }

            public string KetQua { get; set; }

            public int BacSi { get; set; }

            public ThuocModel[] Thuocs { get; set; }
        }

        public class ThuocModel
        {
            public int Id { get; set; }

            public int Quantity { get; set; }
        }
    }
}
