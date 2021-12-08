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
    public class QL_NhaSanXuatController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/QL_NhaSanXuat
        public ActionResult Index_NSX()
        {
            return View(db.NHA_SAN_XUAT.ToList());
        }

        // GET: Admin/QL_NhaSanXuat/Details/5
        public ActionResult Details_NSX(int? id)
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

        // GET: Admin/QL_NhaSanXuat/Create
        public ActionResult Create_NSX()
        {
            return View();
        }

        // POST: Admin/QL_NhaSanXuat/Create
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
                return RedirectToAction("Index_NSX");
            }

            return View(nHA_SAN_XUAT);
        }

        // GET: Admin/QL_NhaSanXuat/Edit/5
        public ActionResult Edit_NSX(int? id)
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

        // POST: Admin/QL_NhaSanXuat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_NSX([Bind(Include = "ID,TEN_NSX")] NHA_SAN_XUAT nHA_SAN_XUAT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHA_SAN_XUAT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index_NSX");
            }
            return View(nHA_SAN_XUAT);
        }

        // GET: Admin/QL_NhaSanXuat/Delete/5
        public ActionResult Delete_NSX(int? id)
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

        // POST: Admin/QL_NhaSanXuat/Delete/5
        [HttpPost, ActionName("Delete_NSX")]
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
