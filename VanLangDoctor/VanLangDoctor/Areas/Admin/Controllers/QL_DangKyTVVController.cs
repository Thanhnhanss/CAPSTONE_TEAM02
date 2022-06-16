using QuickMailer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    [Authorize(Roles = "Quản trị viên, Quản lý")]
    public class QL_DangKyTVVController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();
        private const string PICTURE_PATH = "~/Content/IMG_DOCTOR/";
        private const string CTC_PATH = "~/Content/IMG_CERTIFICATE/";

        public ActionResult Picture(int ID)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + "ttv" +ID, "images");
        }
        public ActionResult Certificate(int ID)
        {
            var path_CTC = Server.MapPath(CTC_PATH);
            return File(path_CTC + ID, "images");
        }
        // GET: User/DANG_KY
        public ActionResult Index()
        {
            var dANG_KY = db.DANG_KY.OrderByDescending(n => n.ID).Include(k => k.KHOA);
            return View(dANG_KY.ToList());
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
                //gửi cho khách hàng
                string substance = System.IO.File.ReadAllText(Server.MapPath("~/Content/Template_Email/SendMailCF.html"));
                substance = substance.Replace("{{CustomerName}}", dANG_KY.HO_TEN.ToUpper());
                substance = substance.Replace("{{Role}}", "Bác sĩ");
                substance = substance.Replace("{{LinkDR}}", "http://cntttest.vanlanguni.edu.vn:18080/CP24Team02/trang-chu-quan-ly");

                var fromEmail = ConfigurationManager.AppSettings["fromEmail"].ToString();
                var fromEmailPassword = ConfigurationManager.AppSettings["fromEmailPassword"].ToString();
                new Email().SendEmail(dANG_KY.EMAIL,fromEmail, fromEmailPassword, "Đơn đã được duyệt", substance);
                TempData["Success"] = "Email xác nhận đã được gửi cho ứng viên "+dANG_KY.HO_TEN.ToUpper();
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
    }
}