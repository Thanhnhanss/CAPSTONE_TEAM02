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
    public class HoSo_BenhAnController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/HoSo_BenhAn
        public ActionResult HS_BenhAn()
        {
            var bENH_AN = db.BENH_AN.Include(b => b.DON_THUOC);
            return View(bENH_AN.ToList());
        }

        // GET: Admin/HoSo_BenhAn/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BENH_AN bENH_AN = db.BENH_AN.Find(id);
            if (bENH_AN == null)
            {
                return HttpNotFound();
            }
            return View(bENH_AN);
        }

        // GET: Admin/HoSo_BenhAn/Create
        public ActionResult Create()
        {
            ViewBag.ID_DON_THUOC = new SelectList(db.DON_THUOC, "ID_DON_THUOC", "CHUAN_DOAN");
            return View();
        }

        // POST: Admin/HoSo_BenhAn/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_BENH_AN,KET_QUA,CHUAN_DOAN,TIEN_SU_BENH,GHI_CHU,ID_DON_THUOC")] BENH_AN bENH_AN)
        {
            if (ModelState.IsValid)
            {
                db.BENH_AN.Add(bENH_AN);
                db.SaveChanges();
                return RedirectToAction("HS_BenhAn");
            }

            ViewBag.ID_DON_THUOC = new SelectList(db.DON_THUOC, "ID_DON_THUOC", "CHUAN_DOAN", bENH_AN.ID_DON_THUOC);
            return View(bENH_AN);
        }

        // GET: Admin/HoSo_BenhAn/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BENH_AN bENH_AN = db.BENH_AN.Find(id);
            if (bENH_AN == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_DON_THUOC = new SelectList(db.DON_THUOC, "ID_DON_THUOC", "CHUAN_DOAN", bENH_AN.ID_DON_THUOC);
            return View(bENH_AN);
        }

        // POST: Admin/HoSo_BenhAn/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_BENH_AN,KET_QUA,CHUAN_DOAN,TIEN_SU_BENH,GHI_CHU,ID_DON_THUOC")] BENH_AN bENH_AN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bENH_AN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("HS_BenhAn");
            }
            ViewBag.ID_DON_THUOC = new SelectList(db.DON_THUOC, "ID_DON_THUOC", "CHUAN_DOAN", bENH_AN.ID_DON_THUOC);
            return View(bENH_AN);
        }

        // GET: Admin/HoSo_BenhAn/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BENH_AN bENH_AN = db.BENH_AN.Find(id);
            if (bENH_AN == null)
            {
                return HttpNotFound();
            }
            return View(bENH_AN);
        }

        // POST: Admin/HoSo_BenhAn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BENH_AN bENH_AN = db.BENH_AN.Find(id);
            db.BENH_AN.Remove(bENH_AN);
            db.SaveChanges();
            return RedirectToAction("HS_BenhAn");
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
