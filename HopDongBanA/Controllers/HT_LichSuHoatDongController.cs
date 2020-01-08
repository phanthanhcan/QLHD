using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using HopDongMgr.Class.Common;
using HopDongMgr.DungChung;
using System.Web.Mvc;
using HopDongMgr.Models;
using X.PagedList;

namespace HopDongMgr.Controllers
{
    public class HT_LichSuHoatDongController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();
        #region GetList
        // GET: HT_LichSuHoatDong
        [CustomAuthorization]
        public ActionResult Index(int? page = 1)
        {
            int pageIndex = (page < 1 ? 1 : page.Value);
            var pageSize = 10;
            int n = (pageIndex - 1) * pageSize;
            int totalData = db.HT_LichSuHoatDong.Count();
            List<HT_LichSuHoatDong> items = db.HT_LichSuHoatDong.OrderByDescending(p => p.NgayThucHien).Skip(n).Take(pageSize).ToList();
            ViewBag.OnePageOfData = new StaticPagedList<HT_LichSuHoatDong>(items, pageIndex, pageSize, totalData);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_IndexPartial");
            }
            return View();
        }

        public ActionResult SeachIndex(string Seach, int? page = 1)
        {
            int totalData;
            List<HT_LichSuHoatDong> items;
            int pageIndex = (page < 1 ? 1 : page.Value);
            var pageSize = 10;
            int n = (pageIndex - 1) * pageSize;
            if (string.IsNullOrEmpty(Seach))
            {
                TempData["Search"] = null;
                totalData = db.HT_LichSuHoatDong.Count();
                items = db.HT_LichSuHoatDong.OrderByDescending(p => p.NgayThucHien).Skip(n).Take(pageSize).ToList();

            }
            else
            {
                TempData["Search"] = Seach;
                totalData = db.HT_LichSuHoatDong.Where(o => o.NguoiThucHien.Contains(Seach.Trim())).Count();
                items = db.HT_LichSuHoatDong.Where(o => o.NguoiThucHien.Contains(Seach.Trim())).OrderByDescending(p => p.NgayThucHien).Skip(n).Take(pageSize).ToList();
            }
            ViewBag.OnePageOfData = new StaticPagedList<HT_LichSuHoatDong>(items, pageIndex, pageSize, totalData);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_IndexPartial");
            }
            return View("Index");
        }
        #endregion
        // GET: HT_LichSuHoatDong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HT_LichSuHoatDong hT_LichSuHoatDong = db.HT_LichSuHoatDong.Find(id);
            if (hT_LichSuHoatDong == null)
            {
                return HttpNotFound();
            }
            return View(hT_LichSuHoatDong);
        }

        // GET: HT_LichSuHoatDong/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HT_LichSuHoatDong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLichSuHoatDong,ChucNang,LoaiHoatDong,NgayThucHien,NguoiThucHien,ChiTietHoatDong")] HT_LichSuHoatDong hT_LichSuHoatDong)
        {
            if (ModelState.IsValid)
            {
                db.HT_LichSuHoatDong.Add(hT_LichSuHoatDong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hT_LichSuHoatDong);
        }

        // GET: HT_LichSuHoatDong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HT_LichSuHoatDong hT_LichSuHoatDong = db.HT_LichSuHoatDong.Find(id);
            if (hT_LichSuHoatDong == null)
            {
                return HttpNotFound();
            }
            return View(hT_LichSuHoatDong);
        }

        // POST: HT_LichSuHoatDong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDLichSuHoatDong,ChucNang,LoaiHoatDong,NgayThucHien,NguoiThucHien,ChiTietHoatDong")] HT_LichSuHoatDong hT_LichSuHoatDong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hT_LichSuHoatDong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hT_LichSuHoatDong);
        }

        // GET: HT_LichSuHoatDong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HT_LichSuHoatDong hT_LichSuHoatDong = db.HT_LichSuHoatDong.Find(id);
            if (hT_LichSuHoatDong == null)
            {
                return HttpNotFound();
            }
            return View(hT_LichSuHoatDong);
        }

        // POST: HT_LichSuHoatDong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HT_LichSuHoatDong hT_LichSuHoatDong = db.HT_LichSuHoatDong.Find(id);
            db.HT_LichSuHoatDong.Remove(hT_LichSuHoatDong);
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
    }
}
