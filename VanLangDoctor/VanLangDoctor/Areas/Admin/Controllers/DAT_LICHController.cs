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
    [Authorize]
    public class DAT_LICHController : Controller
    {
        private readonly CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/DAT_LICH
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var doctor = db.BACSIs.FirstOrDefault(e => e.ID_Email.Equals(userId)).ID_BACSI;
            var dAT_LICH = db.DAT_LICH
                .Where(d => d.ID_BACSI == doctor)
                .Include(d => d.BACSI)
                .OrderByDescending(t => t.NGAY_TRUC);
            return View(dAT_LICH.ToList());
            
        }
        public ActionResult DanhSachLichTruc()
        {
            var dAT_LICH = db.DAT_LICH.Include(d => d.BACSI).OrderByDescending(t => t.NGAY_TRUC).ToList();
            return View(dAT_LICH);
        }
        // GET: Admin/DAT_LICH/Details/5
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

        // GET: Admin/DAT_LICH/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DAT_LICH/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DAT_LICH dAT_LICH)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                dAT_LICH.ID_BACSI = db.BACSIs.FirstOrDefault(e => e.ID_Email == userId).ID_BACSI;

                db.DAT_LICH.Add(dAT_LICH);
                db.SaveChanges();
                return RedirectToAction("Index", "DAT_LICH", new {area = "Admin"});
            }
            //ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dAT_LICH.ID_BACSI);
            return View(dAT_LICH);
        }

        // GET: Admin/DAT_LICH/Edit/5
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

        // POST: Admin/DAT_LICH/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NGAY_TRUC,ID_BACSI,GIO_TRUC")] DAT_LICH dAT_LICH)
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

        // GET: Admin/DAT_LICH/Delete/5
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

        // POST: Admin/DAT_LICH/Delete/5
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
