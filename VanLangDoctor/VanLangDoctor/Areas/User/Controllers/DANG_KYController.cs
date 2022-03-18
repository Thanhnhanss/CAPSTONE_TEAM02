using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.User.Controllers
{
    public class DANG_KYController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();
        private const string PICTURE_PATH = "~/Content/IMG_DOCTOR/";
        private const string CTC_PATH = "~/Content/IMG_CERTIFICATE/";

       
        // GET: User/DANG_KY/Create
        public ActionResult Create()
        {
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA");
            return View();
        }

        // POST: User/DANG_KY/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DANG_KY dANG_KY, HttpPostedFileBase picture, HttpPostedFileBase picture_CTC)
        {
            if (ModelState.IsValid)
            {
                if (picture != null && picture_CTC != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        db.DANG_KY.Add(dANG_KY);
                        db.SaveChanges();

                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + "ttv" + dANG_KY.ID); ;
                        dANG_KY.HINH_ANH = "avt-" + dANG_KY.HO_TEN.ToLower().Trim() + "-" + dANG_KY.SDT + "-" + dANG_KY.ID;

                        var path_CTC = Server.MapPath(CTC_PATH);
                        picture_CTC.SaveAs(path_CTC + dANG_KY.ID);
                        dANG_KY.CHUNG_CHI = "ctc-" + dANG_KY.HO_TEN.ToLower().Trim() + "-" + dANG_KY.SDT + "-" + dANG_KY.ID;

                        db.SaveChanges();

                        //gửi cho admin
                        string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/Template_Email/template.html"));
                        content = content.Replace("{{CustomerName}}", dANG_KY.HO_TEN.ToUpper());
                        content = content.Replace("{{Email}}", dANG_KY.EMAIL);
                        content = content.Replace("{{Job}}", dANG_KY.NGHE_NGHIEP);
                        var toEmail = ConfigurationManager.AppSettings["toEmail"].ToString();
                        new MailHelper().SendMail(toEmail, "Đơn đăng ký mới", content);

                        //gửi cho khách hàng
                        string substance = System.IO.File.ReadAllText(Server.MapPath("~/Content/Template_Email/index.html"));
                        substance = substance.Replace("{{CustomerName}}", dANG_KY.HO_TEN.ToUpper());
                        new MailHelper().SendMail(dANG_KY.EMAIL, "Đơn đăng ký làm tư vấn viên cho VLDoctor", substance);

                        ViewBag.Message = "Hồ sơ đã được gửi. Vui lòng kiểm tra Email để biết thêm thông tin";
                        scope.Complete();
                    }
                }
                else ModelState.AddModelError("", "Hình ảnh không được tìm thấy");
                return RedirectToAction("HomeUser", "HomeUser");
            }
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", dANG_KY.ID_KHOA);
            return View(dANG_KY);
        }
    }
}
