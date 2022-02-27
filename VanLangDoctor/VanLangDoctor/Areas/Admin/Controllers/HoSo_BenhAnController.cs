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
            var SO_KHAM_BENH = db.SO_KHAM_BENH.Include(b => b.DON_THUOC);
            return View(SO_KHAM_BENH.ToList());
        }

        // GET: Admin/HoSo_BenhAn/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SO_KHAM_BENH SO_KHAM_BENH = db.SO_KHAM_BENH.Find(id);
            if (SO_KHAM_BENH == null)
            {
                return HttpNotFound();
            }
            return View(SO_KHAM_BENH);
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
        public ActionResult Create([Bind(Include = "ID_SO_KHAM_BENH,KET_QUA,CHUAN_DOAN,TIEN_SU_BENH,GHI_CHU,ID_DON_THUOC")] SO_KHAM_BENH SO_KHAM_BENH)
        {
            if (ModelState.IsValid)
            {
                db.SO_KHAM_BENH.Add(SO_KHAM_BENH);
                db.SaveChanges();
                return RedirectToAction("HS_BenhAn");
            }

            ViewBag.ID_DON_THUOC = new SelectList(db.DON_THUOC, "ID_DON_THUOC", "CHUAN_DOAN", SO_KHAM_BENH.ID_DON_THUOC);
            return View(SO_KHAM_BENH);
        }

        // GET: Admin/HoSo_BenhAn/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SO_KHAM_BENH SO_KHAM_BENH = db.SO_KHAM_BENH.Find(id);
            if (SO_KHAM_BENH == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_DON_THUOC = new SelectList(db.DON_THUOC, "ID_DON_THUOC", "CHUAN_DOAN", SO_KHAM_BENH.ID_DON_THUOC);
            return View(SO_KHAM_BENH);
        }

        // POST: Admin/HoSo_BenhAn/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_SO_KHAM_BENH,KET_QUA,CHUAN_DOAN,TIEN_SU_BENH,GHI_CHU,ID_DON_THUOC")] SO_KHAM_BENH SO_KHAM_BENH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(SO_KHAM_BENH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("HS_BenhAn");
            }
            ViewBag.ID_DON_THUOC = new SelectList(db.DON_THUOC, "ID_DON_THUOC", "CHUAN_DOAN", SO_KHAM_BENH.ID_DON_THUOC);
            return View(SO_KHAM_BENH);
        }

        // GET: Admin/HoSo_BenhAn/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SO_KHAM_BENH SO_KHAM_BENH = db.SO_KHAM_BENH.Find(id);
            if (SO_KHAM_BENH == null)
            {
                return HttpNotFound();
            }
            return View(SO_KHAM_BENH);
        }

        // POST: Admin/HoSo_BenhAn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SO_KHAM_BENH SO_KHAM_BENH = db.SO_KHAM_BENH.Find(id);
            db.SO_KHAM_BENH.Remove(SO_KHAM_BENH);
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
