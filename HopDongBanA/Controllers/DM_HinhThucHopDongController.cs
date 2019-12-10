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
using System.Data.SqlClient;

namespace HopDongMgr.Controllers
{
    public class DM_HinhThucHopDongController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();
        private Common _common = new Common();
        private string ChucNang = "Danh mục hình thức hợp dồng";

        // GET: DM_HinhThucHopDong
        [CustomAuthorization]
        public ActionResult Index()
        {
            return View(db.DM_HinhThucHopDong.ToList());
        }

        // GET: DM_HinhThucHopDong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DM_HinhThucHopDong dM_HinhThucHopDong = db.DM_HinhThucHopDong.Find(id);
            if (dM_HinhThucHopDong == null)
            {
                return HttpNotFound();
            }
            return View(dM_HinhThucHopDong);
        }
        #region Create
        // GET: DM_HinhThucHopDong/Create
        [CustomAuthorization]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DM_HinhThucHopDong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDHT,TenHinhThuc,Khoa,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat")] DM_HinhThucHopDong dM_HinhThucHopDong)
        {
            db.Configuration.LazyLoadingEnabled = false;
            try
            {
                if (ModelState.IsValid)
                {
                    List<SelectListItem> list = _common.getThongTinBang();
                    dM_HinhThucHopDong.NguoiTao = list.Where(o => o.Value == "NguoiTao").SingleOrDefault().Text;
                    dM_HinhThucHopDong.NgayTao = DateTime.Parse(list.Where(o => o.Value == "NgayTao").SingleOrDefault().Text);
                    db.DM_HinhThucHopDong.Add(dM_HinhThucHopDong);
                    db.SaveChanges();
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                        ChucNang
                        , "CREATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $" Thêm mới - Tên hình thức {dM_HinhThucHopDong.TenHinhThuc} ");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(dM_HinhThucHopDong);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Không ghi được dữ liệu.<br/>Lý do: " + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }
        #endregion

        #region Update
        // GET: DM_HinhThucHopDong/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Configuration.LazyLoadingEnabled = false;
            DM_HinhThucHopDong dM_HinhThucHopDong = db.DM_HinhThucHopDong.Find(id);
            if (dM_HinhThucHopDong == null)
            {
                return HttpNotFound();
            }
            return View(dM_HinhThucHopDong);
        }

        // POST: DM_HinhThucHopDong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDHT,TenHinhThuc,Khoa,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat")] DM_HinhThucHopDong dM_HinhThucHopDong)
        {
            db.Configuration.LazyLoadingEnabled = false;
            try
            {
                //int d = db.DM_DongTien.Count(p => p.MaDongTien != dM_DongTien.MaDongTien && string.Compare(p.TenDongTien.Trim().Replace("\n", "").Replace("\r", ""), dM_DongTien.TenDongTien.Trim()) == 0);
                //if (d > 0) ModelState.AddModelError("TenDongTien", "Tên dồng tiền bị trùng.");
                if (ModelState.IsValid)
                {
                    List<SelectListItem> list = _common.getThongTinBang();
                    dM_HinhThucHopDong.NguoiCapNhat = list.Where(o => o.Value == "NguoiCapNhat").SingleOrDefault().Text;
                    dM_HinhThucHopDong.NgayCapNhat = DateTime.Parse(list.Where(o => o.Value == "NgayCapNhat").SingleOrDefault().Text);
                    db.Entry(dM_HinhThucHopDong).State = EntityState.Modified;
                    db.SaveChanges();
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                        ChucNang
                        , "UPDATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $" Cập nhật - Tên hình thức{dM_HinhThucHopDong.TenHinhThuc} ");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(dM_HinhThucHopDong);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Lỗi ghi dữ liệu.<br/>Lý do:" + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }

        #endregion
        // POST: DM_HinhThucHopDong/Delete/5
        [HttpPost]
        [CustomAuthorization]
        public ActionResult Delete(int id)
        {
            DM_HinhThucHopDong dM_HinhThucHopDong = db.DM_HinhThucHopDong.Find(id);
            db.DM_HinhThucHopDong.Remove(dM_HinhThucHopDong);
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
        public ActionResult DongBo_DanhMucHinhThucHopDong()
        {
            string err = "";
            try
            {
                //string sqlQuery = "exec GetDTXD_DanhMucDiaDiem @Check OUTPUT";
                string sqlQuery = "EXEC @return_value = GetDTXD_DanhMucHinhThucHopDong";
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
