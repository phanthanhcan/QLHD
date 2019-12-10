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
using System.Data.SqlClient;
using X.PagedList;

namespace HopDongMgr.Controllers
{
    public class DM_DiaDiemController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();
        private Common _Common = new Common();
        private string ChucNang = "Danh mục địa điểm";

        #region lấy danh sách
        // GET: DM_DiaDiem
        [CustomAuthorization]
        public ActionResult Index(int? page = 1)
        {
            db.Configuration.LazyLoadingEnabled = false;
            int pageIndex = (page < 1 ? 1 : page.Value);
            var pageSize = 10;
            int n = (pageIndex - 1) * pageSize;
            int totalData = db.DM_DiaDiem.Count();
            List<DM_DiaDiem> items = db.DM_DiaDiem.OrderBy(p => p.TenDD).Skip(n).Take(pageSize).ToList();
            ViewBag.OnePageOfData = new StaticPagedList<DM_DiaDiem>(items, pageIndex, pageSize, totalData);
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
            List<DM_DiaDiem> items;
            int pageIndex = (page < 1 ? 1 : page.Value);
            var pageSize = 10;
            int n = (pageIndex - 1) * pageSize;
            if (string.IsNullOrEmpty(Seach))
            {
                TempData["Search"] = null;
                totalData = db.DM_DiaDiem.Count();
                items = db.DM_DiaDiem
                    .OrderBy(p => p.TenDD)
                    .Skip(n)
                    .Take(pageSize)
                    .ToList();

            }
            else
            {
                TempData["Search"] = Seach;
                totalData = db.DM_DiaDiem
                            .Where(o => (o.TenDD.Contains(Seach) || Seach == "") || (o.MaDD.Contains(Seach) || Seach == ""))
                            .Count();
                items = db.DM_DiaDiem
                    .Where(o => (o.TenDD.Contains(Seach) || Seach == "") || (o.MaDD.Contains(Seach) || Seach == ""))
                    .OrderBy(p => p.TenDD)
                    .Skip(n).Take(pageSize)
                    .ToList();

            }
            ViewBag.OnePageOfData = new StaticPagedList<DM_DiaDiem>(items, pageIndex, pageSize, totalData);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_IndexPartial");
            }
            return View("Index");
        }
        #endregion

        #region Create
        // GET: DM_DiaDiem/Create
        [CustomAuthorization]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DM_DiaDiem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDD,TenDD,Khoa,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat")] DM_DiaDiem dM_DiaDiem)
        {
            db.Configuration.LazyLoadingEnabled = false;
            try
            {
                DM_DiaDiem dd = db.DM_DiaDiem.Find(dM_DiaDiem.MaDD);
                if (dd != null) ModelState.AddModelError("MaDD", $"Mã Địa điểm {dM_DiaDiem.MaDD} đã tồn tại");
                int d = db.DM_DiaDiem.Count(p =>string.Compare(p.TenDD.Trim().Replace("\n", "").Replace("\r", ""), dM_DiaDiem.TenDD.Trim()) == 0);
                if (dd != null) ModelState.AddModelError("TenDD", $"Tên Địa điểm {dM_DiaDiem.TenDD} đã tồn tại");
                if (ModelState.IsValid)
                {
                    List<SelectListItem> list = _Common.getThongTinBang();
                    dM_DiaDiem.NguoiTao = list.Where(o => o.Value == "NguoiTao").SingleOrDefault().Text;
                    dM_DiaDiem.NgayTao = DateTime.Parse(list.Where(o => o.Value == "NgayTao").SingleOrDefault().Text);
                    db.DM_DiaDiem.Add(dM_DiaDiem);
                    db.SaveChanges();
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                        ChucNang
                        , "CREATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $" Thêm mới - {dM_DiaDiem.TenDD} ");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(dM_DiaDiem);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Không ghi được dữ liệu.<br/>Lý do: " + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }
        #endregion

        #region Update
        // GET: DM_DiaDiem/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Configuration.LazyLoadingEnabled = false;
            DM_DiaDiem dM_DiaDiem = db.DM_DiaDiem.Find(id);
            if (dM_DiaDiem == null)
            {
                return HttpNotFound();
            }
            return View(dM_DiaDiem);
        }

        // POST: DM_DiaDiem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDD,TenDD,Khoa,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat")] DM_DiaDiem dM_DiaDiem)
        {
            db.Configuration.LazyLoadingEnabled = false;
            try
            {
                int d = db.DM_DiaDiem.Count(p => p.MaDD != dM_DiaDiem.MaDD && string.Compare(p.TenDD.Trim().Replace("\n", "").Replace("\r", ""), dM_DiaDiem.TenDD.Trim()) == 0);
                if (d > 0) ModelState.AddModelError("TenDD", $"Tên Địa điểm {dM_DiaDiem.TenDD} bị trùng.");
                if (ModelState.IsValid)
                {
                    List<SelectListItem> list = _Common.getThongTinBang();
                    dM_DiaDiem.NguoiCapNhat = list.Where(o => o.Value == "NguoiCapNhat").SingleOrDefault().Text;
                    dM_DiaDiem.NgayCapNhat = DateTime.Parse(list.Where(o => o.Value == "NgayCapNhat").SingleOrDefault().Text);
                    db.Entry(dM_DiaDiem).State = EntityState.Modified;
                    db.SaveChanges();
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                        ChucNang
                        , "UPDATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $" Cập nhật - tên địa điểm {dM_DiaDiem.TenDD} ");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(dM_DiaDiem);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Lỗi ghi dữ liệu.<br/>Lý do:" + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorization]
        public ActionResult Delete(string id)
        {
            DM_DiaDiem dM_DiaDiem = db.DM_DiaDiem.Find(id);
            db.DM_DiaDiem.Remove(dM_DiaDiem);
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

        #region DongBo
        [HttpPost]
        public ActionResult DongBo_DanhMucDiaDiem()
        {
            string err = "";
            try
            {
                //string sqlQuery = "exec GetDTXD_DanhMucDiaDiem @Check OUTPUT";
                string sqlQuery = "EXEC @return_value = GetDTXD_DanhMucDiaDiem";
                SqlParameter return_Pra = new SqlParameter("@return_value", SqlDbType.Int);
                return_Pra.Direction = ParameterDirection.Output;
                var i = db.Database.ExecuteSqlCommand(sqlQuery, return_Pra);
                int return_value = (int)return_Pra.Value;
                err = return_value.ToString(); // 1 thành công, 0 thất bại
            }
            catch(Exception ex)
            {
                err = "0"; // thất bại
            }
                
            return Json(err);
        }
        #endregion
    }
}
