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
    public class BacSiController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: User/BacSi
        public ActionResult Bac_Si()
        {
            var bACSIs = db.BACSIs.Include(b => b.KHOA1);
            return View(bACSIs.ToList());
        }

        // GET: User/BacSi/Details/5
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

        // GET: User/BacSi/Create
        public ActionResult Create()
        {
            ViewBag.CHUYENKHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA");
            return View();
        }

        // POST: User/BacSi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_BACSI,TEN_BACSI,SDT_BACSI,NGAYSINH_BACSI,TUOI,EMAIL,GIOI_TINH,KHOA,KINH_NGHIEM,NGAY_TRUC,BHYT,IMG_BACSI,CHUYENKHOA")] BACSI bACSI)
        {
            if (ModelState.IsValid)
            {
                db.BACSIs.Add(bACSI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CHUYENKHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", bACSI.CHUYENKHOA);
            return View(bACSI);
        }

        // GET: User/BacSi/Edit/5
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
            ViewBag.CHUYENKHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", bACSI.CHUYENKHOA);
            return View(bACSI);
        }

        // POST: User/BacSi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_BACSI,TEN_BACSI,SDT_BACSI,NGAYSINH_BACSI,TUOI,EMAIL,GIOI_TINH,KHOA,KINH_NGHIEM,NGAY_TRUC,BHYT,IMG_BACSI,CHUYENKHOA")] BACSI bACSI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bACSI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CHUYENKHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", bACSI.CHUYENKHOA);
            return View(bACSI);
        }

        // GET: User/BacSi/Delete/5
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

        // POST: User/BacSi/Delete/5
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
