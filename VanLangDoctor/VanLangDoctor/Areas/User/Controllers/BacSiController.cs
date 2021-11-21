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
    public class BacSiController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: User/BacSi
        public ActionResult Bac_Si()
        {
            var bACSIs = db.BACSIs.Include(b => b.KHOA);
            return View(bACSIs.ToList());
        }

        // GET: User/BacSi/Details/5
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
