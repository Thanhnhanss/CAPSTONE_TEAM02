using System;
using System.Linq;
using System.Web.Mvc;
using VanLangDoctor.Models;
using System.Data.Entity;
using System.Net;
using System.Collections.Generic;
using System.Data;
using System.Web;


namespace VanLangDoctor.Areas.Admin.Controllers
{
    public class PhanQuyenController : Controller
    {
        CP24Team02Entities db = new CP24Team02Entities();
        // GET: QuanLy/PhanQuyen
        public ActionResult PhanQuyen(string roleId)
        {
            ViewBag.Role = db.AspNetRoles.Find(roleId);
            ViewBag.Users = new SelectList(db.AspNetUsers, "Id", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PhanQuyen(string roleId, string userId)
        {
            var user = db.AspNetUsers.Find(userId);
            var role = db.AspNetRoles.Find(roleId);

            role.AspNetUsers.Add(user);
            db.Entry(role).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("phanQuyenTK", "PhanQuyen");
        }

        public ActionResult phanQuyenTK()
        {
            return View(db.AspNetRoles.ToList());
        }

        // GET: Admin/AspNetRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AspNetRole aspNetRole)
        {
            if (ModelState.IsValid)
            {
                aspNetRole.Id = Guid.NewGuid().ToString();
                db.AspNetRoles.Add(aspNetRole);
                db.SaveChanges();
                return RedirectToAction("phanQuyenTK");
            }

            return View(aspNetRole);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetRole aspNetRole = db.AspNetRoles.Find(id);
            if (aspNetRole == null)
            {
                return HttpNotFound();
            }
            return View(aspNetRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AspNetRole aspNetRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("phanQuyenTK");
            }
            return View(aspNetRole);
        }

        public ActionResult Delete(string roleId, string userId)
        {
            var role = db.AspNetRoles.Find(roleId);
            var user = db.AspNetUsers.Find(userId);

            role.AspNetUsers.Remove(user);
            db.Entry(role).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("phanQuyenTK", "PhanQuyen");
        }

        public ActionResult XoaQuyen(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetRole aspNetRole = db.AspNetRoles.Find(id);
            if (aspNetRole == null)
            {
                return HttpNotFound();
            }
            return View(aspNetRole);
        }

        // POST: Admin/AspNetRoles/Delete/5
        [HttpPost, ActionName("XoaQuyen")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetRole aspNetRole = db.AspNetRoles.Find(id);
            db.AspNetRoles.Remove(aspNetRole);
            db.SaveChanges();
            return RedirectToAction("phanQuyenTK");
        }
    }
}