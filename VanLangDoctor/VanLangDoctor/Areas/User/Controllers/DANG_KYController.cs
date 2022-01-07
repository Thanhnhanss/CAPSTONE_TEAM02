using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.User.Controllers
{
    public class DANG_KYController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: User/DANG_KY
        public ActionResult Index()
        {
            var dANG_KY = db.DANG_KY.Include(d => d.KHOA);
            return View(dANG_KY.ToList());
        }

        // GET: User/DANG_KY/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANG_KY dANG_KY = db.DANG_KY.Find(id);
            if (dANG_KY == null)
            {
                return HttpNotFound();
            }
            return View(dANG_KY);
        }

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
        public ActionResult Create(VanLangDoctor.Models.sendMail model, DANG_KY dANG_KY)
        {
            if (ModelState.IsValid)
            {
                db.DANG_KY.Add(dANG_KY);
                db.SaveChanges();

                //gửi cho admin
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/template.html"));
                content = content.Replace("{{CustomerName}}", dANG_KY.HO_TEN.ToUpper());
                content = content.Replace("{{Email}}", dANG_KY.EMAIL);
                content = content.Replace("{{Job}}", dANG_KY.NGHE_NGHIEP);
                var toEmail = ConfigurationManager.AppSettings["toEmail"].ToString();
                new MailHelper().SendMail(toEmail, "Đơn đăng ký mới", content);

                //gửi cho khách hàng
                string substance = System.IO.File.ReadAllText(Server.MapPath("~/Content/Template_Email/index.html"));
                substance = substance.Replace("{{CustomerName}}", dANG_KY.HO_TEN.ToUpper());
                new MailHelper().SendMail(dANG_KY.EMAIL, "Đơn đăng ký làm tư vấn viên cho VLDoctor", substance);

                return RedirectToAction("Index");
            }
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", dANG_KY.ID_KHOA);
            return View(dANG_KY);
        }

        // GET: User/DANG_KY/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANG_KY dANG_KY = db.DANG_KY.Find(id);
            if (dANG_KY == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", dANG_KY.ID_KHOA);
            return View(dANG_KY);
        }

        // POST: User/DANG_KY/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HO_TEN,NGAY_SINH,GIOI_TINH,NGHE_NGHIEP,HINH_ANH,MUC_TIEU,HOC_VAN,CHUNG_CHI,SDT,ID_KHOA,EMAIL,TRANG_THAI")] DANG_KY dANG_KY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dANG_KY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_KHOA = new SelectList(db.KHOAs, "ID_KHOA", "TEN_KHOA", dANG_KY.ID_KHOA);
            return View(dANG_KY);
        }

        // GET: User/DANG_KY/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANG_KY dANG_KY = db.DANG_KY.Find(id);
            if (dANG_KY == null)
            {
                return HttpNotFound();
            }
            return View(dANG_KY);
        }

        // POST: User/DANG_KY/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DANG_KY dANG_KY = db.DANG_KY.Find(id);
            db.DANG_KY.Remove(dANG_KY);
            db.SaveChanges();
            return RedirectToAction("Index");
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
