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
    public class BENH_ANController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/BENH_AN
        public ActionResult Index()
        {
            
            return View(db.BENH_AN.ToList());
        }

        // GET: Admin/BENH_AN/Details/5
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

        // GET: Admin/BENH_AN/Create
        public ActionResult Create()
        {
            ViewBag.ID_BENH_NHAN = new SelectList(db.BENH_NHAN, "ID_BENHNHAN", "TEN_BN");
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA");
            return View();
        }

        // POST: Admin/BENH_AN/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_BENHAN,KET_QUA,CHUAN_DOAN,TIEN_SU_BENH,GHI_CHU,ID_BENH_NHAN,ID_KHOA")] BENH_AN bENH_AN)
        {
            if (ModelState.IsValid)
            {
                db.BENH_AN.Add(bENH_AN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_BENH_NHAN = new SelectList(db.BENH_NHAN, "ID_BENHNHAN", "TEN_BN");
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA");
            return View(bENH_AN);
        }

        // GET: Admin/BENH_AN/Edit/5
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
            ViewBag.ID_BENH_NHAN = new SelectList(db.BENH_NHAN, "ID_BENHNHAN", "TEN_BN");
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA");
            return View(bENH_AN);
        }

        // POST: Admin/BENH_AN/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_BENHAN,KET_QUA,CHUAN_DOAN,TIEN_SU_BENH,GHI_CHU,ID_BENH_NHAN,ID_KHOA")] BENH_AN bENH_AN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bENH_AN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_BENH_NHAN = new SelectList(db.BENH_NHAN, "ID_BENHNHAN", "TEN_BN");
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA");
            return View(bENH_AN);
        }

        // GET: Admin/BENH_AN/Delete/5
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

        // POST: Admin/BENH_AN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BENH_AN bENH_AN = db.BENH_AN.Find(id);
            db.BENH_AN.Remove(bENH_AN);
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
