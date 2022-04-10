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
    public class ThuocController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: User/Thuoc
        public ActionResult Index()
        {
            
            var tHUOCs = db.THUOCs.Include(t => t.DANH_MUC_THUOC).Include(t => t.NHA_SAN_XUAT);
            return View(tHUOCs.ToList());
        }
        private const string PICTURE_PATH = "~/Content/IMG_MEDICINE/";

        public ActionResult Picture(int ID_Thuoc)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + ID_Thuoc, "images");
        }
        public ActionResult Details(int? id)
        {
            ViewBag.Thuockhac = GetSameMedicine();
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

        private List<THUOC> GetSameMedicine()
        {
            return db.THUOCs.OrderBy(t => t.ID_DANHMUC).ToList();
        }
    }
}
