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
    [Authorize(Roles = "Quản trị viên, Bác sĩ, Quản lý")]
    public class QL_ThuocController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();
        private const string PICTURE_PATH = "~/Content/IMG_MEDICINE/";

        // GET: Admin/THUOCsAdmin
        public ActionResult Index()
        {
            var tHUOCs = db.THUOCs.Include(t => t.DANH_MUC_THUOC).Include(t => t.NHA_SAN_XUAT);
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
            ViewBag.ID_DANHMUC = new SelectList(db.DANH_MUC_THUOC, "ID", "DanhMuc");
            ViewBag.ID_NSX = new SelectList(db.NHA_SAN_XUAT, "ID", "TEN_NSX");
            return View();
        }

        // POST: Admin/THUOCsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(THUOC tHUOC, HttpPostedFileBase picture)
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
                else if (picture == null)
                {
                    ModelState.AddModelError("", "Hình ảnh không được tìm thấy");
                    TempData["Failed"] = "Không tìm thấy hình ảnh";
                    return RedirectToAction("Create");
                }
                TempData["Success"] = "Thêm thuốc thành công";
            }

            ViewBag.ID_DANHMUC = new SelectList(db.DANH_MUC_THUOC, "ID", "DanhMuc", tHUOC.ID_DANHMUC);
            ViewBag.ID_NSX = new SelectList(db.NHA_SAN_XUAT, "ID", "TEN_NSX");
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

            ViewBag.ID_DANHMUC = new SelectList(db.DANH_MUC_THUOC, "ID", "DanhMuc", tHUOC.ID_DANHMUC);
            ViewBag.ID_NSX = new SelectList(db.NHA_SAN_XUAT, "ID", "TEN_NSX", tHUOC.ID_NSX);
            return View(tHUOC);
        }

        // POST: Admin/THUOCsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(THUOC tHUOC, HttpPostedFileBase picture)
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
                        return RedirectToAction("Index");

                    }
                }
                db.Entry(tHUOC).State = EntityState.Modified;
                TempData["Success"] = "Cập nhật thuốc thành công";
            }

            ViewBag.ID_DANHMUC = new SelectList(db.DANH_MUC_THUOC, "ID", "DanhMuc", tHUOC.ID_DANHMUC);
            ViewBag.ID_NSX = new SelectList(db.NHA_SAN_XUAT, "ID", "TEN_NSX", tHUOC.ID_NSX);
            TempData["Success"] = "Cập nhật thuốc thành công";
            return View(tHUOC);
        }

    }
}
