using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using VanLangDoctor.Models;
using Microsoft.AspNet.Identity;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    [Authorize(Roles = "Quản trị viên, Quản lý")]
    public class QL_BacSiController : Controller
    {
        private readonly CP24Team02Entities db = new CP24Team02Entities();
        private const string PICTURE_PATH = "~/Content/IMG_DOCTOR/";
        // GET: Admin/BACSIs
        public ActionResult DanhSach_BS()
        {
            var bACSIs = db.BACSIs.Include(b => b.AspNetUser).Include(b => b.KHOA);
            return View(bACSIs.ToList());
        }

        #region hide Rating Of Dr
        public ActionResult ApproveRating(int? ID_BACSI)
        {
            if (ID_BACSI == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANH_GIA dANH_GIA = db.DANH_GIA.Where(d => d.ID_BACSI == ID_BACSI).FirstOrDefault();
            if (dANH_GIA == null)
            {
                return HttpNotFound();
            }
            var danhgia = db.DANH_GIA.Where(d => d.ID_BACSI == ID_BACSI).Include(x => x.AspNetUser).ToList();
            ViewBag.DanhGia = danhgia;
            return View(dANH_GIA);
        }

        // POST: DANH_GIA/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveRating(DANH_GIA danhgia)
        {
            
            if (ModelState.IsValid)
            {
                if (danhgia.TRANG_THAI == false)
                {
                    danhgia.TRANG_THAI = true;
                }
                else
                {
                    danhgia.TRANG_THAI = false;
                }
                db.Entry(danhgia).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Thành công";
            }
            return RedirectToAction("ApproveRating", "QL_BacSi", new { danhgia.ID_BACSI });

        }
        #endregion

        //[Authorize(Roles = "Bác sĩ")]
        //GET: Admin/QLBacsi/ThongTinBacSi
        public ActionResult ThongTinBacSi()
        {
            var bacsi = User.Identity.GetUserId();
            if (bacsi != User.Identity.GetUserId())
                return new HttpStatusCodeResult(403);
            if (!string.IsNullOrEmpty(bacsi))
            {
                return RedirectToAction("ThongTinBacSi", "QL_BacSi", new { area = "Admin" });
            }
            return View();
        }

        public ActionResult Picture(int ID_BACSI)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + ID_BACSI, "images");
        }
        public ActionResult Create()
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
        public ActionResult Create(BACSI bACSI, HttpPostedFileBase picture)
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
                else if(picture == null)
                {
                    ModelState.AddModelError("", "Hình ảnh không được tìm thấy");
                    TempData["Failed"] = "Không tìm thấy hình ảnh";
                    return RedirectToAction("Create");
                }
            }
            ViewBag.ID_Email = new SelectList(db.AspNetUsers, "Id", "Email", bACSI.ID_Email);
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", bACSI.ID_KHOA);
            TempData["Success"] = "Thêm bác sĩ thành công";
            return RedirectToAction("DanhSach_BS");
        }

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
            ViewBag.ID_Email = new SelectList(db.AspNetUsers, "Id", "Email", bACSI.ID_Email);
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", bACSI.ID_KHOA);
            return View(bACSI);
        }

        // POST: Admin/QLBacsi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BACSI bACSI, HttpPostedFileBase picture)
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
                        return RedirectToAction("DanhSach_BS");

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

        // POST: Admin/BACSIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BACSI bACSI = db.BACSIs.Find(id);
            db.BACSIs.Remove(bACSI);
            db.SaveChanges();
            TempData["Success"] = "Xóa thành công";
            return RedirectToAction("DanhSach_BS");
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
