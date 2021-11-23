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

namespace VanLangDoctor.Areas.Admin.Controllers
{
    public class BACSIsController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();
        private const string PICTURE_PATH = "~/Content/IMG_DOCTOR/";
        // GET: Admin/BACSIs
        public ActionResult DanhSachBacsi()
        {
            var bACSIs = db.BACSIs.Include(b => b.KHOA);
            return View(bACSIs.ToList());
        }

        public ActionResult Picture(int ID_BACSI)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + ID_BACSI, "images");
        }
        public ActionResult Create()
        {
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA");
            return View();
        }

        // POST: Admin/QLBacsi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_BACSI,TEN_BACSI,NGAYSINH_BS,GIOI_TINH,SDT,EMAIL,HINH_ANH,NGHE_NGHIEP,ID_KHOA,KINH_NGHIEM,NGAY_TRUC")] BACSI bACSI, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                var bs = new BACSI();
                if (picture != null)
                {
                    var path = Server.MapPath(PICTURE_PATH);
                    using (var scope = new TransactionScope())
                    {
                        bs.ID_BACSI = bACSI.ID_BACSI;
                        bs.TEN_BACSI = bACSI.TEN_BACSI;
                        bs.NGAYSINH_BS = bACSI.NGAYSINH_BS;
                        bs.GIOI_TINH = bACSI.GIOI_TINH;
                        bs.SDT = bACSI.SDT;
                        bs.EMAIL = bACSI.EMAIL;
                        bs.NGHE_NGHIEP = bACSI.NGHE_NGHIEP;
                        bs.ID_KHOA = bACSI.ID_KHOA;
                        bs.KINH_NGHIEM = bACSI.KINH_NGHIEM;
                        bs.NGAY_TRUC = bACSI.NGAY_TRUC;
                        bs.HINH_ANH = "";

                        db.BACSIs.Add(bs);
                        db.SaveChanges();

                        picture.SaveAs(path + bs.ID_BACSI);
                        bs.HINH_ANH = path + bs.ID_BACSI;
                        db.Entry(bs).State = EntityState.Modified;
                        db.SaveChanges();

                        scope.Complete();
                    }
                }
                else ModelState.AddModelError("", "Hình ảnh không được tìm thấy");
            }
            else
            {
                 ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", bACSI.ID_KHOA);
            }
            return RedirectToAction("DanhSachBacsi");
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
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", bACSI.ID_KHOA);
            return View(bACSI);
        }

        // POST: Admin/QLBacsi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BACSI bACSI, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                var bs = db.BACSIs.Find(id);
                if (ModelState.IsValid)
                {
                    using (var scope = new TransactionScope())
                    {
                        bs.ID_BACSI = bACSI.ID_BACSI;
                        bs.TEN_BACSI = bACSI.TEN_BACSI;
                        bs.NGAYSINH_BS = bACSI.NGAYSINH_BS;
                        bs.GIOI_TINH = bACSI.GIOI_TINH;
                        bs.SDT = bACSI.SDT;
                        bs.EMAIL = bACSI.EMAIL;
                        bs.NGHE_NGHIEP = bACSI.NGHE_NGHIEP;
                        bs.ID_KHOA = bACSI.ID_KHOA;
                        bs.KINH_NGHIEM = bACSI.KINH_NGHIEM;
                        bs.NGAY_TRUC = bACSI.NGAY_TRUC;

                        db.Entry(bs).State = EntityState.Modified;
                        db.SaveChanges();

                        if (picture != null)
                        {
                            var path = Server.MapPath(PICTURE_PATH);
                            picture.SaveAs(path + bs.ID_BACSI);
                            bs.HINH_ANH = path + bs.ID_BACSI;

                            db.Entry(bs).State = EntityState.Modified;
                            db.SaveChanges();
                        }

                        scope.Complete();
                        return RedirectToAction("DanhSachBacsi");

                    }
                }
                db.Entry(bACSI).State = EntityState.Modified;
            }
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", bACSI.ID_KHOA);
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
            return RedirectToAction("DanhSachBacsi");
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
