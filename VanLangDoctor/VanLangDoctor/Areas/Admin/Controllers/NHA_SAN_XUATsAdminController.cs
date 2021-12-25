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
    public class NHA_SAN_XUATsAdminController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/NHA_SAN_XUATsAdmin
        public ActionResult Index()
        {
            return View(db.NHA_SAN_XUAT.ToList());
        }

        // GET: Admin/NHA_SAN_XUATsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHA_SAN_XUAT nHA_SAN_XUAT = db.NHA_SAN_XUAT.Find(id);
            if (nHA_SAN_XUAT == null)
            {
                return HttpNotFound();
            }
            return View(nHA_SAN_XUAT);
        }

        // GET: Admin/NHA_SAN_XUATsAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NHA_SAN_XUATsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TEN_NSX")] NHA_SAN_XUAT nHA_SAN_XUAT)
        {
            if (ModelState.IsValid)
            {
                db.NHA_SAN_XUAT.Add(nHA_SAN_XUAT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nHA_SAN_XUAT);
        }

        // GET: Admin/NHA_SAN_XUATsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHA_SAN_XUAT nHA_SAN_XUAT = db.NHA_SAN_XUAT.Find(id);
            if (nHA_SAN_XUAT == null)
            {
                return HttpNotFound();
            }
            return View(nHA_SAN_XUAT);
        }

        // POST: Admin/NHA_SAN_XUATsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TEN_NSX")] NHA_SAN_XUAT nHA_SAN_XUAT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHA_SAN_XUAT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nHA_SAN_XUAT);
        }

        // GET: Admin/NHA_SAN_XUATsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHA_SAN_XUAT nHA_SAN_XUAT = db.NHA_SAN_XUAT.Find(id);
            if (nHA_SAN_XUAT == null)
            {
                return HttpNotFound();
            }
            return View(nHA_SAN_XUAT);
        }

        // POST: Admin/NHA_SAN_XUATsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NHA_SAN_XUAT nHA_SAN_XUAT = db.NHA_SAN_XUAT.Find(id);
            db.NHA_SAN_XUAT.Remove(nHA_SAN_XUAT);
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
