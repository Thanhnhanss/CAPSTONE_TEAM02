using QuickMailer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    [Authorize(Roles = "Quản trị viên")]
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
            var userRoles = user.AspNetRoles.ToList();
            ViewBag.role = userRoles.Count > 0 ? userRoles[0].Id : string.Empty;
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
            if (user.AspNetRoles.Count > 0)
                user.AspNetRoles.Remove(user.AspNetRoles.ToList()[0]);
            user.AspNetRoles.Add(db.AspNetRoles.Find(role));
            db.SaveChanges();
            TempData["Success"] = "Phân quyền thành công";

            #region send mail
            var rolename = db.AspNetRoles.Find(role);
            string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/Template_Email/SendMailCF.html"));
            content = content.Replace("{{Role}}", rolename.Name.ToUpper());
            content = content.Replace("{{LinkDR}}", "http://cntttest.vanlanguni.edu.vn:18080/CP24Team02/trang-chu-quan-ly");
            var fromEmail = ConfigurationManager.AppSettings["fromEmail"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["fromEmailPassword"].ToString();

            Email email = new Email();
            email.SendEmail(username, fromEmail, fromEmailPassword, "Phân quyền thành công", content);
            #endregion

            return Redirect("~/Admin/QL_TaiKhoan/TaiKhoan");
        }
    }
}