using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    public class QL_TaiKhoanController : Controller
    {
        CP24Team02Entities db = new CP24Team02Entities();
        // GET: Admin/QL_TaiKhoan
        public ActionResult TaiKhoan()
        {
            var danhsach = db.AspNetUsers.ToList();
            return View(danhsach);
        }

        public ActionResult PhanQuyen(string username)
        {
            var user = db.AspNetUsers.SingleOrDefault(item => item.Email == username);
            if (user == null)
            {
                return new HttpStatusCodeResult(404);
            }
            ViewBag.user = username;
            ViewBag.role = user.AspNetRoles.ToList()[0].Id;
            var phanQuyen = db.AspNetRoles.ToList();
            return View(phanQuyen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PhanQuyen(string username, string role)
        {
            var user = db.AspNetUsers.SingleOrDefault(item => item.Email == username);
            if (user == null)
            {
                return new HttpStatusCodeResult(404);
            }
            var roleObj = user.AspNetRoles.ToList()[0];
            user.AspNetRoles.Remove(roleObj);
            user.AspNetRoles.Add(db.AspNetRoles.Find(role));
            db.SaveChanges();
            return Redirect("~/Admin/QL_TaiKhoan/TaiKhoan");
        }
    }
}