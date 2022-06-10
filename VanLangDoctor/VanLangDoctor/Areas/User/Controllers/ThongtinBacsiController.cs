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
    [HandleError]
    [Authorize]
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

        public ActionResult thongtinbacsi(int? ID_BACSI)
        {
            if (ID_BACSI == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BACSI bACSI = db.BACSIs.Find(ID_BACSI);
            if (bACSI == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userid = User.Identity.GetUserId();
            var ourRating = db.DANH_GIA.Where(d => d.ID_BACSI == ID_BACSI && d.ID_USER == userid).ToList();
            ViewBag.OurRating = ourRating;
            var danhgia = db.DANH_GIA.Where(d => d.ID_BACSI == ID_BACSI && d.TRANG_THAI == true).ToList();
            ViewBag.DanhGia = danhgia;
            ViewBag.ID_BACSI = ID_BACSI.Value;
            var comments = db.DANH_GIA.Where(d => d.ID_BACSI == bACSI.ID_BACSI).ToList();
            ViewBag.Comments = comments;
            var ratings = db.DANH_GIA.Where(d => d.ID_BACSI == bACSI.ID_BACSI && d.TRANG_THAI == true).ToList();
            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.RATING);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
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