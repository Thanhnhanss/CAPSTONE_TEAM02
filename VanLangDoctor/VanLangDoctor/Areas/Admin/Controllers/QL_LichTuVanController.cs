using Google.Apis.Calendar.v3;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using VanLangDoctor.Models;
using Microsoft.AspNet.Identity;
using System.Text;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    public class QL_LichTuVanController : Controller
    {
        //static string[] Scopes = { CalendarService.Scope.Calendar, CalendarService.Scope.CalendarEvents };
        //static string ApplicationName = "Google Calendar API.NET Quickstart";

        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/QL_LichTuVan
        public ActionResult Index()
        {
            var dAT_LICH_TU_VAN = db.DAT_LICH_TU_VAN
                .Include(d => d.AspNetUser)
                .Include(d => d.BACSI);
            return View(dAT_LICH_TU_VAN.ToList().OrderByDescending(e => e.NGAY_KHAM));
        }
        [Authorize(Roles = "Bác sĩ, Quản trị viên")]
        public ActionResult DanhSachDatLich()
        {
            var userId = User.Identity.GetUserId();
            var doctor = db.BACSIs.FirstOrDefault(e => e.ID_Email.Equals(userId)).ID_BACSI;
            var dAT_LICH_TU_VAN = db.DAT_LICH_TU_VAN
                .Where(u => u.BACSI.ID_BACSI == doctor)
                .OrderByDescending(e => e.NGAY_KHAM)
                .ToList();
            return View(dAT_LICH_TU_VAN);
        }

        // GET: Admin/QL_LichTuVan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAT_LICH_TU_VAN dAT_LICH_TU_VAN = db.DAT_LICH_TU_VAN.Find(id);
            if (dAT_LICH_TU_VAN == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_USER = new SelectList(db.AspNetUsers, "Id", "Email", dAT_LICH_TU_VAN.ID_USER);
            ViewBag.ID_BAC_SI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dAT_LICH_TU_VAN.ID_BAC_SI);
            return View(dAT_LICH_TU_VAN);
        }

        // POST: Admin/QL_LichTuVan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DAT_LICH_TU_VAN dAT_LICH_TU_VAN)
        {
            if (ModelState.IsValid)
            {
                if (dAT_LICH_TU_VAN.TRANG_THAI == 1)
                {
                    var httpClient = HttpClientFactory.Create();
                    var uri = new Uri("https://webexapis.com/v1/meetings");
                    var bacSi = await db.BACSIs.FindAsync(dAT_LICH_TU_VAN.ID_BAC_SI);
                    var benhNhan = await db.AspNetUsers.FindAsync(dAT_LICH_TU_VAN.ID_USER);
                    var start = dAT_LICH_TU_VAN.NGAY_KHAM.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz", CultureInfo.InvariantCulture);
                    var end = dAT_LICH_TU_VAN.NGAY_KHAM.AddHours(2).ToString("yyyy-MM-ddTHH\\:mm\\:sszzz", CultureInfo.InvariantCulture);
                    //var body = new Dictionary<string, string>
                    //    {
                    //        { "title", $"Consulting with {bacSi.TEN_BACSI}" },
                    //        // ISO 8601 datetime presentation
                    //        { "start", start },
                    //        { "end", end },
                    //        { "enabledJoinBeforeHost", "true" },
                    //        { "joinBeforeHostMinutes", "15" },
                    //        { "invitees", $"[{{\"email\": \"{benhNhan.Email}\"}}]" }
                    //    };
                    //var content = new FormUrlEncodedContent(body);
                    var content = new StringContent($"{{\"title\": \"Consulting with {bacSi.TEN_BACSI}\"," +
                        $"\"start\": \"{start}\"," +
                        $"\"end\": \"{end}\"," +
                        $"\"enabledJoinBeforeHost\": true," +
                        $"\"joinBeforeHostMinutes\": 15," +
                        $"\"invitees\": [{{\"email\": \"{benhNhan.Email}\"}}]}}", Encoding.UTF8, "application/json");
                    var contentMessage = await content.ReadAsStringAsync();
                    var requestMessage = new HttpRequestMessage
                    {
                        RequestUri = uri,
                        Method = HttpMethod.Post,
                        Content = content
                    };
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Session["ACCESS_TOKEN"]?.ToString());
                    var responseMessage = await httpClient.SendAsync(requestMessage);
                    if (responseMessage.StatusCode == HttpStatusCode.Unauthorized ||
                        responseMessage.StatusCode == HttpStatusCode.Forbidden)
                    {
                        return Redirect($"/CP24Team02/Webex/Auth?redirectUrl=%2FCP24Team02%2FAdmin%2FQL_LichTuVan%2FEdit%2F{dAT_LICH_TU_VAN.ID}");
                        //return Redirect($"/Webex/Auth?redirectUrl=%2FAdmin%2FQL_LichTuVan%2FEdit%2F{dAT_LICH_TU_VAN.ID}");
                    }
                    var responseMessageText = await responseMessage.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<WebexMeetingResposne>(responseMessageText);
                    dAT_LICH_TU_VAN.LINK_GG = response.WebLink;
                    //try
                    //{
                    //    UserCredential credential;
                    //    var path = Server.MapPath("~/Content/") + "tamhtm.json";
                    //    using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    //    {
                    //        /* The file token.json stores the user's access and refresh tokens, and is created
                    //         automatically when the authorization flow completes for the first time. */
                    //        string credPath = Server.MapPath("~/Content/") + "token.json";
                    //        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    //            GoogleClientSecrets.FromStream(stream).Secrets,
                    //            Scopes,
                    //            "user",
                    //            CancellationToken.None,
                    //            new FileDataStore(credPath, true)).Result;
                    //        Console.WriteLine("Credential file saved to: " + credPath);
                    //    }

                    //    //Create Google Calendar API service
                    //    var service = new CalendarService(new BaseClientService.Initializer
                    //    {
                    //        HttpClientInitializer = credential,
                    //        ApplicationName = ApplicationName
                    //    });


                    //    Event body = new Event();
                    //    EventDateTime start = new EventDateTime();
                    //    start.DateTime = dAT_LICH_TU_VAN.NGAY_KHAM;
                    //    EventDateTime end = new EventDateTime();
                    //    end.DateTime = dAT_LICH_TU_VAN.NGAY_KHAM + TimeSpan.FromHours(2);
                    //    body.Start = start;
                    //    body.End = end;
                    //    body.Location = "Online meeting";
                    //    body.Summary = "Tu van kham benh";
                    //    body.ConferenceData = new ConferenceData();
                    //    body.ConferenceData.CreateRequest = new CreateConferenceRequest();
                    //    body.ConferenceData.CreateRequest.RequestId = Guid.NewGuid().ToString();
                    //    body.ConferenceData.CreateRequest.ConferenceSolutionKey = new ConferenceSolutionKey
                    //    {
                    //        Type = "hangoutsMeet"
                    //    };
                    //    EventsResource.InsertRequest request = new EventsResource.InsertRequest(service, body, "hotanminhtam1703@gmail.com");
                    //    request.ConferenceDataVersion = 1;
                    //    Event response = request.Execute();
                    //    dAT_LICH_TU_VAN.LINK_GG = response.HangoutLink;


                    //}
                    //catch (FileNotFoundException e)
                    //{
                    //    Console.WriteLine(e.Message);
                    //}
                }

                db.Entry(dAT_LICH_TU_VAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachDatLich", "QL_LichTuVan", new { area = "Admin" });
            }
            ViewBag.ID_USER = new SelectList(db.AspNetUsers, "Id", "Email", dAT_LICH_TU_VAN.ID_USER);
            ViewBag.ID_BAC_SI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dAT_LICH_TU_VAN.ID_BAC_SI);
            return View(dAT_LICH_TU_VAN);
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

    class WebexMeetingResposne
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string SiteUrl { get; set; }

        public string WebLink { get; set; }
    }
}
