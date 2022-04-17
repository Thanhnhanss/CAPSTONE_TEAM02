using System.Linq;
using System.Web.Mvc;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.User.Controllers
{
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
                return HttpNotFound();
            }
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
    }
}