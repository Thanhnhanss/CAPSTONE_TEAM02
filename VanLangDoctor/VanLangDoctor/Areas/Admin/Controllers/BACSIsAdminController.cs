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
    public class BACSIsAdminController : Controller
    {
        
        private CP24Team02Entities db;
        private List<int> BacSi;
        public BACSIsAdminController()
        {
            db = new CP24Team02Entities();
            BacSi = new List<int>();
        }
        // GET: Admin/BACSIsAdmin
        public ActionResult Index()
        {
            var dltk = db.KHOAs.ToList();
            List<SelectListItem> idtk = new List<SelectListItem>();
            idtk.Add(new SelectListItem { Value = "", Text = "------------------Tất cả------------------", Selected = true });
            foreach (var item in dltk)
            {
                idtk.Add(new SelectListItem { Value = item.ID_KHOA.ToString(), Text = item.TEN_KHOA.ToString() });
            }
            ViewBag.ID_KHOA = idtk;
            return View();
        }
        public JsonResult jsonBS(int? tk)
        {
            var data = (from objBS in db.BACSIs
                        select new ViewBACSI()
                        {
                            IDBS = objBS.ID_BACSI,
                            TENBACSI = objBS.TEN_BACSI,
                            SDT = objBS.SDT,
                            EMAIL = objBS.EMAIL,
                            GIOI_TINH = objBS.GIOI_TINH,
                            IDKHOA = (int)objBS.ID_KHOA,
                            NGAYTRUC = objBS.NGAY_TRUC.ToString(),
                            ANH = objBS.HINH_ANH,
                            NGAYSINH = objBS.NGAYSINH_BS.ToString(),
                            NGHENGHIEP = objBS.NGHE_NGHIEP,
                            KINHNGHIEM = objBS.KINH_NGHIEM.ToString()


                        }).ToList();

            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string sText = Request["search[value]"].ToLower();
            int row = data.Count();
            if (!string.IsNullOrEmpty(sText) && tk != null)
                data = data.Where(m => m.TENBACSI.ToLower().Contains(sText) && m.IDKHOA == tk).ToList();
            else if (!string.IsNullOrEmpty(sText) && tk == null)
                data = data.Where(m => m.TENBACSI.ToLower().Contains(sText)).ToList();
            else if (string.IsNullOrEmpty(sText) && tk != null)
                data = data.Where(m => m.IDKHOA == tk).ToList();
            int rowfilter = data.Count();
            data = data.Skip(start).Take(length).ToList();
            return Json(new { data = data, draw = Request["draw"], recordsTotal = row, recordsFiltered = rowfilter }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/BACSIsAdmin/Details/5
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

        // GET: Admin/BACSIsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA");
            return View();
        }

        // POST: Admin/BACSIsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_BACSI,TEN_BACSI,NGAYSINH_BS,GIOI_TINH,SDT,EMAIL,HINH_ANH,NGHE_NGHIEP,ID_KHOA,KINH_NGHIEM,NGAY_TRUC")] BACSI bACSI)
        {
            if (ModelState.IsValid)
            {
                db.BACSIs.Add(bACSI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", bACSI.ID_KHOA);
            return View(bACSI);
        }

        // GET: Admin/BACSIsAdmin/Edit/5
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

        // POST: Admin/BACSIsAdmin/Edit/5
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
                return RedirectToAction("Index");
            }
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", bACSI.ID_KHOA);
            return View(bACSI);
        }

        // GET: Admin/BACSIsAdmin/Delete/5
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

        // POST: Admin/BACSIsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BACSI bACSI = db.BACSIs.Find(id);
            db.BACSIs.Remove(bACSI);
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
