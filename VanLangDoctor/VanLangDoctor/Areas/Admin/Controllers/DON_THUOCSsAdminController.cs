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
    public class DON_THUOCSsAdminController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();
        private List<int> DONTHUOC = new List<int>();
        // GET: Admin/DON_THUOCSsAdmin
        public ActionResult Index()
        {
            var dltbs = db.BACSIs.ToList();
            List<SelectListItem> idbs = new List<SelectListItem>();
            idbs.Add(new SelectListItem { Value = "", Text = "------------------Tất cả------------------", Selected = true });
            foreach (var item in dltbs)
            {
                idbs.Add(new SelectListItem { Value = item.ID_BACSI.ToString(), Text = item.TEN_BACSI.ToString() });
            }
            ViewBag.ID_DONTHUOC = idbs;
            return View();
        }
        //public JsonResult jsonDT(int? dt)
        //{
        //    var data = (from objDT in db.DON_THUOC
        //                select new ViewDONTHUOC()
        //                {

        //                    IDDT = objDT.ID_DON_THUOC,
        //                    CHUANDOAN = objDT.CHUAN_DOAN,
        //                    CHIDINH = objDT.CHI_DINH,
        //                    LOIDAN = objDT.LOI_DAN,
        //                    NGAYLAP = objDT.NGAY_LAP,
        //                    IDTHUOC = (int)objDT.ID_THUOC,
        //                    ID_BACSI = objDT.ID_BACSI.ToString(),



        //                }).ToList();

        //    int start = Convert.ToInt32(Request["start"]);
        //    int length = Convert.ToInt32(Request["length"]);
        //    string sText = Request["search[value]"].ToLower();
        //    int row = data.Count();
        //    if (!string.IsNullOrEmpty(sText) && dt != null)
        //        data = data.Where(m => m.ID_BACSI.ToLower().Contains(sText) && m.ID_BACSI == dt).ToList();
        //    else if (!string.IsNullOrEmpty(sText) && dt == null)
        //        data = data.Where(m => m.TENBACSI.ToLower().Contains(sText)).ToList();
        //    else if (string.IsNullOrEmpty(sText) && dt != null)
        //        data = data.Where(m => m.IDKHOA == dt).ToList();
        //    int rowfilter = data.Count();
        //    data = data.Skip(start).Take(length).ToList();
        //    return Json(new { data = data, draw = Request["draw"], recordsTotal = row, recordsFiltered = rowfilter }, JsonRequestBehavior.AllowGet);
        //}

        // GET: Admin/DON_THUOCSsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DON_THUOC dON_THUOC = db.DON_THUOC.Find(id);
            if (dON_THUOC == null)
            {
                return HttpNotFound();
            }
            return View(dON_THUOC);
        }

        // GET: Admin/DON_THUOCSsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI");
            ViewBag.ID_THUOC = new SelectList(db.THUOCs, "ID_THUOC", "TEN_THUOC");
            return View();
        }

        // POST: Admin/DON_THUOCSsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_DON_THUOC,CHUAN_DOAN,CHI_DINH,LOI_DAN,NGAY_LAP,ID_THUOC,ID_BACSI")] DON_THUOC dON_THUOC)
        {
            if (ModelState.IsValid)
            {
                db.DON_THUOC.Add(dON_THUOC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dON_THUOC.ID_BACSI);
            ViewBag.ID_THUOC = new SelectList(db.THUOCs, "ID_THUOC", "TEN_THUOC", dON_THUOC.ID_THUOC);
            return View(dON_THUOC);
        }

        // GET: Admin/DON_THUOCSsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DON_THUOC dON_THUOC = db.DON_THUOC.Find(id);
            if (dON_THUOC == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dON_THUOC.ID_BACSI);
            ViewBag.ID_THUOC = new SelectList(db.THUOCs, "ID_THUOC", "TEN_THUOC", dON_THUOC.ID_THUOC);
            return View(dON_THUOC);
        }

        // POST: Admin/DON_THUOCSsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_DON_THUOC,CHUAN_DOAN,CHI_DINH,LOI_DAN,NGAY_LAP,ID_THUOC,ID_BACSI")] DON_THUOC dON_THUOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dON_THUOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dON_THUOC.ID_BACSI);
            ViewBag.ID_THUOC = new SelectList(db.THUOCs, "ID_THUOC", "TEN_THUOC", dON_THUOC.ID_THUOC);
            return View(dON_THUOC);
        }

        // GET: Admin/DON_THUOCSsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DON_THUOC dON_THUOC = db.DON_THUOC.Find(id);
            if (dON_THUOC == null)
            {
                return HttpNotFound();
            }
            return View(dON_THUOC);
        }

        // POST: Admin/DON_THUOCSsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DON_THUOC dON_THUOC = db.DON_THUOC.Find(id);
            db.DON_THUOC.Remove(dON_THUOC);
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
