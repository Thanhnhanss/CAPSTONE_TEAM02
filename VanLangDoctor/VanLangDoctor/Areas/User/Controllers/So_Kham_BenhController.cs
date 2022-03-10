using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.User.Controllers
{
    public class So_Kham_BenhController : Controller
    {
        CP24Team02Entities db = new CP24Team02Entities();

        // GET: User/So_Kham_Benh
        public ActionResult SoKhamBenh(int? id)
        {
            var skb = db.SO_KHAM_BENH.FirstOrDefault(s => s.ID_BENH_NHAN == id);

            if (skb == null)
            {
                return RedirectToAction("DangKy_SKB");
            }
            return View(skb);

        }
        public ActionResult DangKy_SKB()
        {
            ViewBag.ID_BENH_NHAN = new SelectList(db.BENH_NHAN, "ID_BENH_NHAN", "TEN_BN");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST: User/Dang ky So
        public ActionResult DangKy_SKB(SO_KHAM_BENH sO_KHAM_BENH)
        {
            var skb = db.SO_KHAM_BENH.FirstOrDefault(s => s.ID_BENH_NHAN == sO_KHAM_BENH.ID_BENH_NHAN);
            if (ModelState.IsValid)
            {
                db.SO_KHAM_BENH.Add(sO_KHAM_BENH);
                db.SaveChanges();
                return RedirectToAction("SoKhamBenh");
            }
            return RedirectToAction("SoKhamBenh");
        }
    }
}