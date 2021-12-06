using Microsoft.AspNet.Identity;
using System.Linq;
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
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("CapnhatTK", new { id = User.Identity.GetUserId() });
            if (id != User.Identity.GetUserId())
                return new HttpStatusCodeResult(403);
            var user = db.BENH_NHAN.FirstOrDefault(benh_nhan => benh_nhan.AspNetUser.Id == id) ?? new BENH_NHAN
            {
                TEN_BN = "",
                GIOI_TINH ="",
                NGAY_SINH = null,
                SDT = "",
                AspNetUser = db.AspNetUsers.Find(id)
            };
            return View(user);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Luu(string id, string hovaten, string sdt)
        {
            var user = db.BENH_NHAN.FirstOrDefault(benhnhan => benhnhan.AspNetUser.Id == id);
            if (user == null)
            {
                user = new BENH_NHAN
                {
                    //ID_BENH_NHAN = int.Parse(id),
                    TEN_BN = hovaten,
                    SDT = sdt,
                    ID_EMAIL = id
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