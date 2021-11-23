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
    public class ThongtinBacsiController : Controller
    {
        CP24Team02Entities db = new CP24Team02Entities();
        private const string PICTURE_PATH = "~/Content/IMG_DOCTOR/";

        // GET: User/ThongtinBacsi
        public ActionResult DanhSachBacsi()
        {
            var bacsi = db.BACSIs.Include(b => b.KHOA);
            return View(bacsi.ToList());
        }

        // GET: Admin/BACSIs/Details/5
        
        public ActionResult thongtinbacsi(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
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