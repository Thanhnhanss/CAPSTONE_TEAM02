using Microsoft.AspNet.Identity;
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
    [HandleError]
    public class QL_DatLichController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/QL_DatLich
        public ActionResult Index()
        {
            var dAT_LICH = db.DAT_LICH.Include(d => d.BACSI);
            return View(dAT_LICH.ToList());
        }

        // GET: Admin/QL_DatLich/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAT_LICH dAT_LICH = db.DAT_LICH.Find(id);
            if (dAT_LICH == null)
            {
                return HttpNotFound();
            }
            return View(dAT_LICH);
        }

        // GET: Admin/QL_DatLich/Create
        [Authorize]
        public ActionResult Create()
        {
            var bacsi = User.Identity.GetUserId();

            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI");
            return View();
        }

        // POST: Admin/QL_DatLich/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NGAY_TRUC,ID_BACSI")] DAT_LICH dAT_LICH)
        {
            if (ModelState.IsValid)
            {
                db.DAT_LICH.Add(dAT_LICH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dAT_LICH.ID_BACSI);
            return View(dAT_LICH);
        }

        // GET: Admin/QL_DatLich/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAT_LICH dAT_LICH = db.DAT_LICH.Find(id);
            if (dAT_LICH == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dAT_LICH.ID_BACSI);
            return View(dAT_LICH);
        }

        // POST: Admin/QL_DatLich/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NGAY_TRUC,ID_BACSI")] DAT_LICH dAT_LICH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dAT_LICH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dAT_LICH.ID_BACSI);
            return View(dAT_LICH);
        }

        // GET: Admin/QL_DatLich/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAT_LICH dAT_LICH = db.DAT_LICH.Find(id);
            if (dAT_LICH == null)
            {
                return HttpNotFound();
            }
            return View(dAT_LICH);
        }

        // POST: Admin/QL_DatLich/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DAT_LICH dAT_LICH = db.DAT_LICH.Find(id);
            db.DAT_LICH.Remove(dAT_LICH);
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
