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
using HopDongMgr.DungChung;
using X.PagedList;

namespace HopDongMgr.Controllers
{
    public class DM_LoaiHopDongController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();
        private Common _common = new Common();
        private string ChucNang = "Danh mục loại hợp đồng";

        #region Lấy danh sách
        // GET: DM_LoaiHopDong
        [CustomAuthorization]
        public ActionResult Index(int? page = 1)
        {
            db.Configuration.LazyLoadingEnabled = false;
            int pageIndex = (page < 1 ? 1 : page.Value);
            var pageSize = 10;
            int n = (pageIndex - 1) * pageSize;
            int totalData = db.DM_LoaiHopDong.Count();
            List<DM_LoaiHopDong> items = db.DM_LoaiHopDong.OrderBy(p => p.STT).Skip(n).Take(pageSize).ToList();
            ViewBag.OnePageOfData = new StaticPagedList<DM_LoaiHopDong>(items, pageIndex, pageSize, totalData);
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
            List<DM_LoaiHopDong> items;
            int pageIndex = (page < 1 ? 1 : page.Value);
            var pageSize = 10;
            int n = (pageIndex - 1) * pageSize;
            if (string.IsNullOrEmpty(Seach))
            {
                TempData["Search"] = null;
                totalData = db.DM_LoaiHopDong.Count();
                items = db.DM_LoaiHopDong
                        .OrderBy(p => p.STT)
                        .Skip(n)
                        .Take(pageSize)
                        .ToList();
            }
            else
            {
                TempData["Search"] = Seach;
                totalData = db.DM_LoaiHopDong
                            .Where(o => (o.TenLoai.Contains(Seach) || Seach == ""))
                            .Count();
                items = db.DM_LoaiHopDong
                            .Where(o => (o.TenLoai.Contains(Seach) || Seach == ""))
                            .OrderBy(p => p.STT)
                            .Skip(n).Take(pageSize)
                            .ToList();

            }
            ViewBag.OnePageOfData = new StaticPagedList<DM_LoaiHopDong>(items, pageIndex, pageSize, totalData);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_IndexPartial");
            }
            return View("Index");
        }

        #endregion

        #region Create
        // GET: DM_LoaiHopDong/Create
        [CustomAuthorization]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DM_LoaiHopDong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLoai,TenLoai,Khoa,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat")] DM_LoaiHopDong dM_LoaiHopDong)
        {

            db.Configuration.LazyLoadingEnabled = false;
            try
            {
                if (ModelState.IsValid)
                {
                    List<SelectListItem> list = _common.getThongTinBang();
                    dM_LoaiHopDong.NguoiTao = list.Where(o => o.Value == "NguoiTao").SingleOrDefault().Text;
                    dM_LoaiHopDong.NgayTao = DateTime.Parse(list.Where(o => o.Value == "NgayTao").SingleOrDefault().Text);
                    db.DM_LoaiHopDong.Add(dM_LoaiHopDong);
                    db.SaveChanges();
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                        ChucNang
                        , "CREATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $"Thêm mới - Tên loại hợp đồng {dM_LoaiHopDong.TenLoai} ");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(dM_LoaiHopDong);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Không ghi được dữ liệu.<br/>Lý do: " + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }
        #endregion

        #region Update
        // GET: DM_LoaiHopDong/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DM_LoaiHopDong dM_LoaiHopDong = db.DM_LoaiHopDong.Find(id);
            if (dM_LoaiHopDong == null)
            {
                return HttpNotFound();
            }
            return View(dM_LoaiHopDong);
        }

        // POST: DM_LoaiHopDong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDLoai,TenLoai,Khoa,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat")] DM_LoaiHopDong dM_LoaiHopDong)
        {
            db.Configuration.LazyLoadingEnabled = false;
            try
            {
                if (ModelState.IsValid)
                {
                    List<SelectListItem> list = _common.getThongTinBang();
                    dM_LoaiHopDong.NguoiCapNhat = list.Where(o => o.Value == "NguoiCapNhat").SingleOrDefault().Text;
                    dM_LoaiHopDong.NgayCapNhat = DateTime.Parse(list.Where(o => o.Value == "NgayCapNhat").SingleOrDefault().Text);
                    db.Entry(dM_LoaiHopDong).State = EntityState.Modified;
                    db.SaveChanges();
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                        ChucNang
                        , "UPDATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $" Cập nhật - Tên loại hợp đồng {dM_LoaiHopDong.TenLoai} ");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(dM_LoaiHopDong);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Lỗi ghi dữ liệu.<br/>Lý do:" + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }

        #endregion

        #region Delete
        // POST: DM_LoaiHopDong/Delete/5
        [HttpPost]
        [CustomAuthorization]
        public ActionResult Delete(int id)
        {
            DM_LoaiHopDong dM_LoaiHopDong = db.DM_LoaiHopDong.Find(id);
            db.DM_LoaiHopDong.Remove(dM_LoaiHopDong);
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
