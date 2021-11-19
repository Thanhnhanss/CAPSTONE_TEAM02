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
    public class QL_BenhNhanController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/BENH_NHAN
        public ActionResult Benh_Nhan()
        {
            var bENH_NHAN = db.BENH_NHAN.ToList();
            return View(bENH_NHAN);
        }

        // GET: Admin/BENH_NHAN/Details/5
        public ActionResult Details(int? id)
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

        // GET: Admin/BENH_NHAN/Create
        public ActionResult Create()
        {
            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI");
            ViewBag.ID_BENH_AN = new SelectList(db.BENH_AN, "ID_BENH_AN", "KET_QUA");
            return View();
        }

        // POST: Admin/BENH_NHAN/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_BENH_NHAN,TEN_BN,TUOI,NGAY_SINH,EMAIL,SDT,GIOI_TINH,CHUAN_DOAN,NGHE_NGHIEP,BHYT,ID_BACSI,ID_BENH_AN")] BENH_NHAN bENH_NHAN)
        {
            if (ModelState.IsValid)
            {
                db.BENH_NHAN.Add(bENH_NHAN);
                db.SaveChanges();
                return RedirectToAction("Benh_Nhan");
            }

            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", bENH_NHAN.ID_BACSI);
            ViewBag.ID_BENH_AN = new SelectList(db.BENH_AN, "ID_BENH_AN", "KET_QUA", bENH_NHAN.ID_BENH_AN);
            return View(bENH_NHAN);
        }

        // GET: Admin/BENH_NHAN/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", bENH_NHAN.ID_BACSI);
            ViewBag.ID_BENH_AN = new SelectList(db.BENH_AN, "ID_BENH_AN", "KET_QUA", bENH_NHAN.ID_BENH_AN);
            return View(bENH_NHAN);
        }

        // POST: Admin/BENH_NHAN/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int ID, string TenBN, int  Tuoi, DateTime Ngaysinh, string Email, string sdt, bool gioitinh, string Chuan_doan, string Nghenghiep, string bhyt, int ID_BacSi, int ID_BenhAn)
        {
            var benh_nhan = db.BENH_NHAN.Find(ID);
            if (benh_nhan == null)
            {
                benh_nhan = new BENH_NHAN
                {
                    ID_BENH_NHAN = ID,
                    TEN_BN = TenBN,
                    TUOI = Tuoi,
                    NGAY_SINH = Ngaysinh,
                    EMAIL = Email,
                    SDT = sdt,
                    GIOI_TINH = gioitinh,
                    CHUAN_DOAN = Chuan_doan,
                    NGHE_NGHIEP = Nghenghiep,
                    BHYT = bhyt,
                    ID_BACSI = ID_BacSi,
                    ID_BENH_AN = ID_BenhAn
                };
                db.BENH_NHAN.Add(benh_nhan);
            }
            else
            {
                benh_nhan.TEN_BN = TenBN;
                benh_nhan.TUOI = Tuoi;
                benh_nhan.NGAY_SINH = (Ngaysinh);
                benh_nhan.EMAIL = Email;
                benh_nhan.SDT = sdt;
                benh_nhan.GIOI_TINH = gioitinh;
                benh_nhan.CHUAN_DOAN= Chuan_doan;
                benh_nhan.NGHE_NGHIEP= Nghenghiep;
                benh_nhan.BHYT = bhyt;
                db.Entry<BENH_NHAN>(benh_nhan).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            ViewBag.message = "Cập nhật thành công!";
            return View("Benh_Nhan", benh_nhan);
        }

        // GET: Admin/BENH_NHAN/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/BENH_NHAN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BENH_NHAN bENH_NHAN = db.BENH_NHAN.Find(id);
            db.BENH_NHAN.Remove(bENH_NHAN);
            db.SaveChanges();
            return RedirectToAction("Benh_Nhan");
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
