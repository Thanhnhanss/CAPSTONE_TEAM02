using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.User.Controllers
{
    public class SoKhamBenhController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        //GET: User/So Kham Benh
        public ActionResult So_Kham_Benh(int UserId)
        {
            var skb = db.SO_KHAM_BENH.FirstOrDefault(s => s.ID_BENH_NHAN == UserId);
            if(skb == null)
            {
                return RedirectToAction("Index");
            }
            return View(skb);
        }

        //POST: User/Dang ky So
        public ActionResult DangKy_SoKhamBenh(SO_KHAM_BENH sO_KHAM_BENH)
        {
            var skb = db.SO_KHAM_BENH.FirstOrDefault(s => s.ID_BENH_NHAN == sO_KHAM_BENH.ID_BENH_NHAN);
            if (skb == null)
            {
                db.SO_KHAM_BENH.Add(sO_KHAM_BENH);
                db.SaveChanges();
            }
            return RedirectToAction("SoKhamBenh");
        }
    }
}