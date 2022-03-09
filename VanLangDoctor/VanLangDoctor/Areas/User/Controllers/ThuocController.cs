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
    public class ThuocController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: User/Thuoc
        public ActionResult Index()
        {
            var tHUOCs = db.THUOCs.Include(t => t.DANH_MUC_THUOC).Include(t => t.NHA_SAN_XUAT);
            return View(tHUOCs.ToList());
        }
        private const string PICTURE_PATH = "~/Content/IMG_MEDICINE/";

        public ActionResult Picture(int ID_Thuoc)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + ID_Thuoc, "images");
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THUOC tHUOC = db.THUOCs.Find(id);
            if (tHUOC == null)
            {
                return HttpNotFound();
            }
            return View(tHUOC);
        }

        // GET: User/Thuoc/Create
        public ActionResult Create()
        {
            ViewBag.ID_DANHMUC = new SelectList(db.DANH_MUC_THUOC, "ID", "DanhMuc");
            ViewBag.ID_NSX = new SelectList(db.NHA_SAN_XUAT, "ID", "TEN_NSX");
            return View();
        }

        // POST: User/Thuoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_THUOC,TEN_THUOC,LIEU_LUONG,MO_TA,HINH_ANH,ID_NSX,ID_DANHMUC")] THUOC tHUOC)
        {
            if (ModelState.IsValid)
            {
                db.THUOCs.Add(tHUOC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_DANHMUC = new SelectList(db.DANH_MUC_THUOC, "ID", "DanhMuc", tHUOC.ID_DANHMUC);
            ViewBag.ID_NSX = new SelectList(db.NHA_SAN_XUAT, "ID", "TEN_NSX", tHUOC.ID_NSX);
            return View(tHUOC);
        }

        // GET: User/Thuoc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THUOC tHUOC = db.THUOCs.Find(id);
            if (tHUOC == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_DANHMUC = new SelectList(db.DANH_MUC_THUOC, "ID", "DanhMuc", tHUOC.ID_DANHMUC);
            ViewBag.ID_NSX = new SelectList(db.NHA_SAN_XUAT, "ID", "TEN_NSX", tHUOC.ID_NSX);
            return View(tHUOC);
        }

        // POST: User/Thuoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_THUOC,TEN_THUOC,LIEU_LUONG,MO_TA,HINH_ANH,ID_NSX,ID_DANHMUC")] THUOC tHUOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHUOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_DANHMUC = new SelectList(db.DANH_MUC_THUOC, "ID", "DanhMuc", tHUOC.ID_DANHMUC);
            ViewBag.ID_NSX = new SelectList(db.NHA_SAN_XUAT, "ID", "TEN_NSX", tHUOC.ID_NSX);
            return View(tHUOC);
        }

        // GET: User/Thuoc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THUOC tHUOC = db.THUOCs.Find(id);
            if (tHUOC == null)
            {
                return HttpNotFound();
            }
            return View(tHUOC);
        }

        // POST: User/Thuoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            THUOC tHUOC = db.THUOCs.Find(id);
            db.THUOCs.Remove(tHUOC);
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
