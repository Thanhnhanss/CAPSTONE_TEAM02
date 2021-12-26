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
    public class DON_THUOCsAdminController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/DON_THUOCsAdmin
        public ActionResult Index()
        {
            var dON_THUOC = db.DON_THUOC.Include(d => d.BACSI).Include(d => d.THUOC);
            return View(dON_THUOC.ToList());
        }

        // GET: Admin/DON_THUOCsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DON_THUOC dON_THUOC = db.DON_THUOC.Find(id);
            if (dON_THUOC == null)
            {
                return HttpNotFound();
            }
            return View(dON_THUOC);
        }

        // GET: Admin/DON_THUOCsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI");
            ViewBag.ID_THUOC = new SelectList(db.THUOCs, "ID_THUOC", "TEN_THUOC");
            return View();
        }

        // POST: Admin/DON_THUOCsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_DON_THUOC,CHUAN_DOAN,CHI_DINH,LOI_DAN,NGAY_LAP,ID_THUOC,ID_BACSI")] DON_THUOC dON_THUOC)
        {
            if (ModelState.IsValid)
            {
                db.DON_THUOC.Add(dON_THUOC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dON_THUOC.ID_BACSI);
            ViewBag.ID_THUOC = new SelectList(db.THUOCs, "ID_THUOC", "TEN_THUOC", dON_THUOC.ID_THUOC);
            return View(dON_THUOC);
        }

        // GET: Admin/DON_THUOCsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DON_THUOC dON_THUOC = db.DON_THUOC.Find(id);
            if (dON_THUOC == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dON_THUOC.ID_BACSI);
            ViewBag.ID_THUOC = new SelectList(db.THUOCs, "ID_THUOC", "TEN_THUOC", dON_THUOC.ID_THUOC);
            return View(dON_THUOC);
        }

        // POST: Admin/DON_THUOCsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_DON_THUOC,CHUAN_DOAN,CHI_DINH,LOI_DAN,NGAY_LAP,ID_THUOC,ID_BACSI")] DON_THUOC dON_THUOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dON_THUOC).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Cập nhật thuốc thành công";
                return RedirectToAction("Index");
            }
            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dON_THUOC.ID_BACSI);
            ViewBag.ID_THUOC = new SelectList(db.THUOCs, "ID_THUOC", "TEN_THUOC", dON_THUOC.ID_THUOC);
            TempData["Success"] = "Cập nhật thuốc thành công";
            return View(dON_THUOC);
        }

        // GET: Admin/DON_THUOCsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DON_THUOC dON_THUOC = db.DON_THUOC.Find(id);
            if (dON_THUOC == null)
            {
                return HttpNotFound();
            }
            return View(dON_THUOC);
        }

        // POST: Admin/DON_THUOCsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DON_THUOC dON_THUOC = db.DON_THUOC.Find(id);
            db.DON_THUOC.Remove(dON_THUOC);
            db.SaveChanges();
            TempData["Success"] = "Xóa đơn thuốc thành công";
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
