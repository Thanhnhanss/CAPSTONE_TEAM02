using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    public class SendMailController : Controller
    {
        // GET: Admin/SendMail
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(VanLangDoctor.Models.sendMail model, HttpPostedFileBase file)
        {
            MailMessage mm = new MailMessage("hungdp2612@gmail.com", model.to);
            mm.Subject = model.subject;
            mm.Body = model.body;
            mm.IsBodyHtml = false;

            if(file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                mm.Attachments.Add(new Attachment(file.InputStream, fileName));
            }

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("hungdp2612@gmail.com", "hynh26122000");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = nc;
            smtp.Send(mm);

            ViewBag.Message = "Đã gửi mail thành công";
            return RedirectToAction("Index");
        }
    }
}