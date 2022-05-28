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
    [HandleError]
    public class ThongtinBacsiController : Controller
    {
        CP24Team02Entities db = new CP24Team02Entities();
        private const string PICTURE_PATH = "~/Content/IMG_DOCTOR/";

        // GET: User/ThongtinBacsi
        public ActionResult DanhSachBacsi()
        {
            var bACSIs = db.BACSIs.Include(b => b.AspNetUser).Include(b => b.KHOA);
            return View(bACSIs.ToList());
        }

        // GET: Admin/BACSIs/Details/5
        
        public ActionResult thongtinbacsi(int? ID_BACSI)
        {
            if (ID_BACSI == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BACSI bACSI = db.BACSIs.Find(ID_BACSI);
            if (bACSI == null)
            {
                throw new HttpException(404,"Not Found!");
                //return HttpNotFound();
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