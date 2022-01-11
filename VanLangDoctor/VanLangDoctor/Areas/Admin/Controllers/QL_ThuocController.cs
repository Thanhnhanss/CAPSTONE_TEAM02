using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;
using System.Transactions;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    public class QL_ThuocController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();
        private const string PICTURE_PATH = "~/Content/IMG_MEDICINE/";

        // GET: Admin/THUOCsAdmin
        public ActionResult Index()
        {
            var tHUOCs = db.THUOCs.Include(t => t.NHA_SAN_XUAT);
            return View(tHUOCs.ToList());
        }
        public ActionResult Picture(int ID_THUOC)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + ID_THUOC, "images");
        }

        // GET: Admin/THUOCsAdmin/Details/5
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

        // GET: Admin/THUOCsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.ID_NSX = new SelectList(db.NHA_SAN_XUAT, "ID", "TEN_NSX");
            return View();
        }

        // POST: Admin/THUOCsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_THUOC,TEN_THUOC,LIEU_LUONG,MO_TA,HINH_ANH,ID_NSX")] THUOC tHUOC, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        db.THUOCs.Add(tHUOC);
                        db.SaveChanges();

                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + tHUOC.ID_THUOC);

                        scope.Complete();
                    }
                }
                else ModelState.AddModelError("", "Hình ảnh không được tìm thấy");
            }

            ViewBag.ID_NSX = new SelectList(db.NHA_SAN_XUAT, "ID", "TEN_NSX");
            TempData["Success"] = "Thêm thuốc thành công";
            return RedirectToAction("index");
        }

        // GET: Admin/THUOCsAdmin/Edit/5
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

        // POST: Admin/THUOCsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_THUOC,TEN_THUOC,LIEU_LUONG,MO_TA,HINH_ANH,ID_NSX")] THUOC tHUOC, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    using (var scope = new TransactionScope())
                    {
                        db.Entry(tHUOC).State = EntityState.Modified;
                        db.SaveChanges();

                        if (picture != null)
                        {
                            var path = Server.MapPath(PICTURE_PATH);
                            picture.SaveAs(path + tHUOC.ID_THUOC);
                        }

                        scope.Complete();
                        TempData["Success"] = "Cập nhật thuốc thành công";
                        return RedirectToAction("index");

                    }
                }
                db.Entry(tHUOC).State = EntityState.Modified;
                TempData["Success"] = "Cập nhật thuốc thành công";
            }
            ViewBag.ID_NSX = new SelectList(db.NHA_SAN_XUAT, "ID", "TEN_NSX", tHUOC.ID_NSX);
            TempData["Success"] = "Cập nhật thuốc thành công";
            return View(tHUOC);
        }

        // GET: Admin/THUOCsAdmin/Delete/5
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

        // POST: Admin/THUOCsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            THUOC tHUOC = db.THUOCs.Find(id);
            db.THUOCs.Remove(tHUOC);
            db.SaveChanges();
            TempData["Success"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
    }
}
