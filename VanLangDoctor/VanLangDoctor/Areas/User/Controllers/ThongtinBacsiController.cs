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
    //[HandleError]
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
        
        //public ActionResult thongtinbacsi(int? ID_BACSI)
        //{
        //    if (ID_BACSI == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BACSI bACSI = db.BACSIs.Find(ID_BACSI);
        //    if (bACSI == null)
        //    {
        //        throw new HttpException(404,"Not Found!");
        //        //return HttpNotFound();
        //    }
        //    return View(bACSI);
        //}

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
            ViewBag.ID_BACSI = ID_BACSI.Value;
            var comments = db.DANH_GIA.Where(d => d.ID_BACSI == bACSI.ID_BACSI).ToList();
            ViewBag.Comments = comments;
            var ratings = db.DANH_GIA.Where(d => d.ID_BACSI == bACSI.ID_BACSI).ToList();
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

        public ActionResult Create()
        {
            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI");
            ViewBag.ID_USER = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: User/DanhGia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_BACSI,ID_BENHNHAN,RATING,NHAN_XET")] DANH_GIA dANH_GIA)
        {
            if (ModelState.IsValid)
            {
                dANH_GIA.ID_USER = User.Identity.GetUserId();
                db.DANH_GIA.Add(dANH_GIA);
                db.SaveChanges();
                return RedirectToAction("DanhSachBacsi");
            }

            ViewBag.ID_USER = new SelectList(db.AspNetUsers, "Id", "Email", dANH_GIA.ID_USER);
            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dANH_GIA.ID_BACSI);
            return View(dANH_GIA);
        }
    }
}