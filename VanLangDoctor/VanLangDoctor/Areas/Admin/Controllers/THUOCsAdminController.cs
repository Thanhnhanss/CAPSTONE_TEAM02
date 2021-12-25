using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    public class THUOCsAdminController : Controller
    {
        private CP24Team02Entities db;
        private List<int> nsx;
        public THUOCsAdminController()
        {
            db = new CP24Team02Entities();
            nsx = new List<int>();
        }
        // GET: Admin/THUOCsAdmin
        public ActionResult Index()
        {
            var dlnsx = db.NHA_SAN_XUAT.ToList();
            List<SelectListItem> idnsx = new List<SelectListItem>();
            idnsx.Add(new SelectListItem { Value = "", Text = "------------------Tất cả------------------", Selected = true });
            foreach (var item in dlnsx)
            {
                idnsx.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.TEN_NSX.ToString() });
            }
            ViewBag.ID_NSX = idnsx;
            return View();
        }
        public JsonResult jsonThuoc(int? nsx)
        {
            var data = (from objthuoc in db.THUOCs
                        select new ViewTHUOC()
                        {
                            IDTHUOC = objthuoc.ID_THUOC,
                            LIEULUONG = objthuoc.LIEU_LUONG,
                            MOTA = objthuoc.MO_TA,
                            TENTHUOC = objthuoc.TEN_THUOC,
                            IDNSX = (int)objthuoc.ID_NSX,
                            HINHANH =objthuoc.HINH_ANH


                        }).ToList();

            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string sText = Request["search[value]"].ToLower();
            int row = data.Count();
            if (!string.IsNullOrEmpty(sText) && nsx != null)
                data = data.Where(m => m.TENTHUOC.ToLower().Contains(sText) && m.IDNSX == nsx).ToList();
            else if (!string.IsNullOrEmpty(sText) && nsx == null)
                data = data.Where(m => m.TENTHUOC.ToLower().Contains(sText)).ToList();
            else if (!string.IsNullOrEmpty(sText) && nsx != null)
                data = data.Where(m => m.IDNSX == nsx).ToList();
            int rowfilter = data.Count();
            data = data.Skip(start).Take(length).ToList();
            return Json(new { data = data, draw = Request["draw"], recordsTotal = row, recordsFiltered = rowfilter }, JsonRequestBehavior.AllowGet);
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
        public ActionResult Create([Bind(Include = "ID_THUOC,TEN_THUOC,LIEU_LUONG,MO_TA,HINH_ANH,ID_NSX")] THUOC tHUOC)
        {
            if (ModelState.IsValid)
            {
                db.THUOCs.Add(tHUOC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_NSX = new SelectList(db.NHA_SAN_XUAT, "ID", "TEN_NSX", tHUOC.ID_NSX);
            return View(tHUOC);
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
        public ActionResult Edit([Bind(Include = "ID_THUOC,TEN_THUOC,LIEU_LUONG,MO_TA,HINH_ANH,ID_NSX")] THUOC tHUOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHUOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_NSX = new SelectList(db.NHA_SAN_XUAT, "ID", "TEN_NSX", tHUOC.ID_NSX);
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
