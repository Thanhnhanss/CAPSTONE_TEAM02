using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.User.Controllers
{
    [Authorize]
    public class DatLichTuVanController : Controller
    {
        private readonly CP24Team02Entities db = new CP24Team02Entities();

        // GET: User/DatLichTuVan
        public ActionResult Index()
        {
            var dAT_LICH_TU_VAN = db.DAT_LICH_TU_VAN.Include(d => d.AspNetUser).Include(d => d.BACSI);
            return View(dAT_LICH_TU_VAN.ToList());
        }

        // GET: User/DatLichTuVan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAT_LICH_TU_VAN dAT_LICH_TU_VAN = db.DAT_LICH_TU_VAN.Find(id);
            if (dAT_LICH_TU_VAN == null)
            {
                return HttpNotFound();
            }
            return View(dAT_LICH_TU_VAN);
        }

        // GET: User/DatLichTuVan/Create
        public ActionResult Create()
        {
            ViewBag.ID_BAC_SI = new SelectList(db.BACSIs.Where(e=>e.DAT_LICH.Count() > 0), "ID_BACSI", "TEN_BACSI");
            return View();
        }

        // POST: User/DatLichTuVan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DAT_LICH_TU_VAN dAT_LICH_TU_VAN)
        {
            if (ModelState.IsValid)
            {
                dAT_LICH_TU_VAN.ID_USER = User.Identity.GetUserId();

                db.DAT_LICH_TU_VAN.Add(dAT_LICH_TU_VAN);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            //ViewBag.ID_USER = new SelectList(db.AspNetUsers, "Id", "Email", dAT_LICH_TU_VAN.ID_USER);
            ViewBag.ID_BAC_SI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dAT_LICH_TU_VAN.ID_BAC_SI);
            return View(dAT_LICH_TU_VAN);
        }

        // GET: User/DatLichTuVan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAT_LICH_TU_VAN dAT_LICH_TU_VAN = db.DAT_LICH_TU_VAN.Find(id);
            if (dAT_LICH_TU_VAN == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_USER = new SelectList(db.AspNetUsers, "Id", "Email", dAT_LICH_TU_VAN.ID_USER);
            ViewBag.ID_BAC_SI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dAT_LICH_TU_VAN.ID_BAC_SI);
            return View(dAT_LICH_TU_VAN);
        }

        // POST: User/DatLichTuVan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_USER,NGAY_KHAM,ID_BAC_SI,LINK_GG,TRANG_THAI")] DAT_LICH_TU_VAN dAT_LICH_TU_VAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dAT_LICH_TU_VAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_USER = new SelectList(db.AspNetUsers, "Id", "Email", dAT_LICH_TU_VAN.ID_USER);
            ViewBag.ID_BAC_SI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dAT_LICH_TU_VAN.ID_BAC_SI);
            return View(dAT_LICH_TU_VAN);
        }

        // GET: User/DatLichTuVan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAT_LICH_TU_VAN dAT_LICH_TU_VAN = db.DAT_LICH_TU_VAN.Find(id);
            if (dAT_LICH_TU_VAN == null)
            {
                return HttpNotFound();
            }
            return View(dAT_LICH_TU_VAN);
        }

        // POST: User/DatLichTuVan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DAT_LICH_TU_VAN dAT_LICH_TU_VAN = db.DAT_LICH_TU_VAN.Find(id);
            db.DAT_LICH_TU_VAN.Remove(dAT_LICH_TU_VAN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult GetDoctor(int id)
        {
            var bacsi = db.BACSIs.Find(id);
            return Json(new DoctorModel
            {
                ID = bacsi.ID_BACSI,
                Name = bacsi.TEN_BACSI,
                Ngay_Truc = bacsi.DAT_LICH.Select(e => e.NGAY_TRUC.Value.ToString("yyyy/MM/dd HH:mm")).ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        public class DoctorModel
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public List<string> Ngay_Truc { get; set; }

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
