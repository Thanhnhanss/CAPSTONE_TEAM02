using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    [Authorize(Roles = "Bác sĩ, Quản trị viên")]
    public class QL_DonThuocController : Controller
    {
        private readonly CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/QL_DonThuoc
        [HttpGet]
        public ActionResult DS_DonThuoc()
        {
            var dON_THUOC = db.DON_THUOC.Include(d => d.BACSI).Include(d => d.SO_KHAM_BENH).OrderByDescending(t => t.NGAY_LAP);
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
        public ActionResult ThemDonThuoc()
        {
            var doctor = User.Identity.GetUserId();
            ViewBag.Patient = GetAllPatient();
            ViewBag.Medicine = GetAllMedicine();
            ViewBag.Doctor = db.BACSIs.FirstOrDefault(e => e.ID_Email == doctor).ID_BACSI;
            //ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI");
            ViewBag.ID_SO_KHAM_BENH = new SelectList(db.SO_KHAM_BENH, "ID_SOKHAMBENH", "ID_SOKHAMBENH");
            return View();
        }

        // POST: Admin/QL_DonThuoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult ThemDonThuoc(PatientModel dON_THUOC)
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
                    KET_QUA = dON_THUOC.KetQua,
                    NGAY_LAP = DateTime.Now,
                    ID_BACSI = dON_THUOC.BacSi,
                    CHI_TIET_DON_THUOC = dON_THUOC.Thuocs.Select(item => new CHI_TIET_DON_THUOC
                    {
                        ID_THUOC = item.Id,
                        DVT = item.DVT,
                        SO_LUONG = item.Quantity,
                        LIEU_DUNG = item.LieuDung
                    }).ToList()
                });
                db.SaveChanges();
                TempData["Success"] = "Kê đơn thành công";
                return RedirectToAction("DS_DonThuoc", "QL_DonThuoc", new { area = "Admin" });
            }

            TempData["warn"] = "Không thành công";
            return RedirectToAction("ThemDonThuoc", "QL_DonThuoc", new { area = "Admin" }); ;
        }

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
            [Required]
            public int HoTen { get; set; }

            public string ChanDoan { get; set; }


            public string KetQua { get; set; }

            [Required]
            public int BacSi { get; set; }

            [Required]
            public ThuocModel[] Thuocs { get; set; }
        }

        public class ThuocModel
        {
            public int Id { get; set; }

            public int Quantity { get; set; }

            public string DVT { get; set; }

            public string LieuDung { get; set; }
        }
    }
}