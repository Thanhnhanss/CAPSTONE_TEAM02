using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;
using System.Transactions;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    public class QL_TinTucController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/QL_TinTuc
        public ActionResult Index_TT()
        {
            return View(db.TIN_TUC.ToList());
        }

        // GET: Admin/QL_TinTuc/Details/5
        public ActionResult Details_TT(int? id)
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
        public ActionResult Create_TT()
        {
            return View();
        }

        // POST: Admin/QL_TinTuc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create_TT([Bind(Include = "ID_TIN_TUC,TEN_BAI_VIET,NGAY_DANG,NOI_DUNG,TAC_GIA,SEO_TITLE,HINH_ANH")] TIN_TUC tIN_TUC, HttpPostedFileBase picture)
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
            }
            return RedirectToAction("Index_TT");
        }

        // GET: Admin/QL_TinTuc/Edit/5
        public ActionResult Edit_TT(int? id)
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

        // POST: Admin/QL_TinTuc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_TT([Bind(Include = "ID_TIN_TUC,TEN_BAI_VIET,NGAY_DANG,NOI_DUNG,TAC_GIA,SEO_TITLE,HINH_ANH")] TIN_TUC tIN_TUC, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                if(ModelState.IsValid)
                {
                    using (var scope = new TransactionScope())
                    {
                        db.Entry(tIN_TUC).State = EntityState.Modified;
                        db.SaveChanges();

                        if(picture != null)
                        {
                            var path = Server.MapPath(PICTURE_PATH);
                            picture.SaveAs(path + tIN_TUC.ID_TIN_TUC);
                        }

                        scope.Complete();
                        return RedirectToAction("Index_TT");
                    }
                }
                db.Entry(tIN_TUC).State=EntityState.Modified;
            }
            return View(tIN_TUC);
        }

        // GET: Admin/QL_TinTuc/Delete/5
        public ActionResult Delete_TT(int? id)
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
        [HttpPost, ActionName("Delete_TT")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TIN_TUC tIN_TUC = db.TIN_TUC.Find(id);
            db.TIN_TUC.Remove(tIN_TUC);
            db.SaveChanges();
            return RedirectToAction("Index_TT");
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
