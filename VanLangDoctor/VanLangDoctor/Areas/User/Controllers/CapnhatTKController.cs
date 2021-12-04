using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.User.Controllers
{
    [Authorize]
    public class CapnhatTKController : Controller
    {
        CP24Team02Entities db = new CP24Team02Entities();
        // GET: SinhVien/CapnhatTaiKhoan
        public ActionResult CapnhatTK(string id)
        {
            var user = db.BENH_NHAN.Find(id) ?? new BENH_NHAN
            {
                ID_BENH_NHAN = int.Parse(id),
                TEN_BN = "",
                GIOI_TINH ="",
                NGAY_SINH = DateTime.Parse(""),
                SDT = "",
                AspNetUser = db.AspNetUsers.Find(id)
            };
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Luu(string id, string hovaten, string sdt)
        {
            var user = db.BENH_NHAN.Find(id);
            if (user == null)
            {
                user = new BENH_NHAN
                {
                    ID_BENH_NHAN = int.Parse(id),
                    TEN_BN = hovaten,
                    SDT = sdt
                };
                db.BENH_NHAN.Add(user);
            }
            else
            {
                user.TEN_BN = hovaten;
                user.SDT = sdt;
                db.Entry<BENH_NHAN>(user).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            ViewBag.message = "Cập nhật thành công";
            return View("CapnhatTK", user);
        }
    }
}