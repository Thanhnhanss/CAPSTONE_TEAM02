using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.Admin
{
    public class QL_BACSI_Controller : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/QL_BACSI_
        public ActionResult Index()
        {
            var bACSIs = db.BACSIs.Include(b => b.KHOA);
            return View(bACSIs.ToList());
        }

        // GET: Admin/QL_BACSI_/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BACSI bACSI = db.BACSIs.Find(id);
            if (bACSI == null)
            {
                return HttpNotFound();
            }
            return View(bACSI);
        }

        // GET: Admin/QL_BACSI_/Create
        public ActionResult Create()
        {
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA");
            return View();
        }

        // POST: Admin/QL_BACSI_/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_BACSI,TEN_BACSI,NGAYSINH_BS,GIOI_TINH,SDT,EMAIL,HINH_ANH,NGHE_NGHIEP,ID_KHOA,KINH_NGHIEM,NGAY_TRUC")] BACSI bACSI)
        {
            if (ModelState.IsValid)
            {
                db.BACSIs.Add(bACSI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", bACSI.ID_KHOA);
            return View(bACSI);
        }

        // GET: Admin/QL_BACSI_/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BACSI bACSI = db.BACSIs.Find(id);
            if (bACSI == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", bACSI.ID_KHOA);
            return View(bACSI);
        }

        // POST: Admin/QL_BACSI_/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_BACSI,TEN_BACSI,NGAYSINH_BS,GIOI_TINH,SDT,EMAIL,HINH_ANH,NGHE_NGHIEP,ID_KHOA,KINH_NGHIEM,NGAY_TRUC")] BACSI bACSI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bACSI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", bACSI.ID_KHOA);
            return View(bACSI);
        }

        // GET: Admin/QL_BACSI_/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BACSI bACSI = db.BACSIs.Find(id);
            if (bACSI == null)
            {
                return HttpNotFound();
            }
            return View(bACSI);
        }

        // POST: Admin/QL_BACSI_/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BACSI bACSI = db.BACSIs.Find(id);
            db.BACSIs.Remove(bACSI);
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
