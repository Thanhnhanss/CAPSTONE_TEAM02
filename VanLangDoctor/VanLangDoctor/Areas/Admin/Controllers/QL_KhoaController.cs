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
    public class QL_KhoaController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/QL_Khoa
        public ActionResult KHOA()
        {
            return View(db.KHOAs.ToList());
        }

        // GET: Admin/QL_Khoa/Create
        public ActionResult Create_K()
        {
            return View();
        }

        // POST: Admin/QL_Khoa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_K(KHOA kHOA)
        {
            if (ModelState.IsValid)
            {
                db.KHOAs.Add(kHOA);
                db.SaveChanges();
                return RedirectToAction("KHOA");
            }

            return View(kHOA);
        }

        // GET: Admin/QL_Khoa/Edit/5
        public ActionResult Edit_K(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHOA kHOA = db.KHOAs.Find(id);
            if (kHOA == null)
            {
                return HttpNotFound();
            }
            return View(kHOA);
        }

        // POST: Admin/QL_Khoa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_K(KHOA kHOA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHOA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("KHOA");
            }
            return View(kHOA);
        }

        // GET: Admin/QL_Khoa/Delete/5
        public ActionResult Delete_K(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHOA kHOA = db.KHOAs.Find(id);
            if (kHOA == null)
            {
                return HttpNotFound();
            }
            return View(kHOA);
        }

        // POST: Admin/QL_Khoa/Delete/5
        [HttpPost, ActionName("Delete_K")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KHOA kHOA = db.KHOAs.Find(id);
            db.KHOAs.Remove(kHOA);
            db.SaveChanges();
            return RedirectToAction("KHOA");
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
