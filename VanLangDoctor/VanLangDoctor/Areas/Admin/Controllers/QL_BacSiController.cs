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
    [Authorize]
    public class QL_BacSiController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/QL_BacSi
        public ActionResult Bac_Si()
        {
            var bACSIs = db.BACSIs.Include(b => b.KHOA);
            return View(bACSIs.ToList());
        }

        // GET: Admin/QL_BacSi/Details/5
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

        private const string PICTURE_PATH = "~/Content/IMG_DOCTOR/";

        public ActionResult Picture(int ID_BacSi)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + ID_BacSi, "images");
        }

        // GET: Admin/QL_BacSi/Create
        public ActionResult Create()
        {
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA");
            return View();
        }

        // POST: Admin/QL_BacSi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TEN_BACSI,NGAYSINH_BS,GIOI_TINH,SDT,EMAIL,NGHE_NGHIEP,ID_KHOA,KINH_NGHIEM,NGAY_TRUC")] BACSI bACSI, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                var doc_tor = new BACSI();
                if (picture != null)
                {
                    var path = Server.MapPath(PICTURE_PATH);

                    using (var scope = new TransactionScope())
                    {
                        doc_tor.ID_BACSI = bACSI.ID_BACSI;
                        doc_tor.TEN_BACSI = bACSI.TEN_BACSI;
                        doc_tor.NGAYSINH_BS = bACSI.NGAYSINH_BS;
                        doc_tor.GIOI_TINH = bACSI.GIOI_TINH;
                        doc_tor.SDT = bACSI.SDT;
                        doc_tor.EMAIL = bACSI.EMAIL;
                        doc_tor.NGHE_NGHIEP = bACSI.NGHE_NGHIEP;
                        doc_tor.ID_KHOA = bACSI.ID_KHOA;
                        doc_tor.KINH_NGHIEM = bACSI.KINH_NGHIEM;
                        doc_tor.NGAY_TRUC = bACSI.NGAY_TRUC;
                        doc_tor.HINH_ANH = "";

                        db.BACSIs.Add(doc_tor);
                        db.SaveChanges();

                        picture.SaveAs(path + doc_tor.ID_BACSI);
                        doc_tor.HINH_ANH = path + doc_tor.ID_BACSI;
                        db.Entry(doc_tor).State = EntityState.Modified;
                        db.SaveChanges();

                        scope.Complete();
                    }
                }
                else ModelState.AddModelError("", "Hình ảnh không được tìm thấy");
            }
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", bACSI.ID_KHOA);
            return RedirectToAction("Bac_Si");
        }

        // GET: Admin/QL_BacSi/Edit/5
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

        // POST: Admin/QL_BacSi/Edit/5
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
                return RedirectToAction("Bac_Si");
            }
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", bACSI.ID_KHOA);
            return View(bACSI);
        }

        // GET: Admin/QL_BacSi/Delete/5
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

        // POST: Admin/QL_BacSi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BACSI bACSI = db.BACSIs.Find(id);
            db.BACSIs.Remove(bACSI);
            db.SaveChanges();
            return RedirectToAction("Bac_Si");
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
