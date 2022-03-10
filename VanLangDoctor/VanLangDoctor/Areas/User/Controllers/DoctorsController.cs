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
	public class DoctorsController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        private const string PICTURE_PATH = "~/Image/IMG_DOCTOR/";

        public ActionResult Doctor()
        {
            var bACSIs = db.BACSIs.Include(b => b.AspNetUser).Include(b => b.KHOA);
            return View(bACSIs.ToList());
        }

        public ActionResult DetailsDoctor(int id)
        {
            BACSI bACSI = db.BACSIs.Find(id);
            if (bACSI == null)
            {
                return HttpNotFound();
            }
            return View(bACSI);
        }


        public ActionResult Picture(int ID_BACSI)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + ID_BACSI, "images");
        }
    }
}