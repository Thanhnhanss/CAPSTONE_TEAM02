using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.User.Controllers
{
    [HandleError]
    [Authorize]
    public class HealthRecordController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: User/So_Kham_Benh
        public ActionResult HealthRecord(int? id_skb)
        {
            if (id_skb == null)
            {
                return new HttpStatusCodeResult(400);
            }
            var skb = db.SO_KHAM_BENH.FirstOrDefault(s => s.ID_BENH_NHAN == id_skb);

            if (skb == null)
            {
                throw new HttpException(404, "Not Found!");
                //return HttpNotFound();
            }
            ViewBag.Prescriptions = GetAllPrescriptions(id_skb);
            return View(skb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST: User/Dang ky So
        public ActionResult RegisterRecord(int id_skb)
        {
            var skb = db.SO_KHAM_BENH.FirstOrDefault(s => s.ID_BENH_NHAN == id_skb);
            if (skb == null)
            {
                db.SO_KHAM_BENH.Add(new SO_KHAM_BENH
                {
                    ID_BENH_NHAN = id_skb
                });
                db.SaveChanges();
            }
            return RedirectToAction("HealthRecord", new { id_skb });
        }

        [HttpGet]
        public List<DON_THUOC> GetAllPrescriptions(int? id)
        {
            return db.DON_THUOC.OrderByDescending(t => t.NGAY_LAP).Where(t => t.SO_KHAM_BENH.BENH_NHAN.ID_BENH_NHAN == id).ToList();
        }

        [HttpGet]
        public ActionResult Prescriptions(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DON_THUOC dON_THUOC = db.DON_THUOC.Find(id);
            if (dON_THUOC == null)
            {
                throw new HttpException(404, "Not Found!");
                //return HttpNotFound();
            }
            return View(dON_THUOC);
        }
    }
}