using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    public class QL_DonThuocController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();
        private List<DON_THUOC> DonThuoc = null;
        public QL_DonThuocController()
        {
            var session = System.Web.HttpContext.Current.Session;
            if (session["DonThuoc"] != null)
                DonThuoc = session["DonThuoc"] as List<DON_THUOC>;
            else {
                DonThuoc = new List<DON_THUOC>();
                session["DonThuoc"] = DonThuoc;
            }
        }
        
        public ActionResult Index()
        {
            var hashtable = new Hashtable();
            foreach(var donthuoc in DonThuoc)
            {
                if (hashtable[donthuoc.THUOC.ID_THUOC] != null)
                {
                    (hashtable[donthuoc.THUOC.ID_THUOC] as DON_THUOC).CHI_DINH += donthuoc.CHI_DINH;
                }
                else hashtable[donthuoc.THUOC.ID_THUOC] = donthuoc;
            }
            DonThuoc.Clear();
            foreach (DON_THUOC donthuoc in hashtable.Values)
                DonThuoc.Add(donthuoc);
            return View(DonThuoc);
        }
        [HttpPost]
        public ActionResult Create(int ID_Thuoc)
        {
            var thuoc = db.THUOCs.Find(ID_Thuoc);
            DonThuoc.Add(new DON_THUOC
            {
                THUOC = thuoc
            });
            ViewBag.ID_BACSI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI");
            ViewBag.ID_THUOC = new SelectList(db.THUOCs, "ID_THUOC", "TEN_THUOC");
            TempData["Success"] = "Thêm thành công";
            return RedirectToAction("Index");
        }

        public ActionResult Delete()
        {
            DonThuoc.Clear();
            Session["DonThuoc"] = DonThuoc;
            TempData["Success"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        
        public ActionResult DS_DonThuoc()
        {
            var model = db.DON_THUOC.ToList();
            return View(model);
        }

        public ActionResult ThemDonThuoc()
        {
            
            ViewBag.Cart = DonThuoc;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemDonThuoc(DON_THUOC model)
        {
            ValidateDonThuoc(model);
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    model.NGAY_LAP = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                    db.DON_THUOC.Add(model);
                    db.SaveChanges();

                    foreach (var item in DonThuoc)
                    {
                        db.DON_THUOC.Add(new DON_THUOC
                        {
                            ID_DON_THUOC = model.ID_DON_THUOC,
                            ID_THUOC = item.THUOC.ID_THUOC
                        });
                    }
                    db.SaveChanges();

                    scope.Complete();
                    Session["DonThuoc"] = null;
                    TempData["Success"] = "Kê đơn thuốc thành công";
                    return RedirectToAction("DS_DonThuoc");
                }
            }
            
            ViewBag.Cart = DonThuoc;
            
            return View(model);
        }

        private void ValidateDonThuoc(DON_THUOC model)
        {
            if (DonThuoc.Count == 0)
                ModelState.AddModelError("", "Chưa có thuốc trong đơn!");
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
