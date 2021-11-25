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
    [Authorize]
    public class QL_ThuocController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/QL_Thuoc
        public ActionResult Thuoc()
        {
            var tHUOCs = db.THUOCs.Include(t => t.NHA_SAN_XUAT);
            return View(tHUOCs.ToList());
        }

        // GET: Admin/QL_Thuoc/Details/5
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

        private const string PICTURE_PATH = "~/Content/IMG_MEDICINE/";

        public ActionResult Picture(int ID_Thuoc)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + ID_Thuoc, "images");
        }

        // GET: Admin/QL_Thuoc/Create
        public ActionResult Create()
        {
            ViewBag.ID_NSX = new SelectList(db.NHA_SAN_XUAT, "ID", "TEN_NSX");
            return View();
        }

        // POST: Admin/QL_Thuoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_THUOC,TEN_THUOC,LIEU_LUONG,MO_TA,HINH_ANH,ID_NSX")] THUOC tHUOC, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                var thuoc = new THUOC();
                if (picture != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        thuoc.ID_THUOC = tHUOC.ID_THUOC;
                        thuoc.TEN_THUOC = tHUOC.TEN_THUOC;
                        thuoc.LIEU_LUONG = tHUOC.LIEU_LUONG;
                        thuoc.MO_TA = tHUOC.MO_TA;
                        thuoc.ID_NSX = tHUOC.ID_NSX;

                        db.THUOCs.Add(thuoc);
                        db.SaveChanges();

                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + thuoc.ID_THUOC);

                        scope.Complete();
                    }
                }
                else ModelState.AddModelError("", "Hình ảnh không được tìm thấy.");
            }
            ViewBag.ID_NSX = new SelectList(db.NHA_SAN_XUAT, "ID", "TEN_NSX", tHUOC.ID_NSX);
            return RedirectToAction("Thuoc");
        }

        // GET: Admin/QL_Thuoc/Edit/5
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
            ViewBag.ID_NSX = new SelectList(db.NHA_SAN_XUAT, "ID", "TEN_NSX", tHUOC.ID_NSX);
            return View(tHUOC);
        }

        // POST: Admin/QL_Thuoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_THUOC,TEN_THUOC,LIEU_LUONG,MO_TA,HINH_ANH,ID_NSX")] THUOC tHUOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHUOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Thuoc");
            }
            ViewBag.ID_NSX = new SelectList(db.NHA_SAN_XUAT, "ID", "TEN_NSX", tHUOC.ID_NSX);
            return View(tHUOC);
        }

        // GET: Admin/QL_Thuoc/Delete/5
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

        // POST: Admin/QL_Thuoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            THUOC tHUOC = db.THUOCs.Find(id);
            db.THUOCs.Remove(tHUOC);
            db.SaveChanges();
            return RedirectToAction("Thuoc");
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
