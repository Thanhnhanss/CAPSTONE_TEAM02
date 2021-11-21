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
    public class QL_TinTucController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/QL_TinTuc
        public ActionResult Tin_tuc()
        {
            return View(db.TIN_TUC.ToList());
        }

        // GET: Admin/QL_TinTuc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_TUC tIN_TUC = db.TIN_TUC.Find(id);
            if (tIN_TUC == null)
            {
                return HttpNotFound();
            }
            return View(tIN_TUC);
        }

        // GET: Admin/QL_TinTuc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QL_TinTuc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_TIN_TUC,TEN_BAI_VIET,NGAY_DANG,NOI_DUNG,TAC_GIA,SEO_TITLE")] TIN_TUC tIN_TUC)
        {
            if (ModelState.IsValid)
            {
                db.TIN_TUC.Add(tIN_TUC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tIN_TUC);
        }

        // GET: Admin/QL_TinTuc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_TUC tIN_TUC = db.TIN_TUC.Find(id);
            if (tIN_TUC == null)
            {
                return HttpNotFound();
            }
            return View(tIN_TUC);
        }

        // POST: Admin/QL_TinTuc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_TIN_TUC,TEN_BAI_VIET,NGAY_DANG,NOI_DUNG,TAC_GIA,SEO_TITLE")] TIN_TUC tIN_TUC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIN_TUC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tIN_TUC);
        }

        // GET: Admin/QL_TinTuc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_TUC tIN_TUC = db.TIN_TUC.Find(id);
            if (tIN_TUC == null)
            {
                return HttpNotFound();
            }
            return View(tIN_TUC);
        }

        // POST: Admin/QL_TinTuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TIN_TUC tIN_TUC = db.TIN_TUC.Find(id);
            db.TIN_TUC.Remove(tIN_TUC);
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
