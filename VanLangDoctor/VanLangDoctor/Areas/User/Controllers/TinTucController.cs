﻿using PagedList;
using System;
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
    [HandleError]
    public class TinTucController : Controller
    {
        private CP24Team02Entities db = new CP24Team02Entities();

        // GET: User/TinTuc
        public ActionResult Tin_Tuc(int id_cate = 1)
        {
            ViewBag.Top1 = GetTop1View();
            ViewBag.Top3 = GetTop3Views();
            ViewBag.RecentArticles = GetRecentArticles();
            var danhmuctin = db.TIN_TUC.Where(tk => tk.ID_Danhmuc_tin == id_cate).ToList();
            ViewBag.danhmuc = GetDanhMucTin();
            ViewBag.Top = GetTop3Views();
            return View(danhmuctin.OrderByDescending(t => t.NGAY_DANG).ToList());
        }

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page, int id_cate = 1)
        {
            ViewBag.Top1 = GetTop1View();
            ViewBag.Top3 = GetTop3Views();
            ViewBag.RecentArticles = GetRecentArticles();
            var danhmuctin = db.TIN_TUC.Where(tk => tk.ID_Danhmuc_tin == id_cate).ToList();
            ViewBag.danhmuc = GetDanhMucTin();
            ViewBag.Top = GetTop3Views();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = from s in db.TIN_TUC
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.TEN_BAI_VIET.Contains(searchString)
                                       || s.TAC_GIA.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.TEN_BAI_VIET);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.NGAY_DANG);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.NGAY_DANG);
                    break;
                default:  // Name ascending 
                    students = students.OrderBy(s => s.TEN_BAI_VIET);
                    break;
            }

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        private const string PICTURE_PATH = "~/Content/IMG_NEWS/";

        public ActionResult Picture(int ID_Tin_Tuc)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + ID_Tin_Tuc, "images");
        }

        // GET: User/TinTuc/Details/5
        public ActionResult Details(int? ID_TIN_TUC)
        {
            if (ID_TIN_TUC == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_TUC tIN_TUC = db.TIN_TUC.Find(ID_TIN_TUC);
            if (tIN_TUC == null)
            {
                throw new HttpException(404,"Not Found!");
                //return HttpNotFound();
            }
            tIN_TUC.CountViews += 1;
            db.SaveChanges();
            ViewBag.Top = GetTop3Views();
            ViewBag.danhmuc = GetDanhMucTin();
            return View(tIN_TUC);
        }

        //GET: TOP 1 View
        public TIN_TUC GetTop1View()
        {
            return db.TIN_TUC.OrderByDescending(item => item.CountViews).First();
        }


        //GET: Top Views
        private List<TIN_TUC> GetTop3Views()
        {
            return db.TIN_TUC.OrderByDescending(t => t.CountViews).Skip(1).Take(3).ToList();
        }

        //GET: Recent Articles
        public List<TIN_TUC> GetRecentArticles()
        {
            return db.TIN_TUC.OrderByDescending(t => t.NGAY_DANG).Take(5).ToList();
        }
        private List<DANH_MUC_TIN> GetDanhMucTin()
        {
            return db.DANH_MUC_TIN.OrderByDescending(t => t.Danhmuc_tin).ToList();
        }
    }
}