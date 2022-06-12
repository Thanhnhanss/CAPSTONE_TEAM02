using Microsoft.AspNet.Identity;
using QuickMailer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.User.Controllers
{
    public class DanhGiaController : Controller
    {
        private readonly CP24Team02Entities db = new CP24Team02Entities();

        [Authorize]
        public ActionResult Create(int ID_BACSI)
        {
            if (ID_BACSI.Equals(null))
            {
                throw new HttpException(404, "Not Found!");
            }
            var ID_USER = User.Identity.GetUserId();
            var danhgia = db.DANH_GIA.FirstOrDefault(d => d.ID_BACSI == ID_BACSI && d.ID_USER == ID_USER) ?? new DANH_GIA
            {
                ID_BACSI = ID_BACSI,
                ID_USER = ID_USER,
                NHAN_XET = "",
                RATING = 0,
                NGAY_TAO = DateTime.Now,
                TRANG_THAI = true,
                AspNetUser = db.AspNetUsers.Find(ID_USER)
            };
            return View(danhgia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string userid, int ID_BACSI, string nhanxet, int rating)
        {
            userid = User.Identity.GetUserId();
            var comments = db.DANH_GIA.Where(d => d.ID_BACSI == ID_BACSI).ToList();
            ViewBag.Comments = comments;
            var danhgia = db.DANH_GIA.FirstOrDefault(d => d.ID_BACSI == ID_BACSI && d.ID_USER == userid);
            if (danhgia == null)
            {
                danhgia = new DANH_GIA
                {
                    ID_USER = userid,
                    ID_BACSI = ID_BACSI,
                    NHAN_XET = nhanxet,
                    RATING = rating,
                    TRANG_THAI = true,
                    NGAY_TAO = DateTime.Now,
                    BACSI = db.BACSIs.Find(ID_BACSI),
                    AspNetUser = db.AspNetUsers.Find(userid)
                };
                db.DANH_GIA.Add(danhgia);
            }
            db.SaveChanges();

            #region sendmail
            string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/Template_Email/Feedback.html"));
            content = content.Replace("{{CustomerName}}", danhgia.AspNetUser.Email);
            content = content.Replace("{{Doctor}}", danhgia.BACSI.TEN_BACSI);
            content = content.Replace("{{Feedback}}", danhgia.NHAN_XET);
            content = content.Replace("{{Date}}", danhgia.NGAY_TAO.ToString());
            content = content.Replace("{{Rating}}", danhgia.RATING.ToString());

            var mailBS = db.AspNetUsers.Find(db.BACSIs.Find(ID_BACSI).ID_Email).Email;
            List<string> toEmail = new List<string>();
            toEmail.Add(danhgia.AspNetUser.Email);
            toEmail.Add(mailBS);
            toEmail.Add(ConfigurationManager.AppSettings["toEmail"].ToString());
            var fromEmail = ConfigurationManager.AppSettings["fromEmail"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["fromEmailPassword"].ToString();
            Email email = new Email();
            email.SendEmail(toEmail, fromEmail, fromEmailPassword, "Đánh giá về bác sĩ", content);
            #endregion

            return RedirectToAction("thongtinbacsi", "ThongtinBacsi", new { ID_BACSI });
        }
        
        // GET: User/DanhGia/Delete/5
        public ActionResult Delete(int id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANH_GIA dANH_GIA = db.DANH_GIA.Find(id);
            if (dANH_GIA == null)
            {
                return HttpNotFound();
            }
            //dANH_GIA.TRANG_THAI = false;
            //db.Entry(dANH_GIA).State = EntityState.Modified;
            db.DANH_GIA.Remove(dANH_GIA);
            db.SaveChanges();
            return RedirectToAction("thongtinbacsi", "ThongtinBacsi", new { dANH_GIA.ID_BACSI });
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
