using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    public class QL_DoctorsController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();
        private const string PICTURE_PATH = "~/Image/IMG_DOCTOR/";

        public ActionResult GetAllDoctor()
        {
            var doctors = db.BACSIs.ToList();
            return View(doctors);
        }

        public ActionResult Picture(int ID_BACSI)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + ID_BACSI, "images");
        }

        public ActionResult CreateDoctor()
        {
            ViewBag.ID_Email = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA");
            return View();
        }

        // POST: Admin/QLBacsi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDoctor(BACSI bACSI, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        db.BACSIs.Add(bACSI);
                        db.SaveChanges();

                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + bACSI.ID_BACSI);

                        scope.Complete();
                    }
                }
                else if (picture == null)
                {
                    ModelState.AddModelError("", "Hình ảnh không được tìm thấy");
                    TempData["Failed"] = "Không tìm thấy hình ảnh";
                    return RedirectToAction("CreateDoctor");
                }
            }
            ViewBag.ID_Email = new SelectList(db.AspNetUsers, "Id", "Email", bACSI.ID_Email);
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", bACSI.ID_KHOA);
            TempData["Success"] = "Thêm bác sĩ thành công";
            return RedirectToAction("GetAllDoctor");
        }

        public ActionResult UpdateDoctor(int? id)
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
            ViewBag.ID_Email = new SelectList(db.AspNetUsers, "Id", "Email", bACSI.ID_Email);
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", bACSI.ID_KHOA);
            return View(bACSI);
        }

        // POST: Admin/QLBacsi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateDoctor(BACSI bACSI, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    using (var scope = new TransactionScope())
                    {
                        db.Entry(bACSI).State = EntityState.Modified;
                        db.SaveChanges();

                        if (picture != null)
                        {
                            var path = Server.MapPath(PICTURE_PATH);
                            picture.SaveAs(path + bACSI.ID_BACSI);
                        }

                        scope.Complete();
                        TempData["Success"] = "Cập nhật bác sĩ thành công";
                        return RedirectToAction("GetAllDoctor");

                    }
                }
                db.Entry(bACSI).State = EntityState.Modified;
                TempData["Success"] = "Cập nhật bác sĩ thành công";
            }
            ViewBag.ID_Email = new SelectList(db.AspNetUsers, "Id", "Email", bACSI.ID_Email);
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", bACSI.ID_KHOA);
            TempData["Success"] = "Cập nhật bác sĩ thành công";
            return View(bACSI);
        }

        // GET: Admin/BACSIs/Delete/5
        public ActionResult DeleteDoctor(int? id)
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

        // POST: Admin/BACSIs/Delete/5
        [HttpPost, ActionName("DeleteDoctor")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BACSI bACSI = db.BACSIs.Find(id);
            db.BACSIs.Remove(bACSI);
            db.SaveChanges();
            TempData["Success"] = "Xóa thành công";
            return RedirectToAction("GetAllDoctor");
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