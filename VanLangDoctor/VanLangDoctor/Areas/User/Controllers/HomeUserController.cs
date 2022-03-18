using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.User.Controllers
{
    public class HomeUserController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: User/HomeUser
        public ActionResult HomeUser()
        {
            ViewBag.Top = GetTopViews();
            ViewBag.Medicine = ListMedicine();
            var doctor = db.BACSIs.ToList();
            return View(doctor);
        }


        /// <summary>
        /// IMG Doctor
        /// </summary>
        //private const string PICTURE_PATH = "~/Image/IMG_DOCTOR/";

        //public ActionResult Picture(int ID_Tin_Tuc)
        //{
        //    var path = Server.MapPath(PICTURE_PATH);
        //    return File(path + ID_Tin_Tuc, "images");
        //}

        /// <summary>
        /// IMG News
        /// </summary>
        //private const string PICTURE_PATHs = "~/Image/IMG_NEWS/";

        //public ActionResult Pictures(int ID_Tin_Tuc)
        //{
        //    var path = Server.MapPath(PICTURE_PATH);
        //    return File(path + ID_Tin_Tuc, "images");
        //}

        //GET: Top Views
        private List<TIN_TUC> GetTopViews()
        {
            return db.TIN_TUC.OrderByDescending(t => t.CountViews).Take(3).ToList();
        }

        //GET: Medicine
        private List<THUOC> ListMedicine()
        {
            return db.THUOCs.OrderBy(t => t.TEN_THUOC).ToList();
        }
        public ActionResult GioiThieu()
        {
            return View();
        }
    }
}