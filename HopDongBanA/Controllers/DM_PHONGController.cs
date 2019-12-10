using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HopDongMgr.Models;
using HopDongMgr.Class.Common;
using X.PagedList;

namespace HopDongMgr.Controllers
{
    public class DM_PHONGController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();
        private string ChucNang = "Danh mục phòng";

        #region Lấy danh sách
        // GET: DM_PHONG
        [CustomAuthorization]
        public ActionResult Index(int? page = 1)
        {
            db.Configuration.LazyLoadingEnabled = false;
            int pageIndex = (page < 1 ? 1 : page.Value);
            var pageSize = 10;
            int n = (pageIndex - 1) * pageSize;
            int totalData = db.DM_PHONG.Count();
            List<DM_PHONG> items = db.DM_PHONG.OrderBy(p => p.Ten).Skip(n).Take(pageSize).ToList();
            ViewBag.OnePageOfData = new StaticPagedList<DM_PHONG>(items, pageIndex, pageSize, totalData);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_IndexPartial");
            }
            return View();
        }

        // GET: DM_CongTrinh
        public ActionResult SeachIndex(string Seach = "", int? page = 1)
        {
            db.Configuration.LazyLoadingEnabled = false;
            int totalData;
            List<DM_PHONG> items;
            int pageIndex = (page < 1 ? 1 : page.Value);
            var pageSize = 10;
            int n = (pageIndex - 1) * pageSize;
            if (string.IsNullOrEmpty(Seach))
            {
                TempData["Search"] = null;
                totalData = db.DM_PHONG.Count();
                items = db.DM_PHONG
                    .OrderBy(p => p.Ten)
                    .Skip(n)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                TempData["Search"] = Seach;
                totalData = db.DM_PHONG
                            .Where(o => o.Ten.Contains(Seach) || Seach == "")
                            .Count();
                items = db.DM_PHONG
                            .Where(o => o.Ten.Contains(Seach) || Seach == "")
                            .OrderBy(p => p.Ten)
                            .Skip(n).Take(pageSize)
                            .ToList();

            }
            ViewBag.OnePageOfData = new StaticPagedList<DM_PHONG>(items, pageIndex, pageSize, totalData);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_IndexPartial");
            }
            return View("Index");
        }
        #endregion

        #region Create
        // GET: DM_PHONG/Create
        [CustomAuthorization]
        public ActionResult Create()
        {
            db.Configuration.LazyLoadingEnabled = false;
            ViewBag.MaDV = new SelectList(db.DM_DONVI, "MA_DVIQLY", "TEN_DVIQLY");
            return View();
        }

        // POST: DM_PHONG/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ten,MaDV,TenTat")] DM_PHONG dM_PHONG)
        {

            db.Configuration.LazyLoadingEnabled = false;
            try
            {
                if (ModelState.IsValid)
                {
                    dM_PHONG.Id = Guid.NewGuid();
                    db.DM_PHONG.Add(dM_PHONG);
                    db.SaveChanges();
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                        ChucNang
                        , "CREATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $"Thêm mới - Tên phòng{dM_PHONG.Ten} ");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.MaDV = new SelectList(db.DM_DONVI, "MA_DVIQLY", "TEN_DVIQLY", dM_PHONG.MaDV);
                return View(dM_PHONG);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Không ghi được dữ liệu.<br/>Lý do: " + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }

        #endregion 

        #region Edit
        // GET: DM_PHONG/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Configuration.LazyLoadingEnabled = false;
            DM_PHONG dM_PHONG = db.DM_PHONG.Find(id);
            if (dM_PHONG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDV = new SelectList(db.DM_DONVI, "MA_DVIQLY", "TEN_DVIQLY", dM_PHONG.MaDV);
            return View(dM_PHONG);

        }

        // POST: DM_PHONG/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ten,MaDV,TenTat")] DM_PHONG dM_PHONG)
        {
            db.Configuration.LazyLoadingEnabled = false;
            try
            {
                //int d = db.DM_DongTien.Count(p => p.MaDongTien != dM_DongTien.MaDongTien && string.Compare(p.TenDongTien.Trim().Replace("\n", "").Replace("\r", ""), dM_DongTien.TenDongTien.Trim()) == 0);
                //if (d > 0) ModelState.AddModelError("TenDongTien", "Tên dồng tiền bị trùng.");
                if (ModelState.IsValid)
                {
                    db.Entry(dM_PHONG).State = EntityState.Modified;
                    db.SaveChanges();
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                        ChucNang
                        ,"UPDATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $"Cập nhật - Tên phòng {dM_PHONG.Ten} ");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.MaDV = new SelectList(db.DM_DONVI, "MA_DVIQLY", "TEN_DVIQLY", dM_PHONG.MaDV);
                return View(dM_PHONG);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Lỗi ghi dữ liệu.<br/>Lý do:" + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }

        }

        #endregion

        #region Delete
        // POST: DM_PHONG/Delete/5
        [CustomAuthorization]
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            DM_PHONG dM_PHONG = db.DM_PHONG.Find(id);
            db.DM_PHONG.Remove(dM_PHONG);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
