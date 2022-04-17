using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    [Authorize(Roles = "Quản trị viên, Quản lý")]
    public class QL_TinTucController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/QL_TinTuc
        public ActionResult Index()
        {
            var tIN_TUC = db.TIN_TUC.Include(t => t.DANH_MUC_TIN);
            return View(db.TIN_TUC.ToList());
        }

        // GET: Admin/QL_TinTuc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_TUC tIN_TUC = db.TIN_TUC.Find(id);
            if (tIN_TUC == null)
            {
                return HttpNotFound();
            }
            return View(tIN_TUC);
        }

        private const string PICTURE_PATH = "~/Content/IMG_NEWS/";

        public ActionResult Picture(int ID_Tin_Tuc)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + ID_Tin_Tuc, "images");
        }

        // GET: Admin/QL_TinTuc/Create
        public ActionResult Create()
        {
            ViewBag.ID_Danhmuc_tin = new SelectList(db.DANH_MUC_TIN, "ID", "Danhmuc_tin");
            return View();
        }

        // POST: Admin/QL_TinTuc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TIN_TUC tIN_TUC, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        tIN_TUC.NGAY_DANG = DateTime.Now;
                        db.TIN_TUC.Add(tIN_TUC);
                        db.SaveChanges();

                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + tIN_TUC.ID_TIN_TUC);

                        scope.Complete();
                    }
                }
                else ModelState.AddModelError("", "Hình ảnh không được tìm thấy.");
                TempData["Success"] = "Thêm tin mới thành công";
            }
            ViewBag.ID_Danhmuc_tin = new SelectList(db.DANH_MUC_TIN, "ID", "Danhmuc_tin", tIN_TUC.ID_Danhmuc_tin);
            return RedirectToAction("Index");
        }

        // GET: Admin/QL_TinTuc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_TUC tIN_TUC = db.TIN_TUC.Find(id);
            if (tIN_TUC == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Danhmuc_tin = new SelectList(db.DANH_MUC_TIN, "ID", "Danhmuc_tin", tIN_TUC.ID_Danhmuc_tin);
            return View(tIN_TUC);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TIN_TUC tIN_TUC, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    using (var scope = new TransactionScope())
                    {
                        db.Entry(tIN_TUC).State = EntityState.Modified;
                        db.SaveChanges();

                        if (picture != null)
                        {
                            var path = Server.MapPath(PICTURE_PATH);
                            picture.SaveAs(path + tIN_TUC.ID_TIN_TUC);
                        }

                        scope.Complete();
                        TempData["Success"] = "Cập nhật tin thành công";
                        return RedirectToAction("Index");
                    }
                }
                db.Entry(tIN_TUC).State = EntityState.Modified;
            }
            ViewBag.ID_Danhmuc_tin = new SelectList(db.DANH_MUC_TIN, "ID", "Danhmuc_tin", tIN_TUC.ID_Danhmuc_tin);

            return View(tIN_TUC);
        }

        // GET: Admin/QL_TinTuc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_TUC tIN_TUC = db.TIN_TUC.Find(id);
            if (tIN_TUC == null)
            {
                return HttpNotFound();
            }
            return View(tIN_TUC);
        }

        // POST: Admin/QL_TinTuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TIN_TUC tIN_TUC = db.TIN_TUC.Find(id);
            db.TIN_TUC.Remove(tIN_TUC);
            db.SaveChanges();
            TempData["Success"] = "Xóa tin thành công";
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
