﻿using System;
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
using System.Globalization;
using System.Data.SqlClient;

namespace HopDongMgr.Controllers
{
    public class DM_NguonVonController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();
        private Common _Common = new Common();
        private string ChucNang = "Danh mục nguồn vốn";
        // GET: DM_NguonVon
        [CustomAuthorization]
        public ActionResult Index()
        {
            return View(db.DM_NguonVon.OrderBy(a => a.MaNV).ToList());
        }

        // GET: DM_NguonVon/Details/5
        [CustomAuthorization]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DM_NguonVon dM_NguonVon = db.DM_NguonVon.Find(id);
            if (dM_NguonVon == null)
            {
                return HttpNotFound();
            }
            return View(dM_NguonVon);
        }

        #region Create
        // GET: DM_NguonVon/Create
        [CustomAuthorization]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DM_NguonVon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,TenNV,Khoa,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat")] DM_NguonVon dM_NguonVon)
        {
            db.Configuration.LazyLoadingEnabled = false;
            try
            {
                if (ModelState.IsValid)
                {
                    List<SelectListItem> ListTTin = _Common.getThongTinBang();
                    dM_NguonVon.NguoiTao = ListTTin.Where(o => o.Value == "NguoiTao").SingleOrDefault().Text;
                    dM_NguonVon.NgayTao = DateTime.Parse(ListTTin.Where(o => o.Value == "NgayTao").SingleOrDefault().Text);
                    dM_NguonVon.NguoiCapNhat = ListTTin.Where(o => o.Value == "NguoiCapNhat").SingleOrDefault().Text;
                    dM_NguonVon.NgayCapNhat = DateTime.Parse(ListTTin.Where(o => o.Value == "NgayCapNhat").SingleOrDefault().Text);
                    db.DM_NguonVon.Add(dM_NguonVon);
                    db.SaveChanges();
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                        ChucNang
                        , "CREATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $"Thêm mới - Tên nguồn vốn {dM_NguonVon.TenNV} ");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(dM_NguonVon);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Không ghi được dữ liệu.<br/>Lý do: " + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }
        #endregion

        #region Update
        // GET: DM_NguonVon/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Configuration.LazyLoadingEnabled = false;
            DM_NguonVon dM_NguonVon = db.DM_NguonVon.Find(id);
            if (dM_NguonVon == null)
            {
                return HttpNotFound();
            }
            return View(dM_NguonVon);
        }

        // POST: DM_NguonVon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,TenNV,Khoa,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat")] DM_NguonVon dM_NguonVon)
        {
            db.Configuration.LazyLoadingEnabled = false;
            try
            {
                if (ModelState.IsValid)
                {
                    List<SelectListItem> ListTTin = _Common.getThongTinBang();
                    dM_NguonVon.NguoiCapNhat = ListTTin.Where(o => o.Value == "NguoiCapNhat").SingleOrDefault().Text;
                    dM_NguonVon.NgayCapNhat = DateTime.Parse(ListTTin.Where(o => o.Value == "NgayCapNhat").SingleOrDefault().Text);
                    db.Entry(dM_NguonVon).State = EntityState.Modified;
                    db.SaveChanges();
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                        ChucNang
                        , "UPDATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $" Cập nhật - Tên nguồn vốn {dM_NguonVon.TenNV} ");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(dM_NguonVon);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Lỗi ghi dữ liệu.<br/>Lý do:" + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }

        #endregion
        [CustomAuthorization]
        [HttpPost]
        public ActionResult Delete(string id)
        {
            DM_NguonVon dM_NguonVon = db.DM_NguonVon.Find(id);
            db.DM_NguonVon.Remove(dM_NguonVon);
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
        [HttpPost]
        public ActionResult DongBo_DanhMucNguonVon()
        {
            string err = "";
            try
            {
                //string sqlQuery = "exec GetDTXD_DanhMucDiaDiem @Check OUTPUT";
                string sqlQuery = "EXEC @return_value = GetDTXD_DanhMucNguonVon";
                SqlParameter return_Pra = new SqlParameter("@return_value", SqlDbType.Int);
                return_Pra.Direction = ParameterDirection.Output;
                var i = db.Database.ExecuteSqlCommand(sqlQuery, return_Pra);
                int return_value = (int)return_Pra.Value;
                err = return_value.ToString(); // 1 thành công, 0 thất bại
            }
            catch (Exception ex)
            {
                err = "0"; // thất bại
            }

            return Json(err);
        }
    }
}