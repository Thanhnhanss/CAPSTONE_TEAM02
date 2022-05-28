using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.User.Controllers
{
    [HandleError]
    [Authorize]
    public class CapnhatTKController : Controller
    {
        CP24Team02Entities db = new CP24Team02Entities();

        // GET: SinhVien/CapnhatTaiKhoan
        public ActionResult CapnhatTK(string id_user, int? id_skb)
        {
            if (string.IsNullOrEmpty(id_user))
                return RedirectToAction("CapnhatTK", new { id_user = User.Identity.GetUserId() });
            if (id_user != User.Identity.GetUserId())
                throw new HttpException(404,"Not Found!");
            var user = db.BENH_NHAN.FirstOrDefault(benh_nhan => benh_nhan.AspNetUser.Id == id_user) ?? new BENH_NHAN
            {
                TEN_BN = "",
                GIOI_TINH ="",
                NGAY_SINH = null,
                SDT = "",
                DIA_CHI = "",
                AspNetUser = db.AspNetUsers.Find(id_user)
            };
            ViewBag.Prescriptions = GetAllPrescriptions(id_skb);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Luu(string id, string hovaten, string sdt, string gioitinh, string ngaysinh, string diachi)
        {
            var user = db.BENH_NHAN.FirstOrDefault(benhnhan => benhnhan.AspNetUser.Id == id);
            if (user == null)
            {
                user = new BENH_NHAN
                {
                    TEN_BN = hovaten,
                    SDT = sdt,
                    GIOI_TINH = gioitinh,
                    NGAY_SINH = DateTime.Parse(ngaysinh),
                    DIA_CHI = diachi,
                    ID_EMAIL = id,
                    AspNetUser = db.AspNetUsers.Find(id)
                };
                db.BENH_NHAN.Add(user);
            }
            else
            {
                user.TEN_BN = hovaten;
                user.SDT = sdt;
                user.GIOI_TINH = gioitinh;
                user.NGAY_SINH = DateTime.Parse(ngaysinh);
                user.DIA_CHI = diachi;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            TempData["Success"] = "Cập nhật thành công";
            return View("CapnhatTK", user);
        }

        [HttpGet]
        public List<DON_THUOC> GetAllPrescriptions(int? id)
        {
            return db.DON_THUOC.OrderByDescending(t => t.NGAY_LAP).Where(t => t.SO_KHAM_BENH.BENH_NHAN.ID_BENH_NHAN == id).ToList();
        }
    }
}