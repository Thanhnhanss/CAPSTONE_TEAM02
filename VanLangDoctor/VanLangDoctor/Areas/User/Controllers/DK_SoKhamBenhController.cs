using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.User.Controllers
{
    public class DK_SoKhamBenhController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: User/DK_SoKhamBenh
        public ActionResult Index()
        {
            var sO_KHAM_BENH = db.SO_KHAM_BENH.Include(s => s.BENH_NHAN);
            return View(sO_KHAM_BENH.ToList());
        }

        // GET: User/DK_SoKhamBenh/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SO_KHAM_BENH sO_KHAM_BENH = db.SO_KHAM_BENH.Find(id);
            if (sO_KHAM_BENH == null)
            {
                return HttpNotFound();
            }
            return View(sO_KHAM_BENH);
        }

        // GET: User/DK_SoKhamBenh/Create
        public ActionResult Create()
        {
            ViewBag.ID_BENH_NHAN = new SelectList(db.BENH_NHAN, "ID_BENH_NHAN", "TEN_BN");
            return RedirectToAction("Create");
        }

        // POST: User/DK_SoKhamBenh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_SOKHAMBENH,ID_BENH_NHAN")] SO_KHAM_BENH sO_KHAM_BENH)
        {
            if (ModelState.IsValid)
            {
                db.SO_KHAM_BENH.Add(sO_KHAM_BENH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_BENH_NHAN = new SelectList(db.BENH_NHAN, "ID_BENH_NHAN", "TEN_BN", sO_KHAM_BENH.ID_BENH_NHAN);
            return View(sO_KHAM_BENH);
        }

        // GET: User/DK_SoKhamBenh/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SO_KHAM_BENH sO_KHAM_BENH = db.SO_KHAM_BENH.Find(id);
            if (sO_KHAM_BENH == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_BENH_NHAN = new SelectList(db.BENH_NHAN, "ID_BENH_NHAN", "TEN_BN", sO_KHAM_BENH.ID_BENH_NHAN);
            return View(sO_KHAM_BENH);
        }

        // POST: User/DK_SoKhamBenh/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_SOKHAMBENH,ID_BENH_NHAN")] SO_KHAM_BENH sO_KHAM_BENH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sO_KHAM_BENH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_BENH_NHAN = new SelectList(db.BENH_NHAN, "ID_BENH_NHAN", "TEN_BN", sO_KHAM_BENH.ID_BENH_NHAN);
            return View(sO_KHAM_BENH);
        }

        // GET: User/DK_SoKhamBenh/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SO_KHAM_BENH sO_KHAM_BENH = db.SO_KHAM_BENH.Find(id);
            if (sO_KHAM_BENH == null)
            {
                return HttpNotFound();
            }
            return View(sO_KHAM_BENH);
        }

        // POST: User/DK_SoKhamBenh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SO_KHAM_BENH sO_KHAM_BENH = db.SO_KHAM_BENH.Find(id);
            db.SO_KHAM_BENH.Remove(sO_KHAM_BENH);
            db.SaveChanges();
            return RedirectToAction("Index");
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
