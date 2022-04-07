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
    public class MedicineController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: User/Thuoc
        public ActionResult Medicine()
        {
            var tHUOCs = db.THUOCs.Include(t => t.NHA_SAN_XUAT);
            return View(tHUOCs.ToList());
        }

        // GET: User/Thuoc/Details/5
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

        private const string PICTURE_PATH = "~/Image/IMG_MEDICINE/";

        public ActionResult Picture(int ID_Thuoc)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + ID_Thuoc, "images");
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
