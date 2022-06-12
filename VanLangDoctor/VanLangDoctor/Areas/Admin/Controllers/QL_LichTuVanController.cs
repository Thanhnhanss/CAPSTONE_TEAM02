﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VanLangDoctor.Models;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    public class QL_LichTuVanController : Controller
    {
        static string[] Scopes = { CalendarService.Scope.Calendar, CalendarService.Scope.CalendarEvents };
        static string ApplicationName = "Google Calendar API.NET Quickstart";

        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: Admin/QL_LichTuVan
        public ActionResult Index()
        {
            var dAT_LICH_TU_VAN = db.DAT_LICH_TU_VAN.Include(d => d.AspNetUser).Include(d => d.BACSI);
            return View(dAT_LICH_TU_VAN.ToList());
        }

        // GET: Admin/QL_LichTuVan/Details/5
        public ActionResult Details(int? id)
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
        public ActionResult Edit(DAT_LICH_TU_VAN dAT_LICH_TU_VAN)
        {
            if (ModelState.IsValid)
            {
                if (dAT_LICH_TU_VAN.TRANG_THAI == 1)
                {
                    try
                    {
                        UserCredential credential;
                        var path = Server.MapPath("~/Content/") + "tamhtm.json";
                        using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                        {
                            /* The file token.json stores the user's access and refresh tokens, and is created
                             automatically when the authorization flow completes for the first time. */
                            string credPath = Server.MapPath("~/Content/") + "token.json";
                            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                                GoogleClientSecrets.FromStream(stream).Secrets,
                                Scopes,
                                "user",
                                CancellationToken.None,
                                new FileDataStore(credPath, true)).Result;
                            Console.WriteLine("Credential file saved to: " + credPath);
                        }

                        //Create Google Calendar API service
                        var service = new CalendarService(new BaseClientService.Initializer
                        {
                            HttpClientInitializer = credential,
                            ApplicationName = ApplicationName
                        });


                        Event body = new Event();
                        EventDateTime start = new EventDateTime();
                        start.DateTime = dAT_LICH_TU_VAN.NGAY_KHAM;
                        EventDateTime end = new EventDateTime();
                        end.DateTime = dAT_LICH_TU_VAN.NGAY_KHAM + TimeSpan.FromHours(2);
                        body.Start = start;
                        body.End = end;
                        body.Location = "Online meeting";
                        body.Summary = "Tu van kham benh";
                        body.ConferenceData = new ConferenceData();
                        body.ConferenceData.CreateRequest = new CreateConferenceRequest();
                        body.ConferenceData.CreateRequest.RequestId = Guid.NewGuid().ToString();
                        body.ConferenceData.CreateRequest.ConferenceSolutionKey = new ConferenceSolutionKey
                        {
                            Type = "hangoutsMeet"
                        };
                        EventsResource.InsertRequest request = new EventsResource.InsertRequest(service, body, "hotanminhtam1703@gmail.com");
                        request.ConferenceDataVersion = 1;
                        Event response = request.Execute();
                        dAT_LICH_TU_VAN.LINK_GG = response.HangoutLink;
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                db.Entry(dAT_LICH_TU_VAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_USER = new SelectList(db.AspNetUsers, "Id", "Email", dAT_LICH_TU_VAN.ID_USER);
            ViewBag.ID_BAC_SI = new SelectList(db.BACSIs, "ID_BACSI", "TEN_BACSI", dAT_LICH_TU_VAN.ID_BAC_SI);
            return View(dAT_LICH_TU_VAN);
        }

        // GET: Admin/QL_LichTuVan/Delete/5
        public ActionResult Delete(int? id)
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
            return View(dAT_LICH_TU_VAN);
        }

        // POST: Admin/QL_LichTuVan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DAT_LICH_TU_VAN dAT_LICH_TU_VAN = db.DAT_LICH_TU_VAN.Find(id);
            db.DAT_LICH_TU_VAN.Remove(dAT_LICH_TU_VAN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private static void CreateEvent(CalendarService _service)
        {
            Event body = new Event();
            EventDateTime start = new EventDateTime();
            start.DateTime = new DateTime(2022, 6, 11, 15, 0, 0);
            EventDateTime end = new EventDateTime();
            end.DateTime = new DateTime(2022, 6, 11, 15, 30, 0);
            body.Start = start;
            body.End = end;
            body.Location = "Test Meeting";
            body.Summary = "ABC";
            body.ConferenceData = new ConferenceData();
            body.ConferenceData.CreateRequest = new CreateConferenceRequest();
            body.ConferenceData.CreateRequest.RequestId = Guid.NewGuid().ToString();
            body.ConferenceData.CreateRequest.ConferenceSolutionKey = new ConferenceSolutionKey
            {
                Type = "hangoutsMeet"
            };
            EventsResource.InsertRequest request = new EventsResource.InsertRequest(_service, body, "hotanminhtam1703@gmail.com");
            request.ConferenceDataVersion = 1;
            Event response = request.Execute();
            Console.WriteLine(response.HangoutLink);
        }
    }
}