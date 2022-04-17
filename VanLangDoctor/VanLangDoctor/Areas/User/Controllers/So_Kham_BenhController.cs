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
    public class RegisterConsultationController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: User/So_Kham_Benh
        public ActionResult SoKhamBenh(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(400);
            }
            var skb = db.SO_KHAM_BENH.FirstOrDefault(s => s.ID_BENH_NHAN == id);

            if (skb == null)
            {
                return HttpNotFound();
            }
            return View(skb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST: User/Dang ky So
        public ActionResult RegisterRecord(int id)
        {
            var skb = db.SO_KHAM_BENH.FirstOrDefault(s => s.ID_BENH_NHAN == id);
            if (skb == null)
            {
                db.SO_KHAM_BENH.Add(new SO_KHAM_BENH
                {
                    ID_BENH_NHAN = id
                });
                db.SaveChanges();
            }
            return RedirectToAction("HealthRecord", new { id });
        }
    }
}