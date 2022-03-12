using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.User.Controllers
{
    public class NewsController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: User/TinTuc
        public ActionResult News()
        {
            ViewBag.Top = GetTopViews();
            return View(db.TIN_TUC.ToList());
        }

        private const string PICTURE_PATH = "~/Image/IMG_NEWS/";

        public ActionResult Picture(int ID_Tin_Tuc)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + ID_Tin_Tuc, "images");
        }

        // GET: User/TinTuc/Details/5
        public ActionResult NewsDetail(int? id)
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
            ViewBag.Top = GetTopViews();
            return View(tIN_TUC);
        }

        //GET: Top Views
        private List<TIN_TUC> GetTopViews()
        {
            return db.TIN_TUC.OrderByDescending(t => t.CountViews).Take(3).ToList();
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