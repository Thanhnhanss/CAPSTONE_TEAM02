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
    public class TinTucController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: User/TinTuc
        public ActionResult Tin_Tuc()
        {
            ViewBag.Top = GetTopViews();
            ViewBag.danhmuc = GetDanhMucTin();
            return View(db.TIN_TUC.OrderByDescending(t => t.NGAY_DANG).ToList());
        }

        private const string PICTURE_PATH = "~/Content/IMG_NEWS/";

        public ActionResult Picture(int ID_Tin_Tuc)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + ID_Tin_Tuc, "images");
        }

        // GET: User/TinTuc/Details/5
        public ActionResult Details(int? ID_TIN_TUC)
        {
            if (ID_TIN_TUC == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_TUC tIN_TUC = db.TIN_TUC.Find(ID_TIN_TUC);
            if (tIN_TUC == null)
            {
                return HttpNotFound();
            }
            tIN_TUC.CountViews += 1;
            db.SaveChanges();
            ViewBag.Top = GetTopViews();
            ViewBag.danhmuc = GetDanhMucTin();
            return View(tIN_TUC);
        }

        //GET: Top Views
        private List<TIN_TUC> GetTopViews()
        {
            return db.TIN_TUC.OrderByDescending(t => t.CountViews).Take(3).ToList();
        }
        private List<DANH_MUC_TIN> GetDanhMucTin()
        {
            return db.DANH_MUC_TIN.OrderByDescending(t => t.Danhmuc_tin).ToList();
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