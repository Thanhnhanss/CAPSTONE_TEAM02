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
        public ActionResult SoKhamBenh(int Id)
        {
            var skb = db.SO_KHAM_BENH.Include(s => s.BENH_NHAN).Include(s => s.ID_SOKHAMBENH).Where(s => s.ID_SOKHAMBENH == Id);
            return View(skb);
        }


        //GET: User/So_Kham_Benh/Update
        [HttpGet]
        public ActionResult Update_SKB(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var skb = db.SO_KHAM_BENH.Find(Id);
            if (skb == null)
            {
                return HttpNotFound();
            }
            return View(skb);
        }


        //POST: User/So_Kham_Benh/Update
        [HttpPost]
        public ActionResult Update_SKB(SO_KHAM_BENH sO_KHAM_BENH)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    db.Entry(sO_KHAM_BENH).State = EntityState.Modified;
                    db.SaveChanges();

                    scope.Complete();
                    TempData["Success"] = "Cập nhật thành công!!!";
                    return RedirectToAction("SoKhamBenh");
                }
            }
            db.Entry(sO_KHAM_BENH).State = EntityState.Modified;
            TempData["Success"] = "Cập nhật thành công!!!";

            return View(sO_KHAM_BENH);
        }


        //POST: User/So_Kham_Benh/Create
        public ActionResult Create_SKB(SO_KHAM_BENH sO_KHAM_BENH)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    db.SO_KHAM_BENH.Add(sO_KHAM_BENH);
                    db.SaveChanges();

                    scope.Complete();
                }
            }
            TempData["Success"] = "Đăng ký sổ thành công!!!";

            return RedirectToAction("SoKhamBenh");
        }
    }
}