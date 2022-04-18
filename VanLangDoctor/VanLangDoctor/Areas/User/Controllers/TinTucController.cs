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
            ViewBag.Top1 = GetTop1View();
            ViewBag.Top3 = GetTop3Views();
            ViewBag.RecentArticles = GetRecentArticles();
            var TinTuc = db.TIN_TUC.ToList();
            return View(TinTuc);
        }

        private const string PICTURE_PATH = "~/Content/IMG_NEWS/";

        public ActionResult Picture(int ID_Tin_Tuc)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + ID_Tin_Tuc, "images");
        }

        // GET: User/TinTuc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_TUC tIN_TUC = db.TIN_TUC.Find(id);
            if (tIN_TUC == null)
            {
                return HttpNotFound();
            }
            tIN_TUC.CountViews += 1;
            db.SaveChanges();
            ViewBag.Top = GetTop3Views();
            return View(tIN_TUC);
        }

        //GET: TOP 1 View
        public TIN_TUC GetTop1View()
        {
            return db.TIN_TUC.OrderByDescending(item => item.CountViews).First();
        }


        //GET: Top Views
        private List<TIN_TUC> GetTop3Views()
        {
            return db.TIN_TUC.OrderByDescending(t => t.CountViews).Skip(1).Take(3).ToList();
        }

        //GET: Recent Articles
        public List<TIN_TUC> GetRecentArticles()
        {
            return db.TIN_TUC.OrderByDescending(t=>t.NGAY_DANG).Take(5).ToList();
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
