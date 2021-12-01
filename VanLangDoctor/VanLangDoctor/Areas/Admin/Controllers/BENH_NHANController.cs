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
    public class BENH_NHANController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/BENH_NHAN
        public ActionResult DanhSachBN()
        {
            var bENH_NHAN = db.BENH_NHAN.Include(b => b.BENH_AN);
            return View(bENH_NHAN.ToList());
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
            ViewBag.ID_BENH_AN = new SelectList(db.BENH_AN, "ID_BENHAN", "KET_QUA");
            return View();
        }

        // POST: Admin/BENH_NHAN/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_BENHNHAN,TEN_BN,GIOI_TINH,NGAY_SINH,EMAIL,SDT,CHUAN_DOAN,ID_BACSI,ID_BENH_AN")] BENH_NHAN bENH_NHAN)
        {
            if (ModelState.IsValid)
            {
                db.BENH_NHAN.Add(bENH_NHAN);
                db.SaveChanges();
                return RedirectToAction("DanhSachBN");
            }

            ViewBag.ID_BENH_AN = new SelectList(db.BENH_AN, "ID_BENHAN", "KET_QUA", bENH_NHAN.ID_BENH_AN);
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
            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI");
            ViewBag.ID_BENH_AN = new SelectList(db.BENH_AN, "ID_BENHAN", "KET_QUA", bENH_NHAN.ID_BENH_AN);
            return View(bENH_NHAN);
        }

        // POST: Admin/BENH_NHAN/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_BENHNHAN,TEN_BN,GIOI_TINH,NGAY_SINH,EMAIL,SDT,CHUAN_DOAN,ID_BACSI,ID_BENH_AN")] BENH_NHAN bENH_NHAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bENH_NHAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachBN");
            }
            ViewBag.ID_BENH_AN = new SelectList(db.BENH_AN, "ID_BENHAN", "KET_QUA", bENH_NHAN.ID_BENH_AN);
            return View(bENH_NHAN);
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
            return RedirectToAction("DanhSachBN");
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
