using Microsoft.AspNet.Identity;
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
    public class FeedbackController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        public ActionResult Create(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Create", new { id = User.Identity.GetUserId() });
            if (id != User.Identity.GetUserId())
                return new HttpStatusCodeResult(403);
            var fEEDBACK = db.FEEDBACKs.FirstOrDefault(feed => feed.AspNetUser.Id == id) ?? new FEEDBACK
            {
                TEN_NDG = "",
                SDT = "",
                GOP_Y = null,
                DANH_GIA = "",
                AspNetUser = db.AspNetUsers.Find(id)
            };
            return View(fEEDBACK);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string id, string hovaten, string sdt, string gopy, string danhgia)
        {
            var feedback = db.FEEDBACKs.FirstOrDefault(feed => feed.AspNetUser.Id == id);
            if (feedback == null)
            {
                feedback = new FEEDBACK
                {
                    TEN_NDG = hovaten,
                    SDT = sdt,
                    GOP_Y = gopy,
                    DANH_GIA = danhgia,
                    ID_USER = id,
                    AspNetUser = db.AspNetUsers.Find(id)
                };
                db.FEEDBACKs.Add(feedback);
            }
            else
            {
                feedback.TEN_NDG = hovaten;
                feedback.SDT = sdt;
                feedback.GOP_Y = gopy;
                feedback.DANH_GIA = danhgia;
                db.Entry(feedback).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            //gửi cho admin
            string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/Template_Email/Feedback.html"));
            content = content.Replace("{{CustomerName}}", feedback.TEN_NDG.ToUpper());
            content = content.Replace("{{Mail}}", feedback.AspNetUser.Email);
            content = content.Replace("{{Feedback}}", gopy);
            content = content.Replace("{{Phone}}", sdt);
            content = content.Replace("{{Rating}}", danhgia);
            var toEmail = ConfigurationManager.AppSettings["toEmail"].ToString();
            new MailHelper().SendMail(toEmail, "Phản hồi của khách hàng", content);

            ViewBag.message = "Đánh giá đã được gửi";
            return View("Create", feedback);
        }

    }
}
