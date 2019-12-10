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

namespace HopDongMgr.Controllers
{
    public class DM_DonViThucHienController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();
        private Common _common = new Common();
        private string ChucNang = "Danh mục đơn vị thực hiện";

        // GET: DM_DonViThucHien
        [CustomAuthorization]
        public ActionResult Index()
        {
            return View(db.DM_DonViThucHien.ToList());
        }

        // GET: DM_DonViThucHien/Details/5
        [CustomAuthorization]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DM_DonViThucHien dM_DonViThucHien = db.DM_DonViThucHien.Find(id);
            if (dM_DonViThucHien == null)
            {
                return HttpNotFound();
            }
            return View(dM_DonViThucHien);
        }

        #region Create 
        // GET: DM_DonViThucHien/Create
        [CustomAuthorization]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DM_DonViThucHien/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDDV,TenDV,DiaChi,Khoa,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat")] DM_DonViThucHien dM_DonViThucHien)
        {
            db.Configuration.LazyLoadingEnabled = false;
            try
            {
                if (ModelState.IsValid)
                {
                    List<SelectListItem> ListTTin = _common.getThongTinBang();
                    dM_DonViThucHien.NguoiTao = ListTTin.Where(o => o.Value == "NguoiTao").SingleOrDefault().Text;
                    dM_DonViThucHien.NgayTao = DateTime.Parse(ListTTin.Where(o => o.Value == "NgayTao").SingleOrDefault().Text);
                    db.DM_DonViThucHien.Add(dM_DonViThucHien);
                    db.SaveChanges();
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                        ChucNang
                        , "CREATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $" Thêm mới - Tên đơn vị {dM_DonViThucHien.TenDV} ");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(dM_DonViThucHien);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Không ghi được dữ liệu.<br/>Lý do: " + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }
        #endregion

        #region Update
        // GET: DM_DonViThucHien/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Configuration.LazyLoadingEnabled = false;
            DM_DonViThucHien dM_DonViThucHien = db.DM_DonViThucHien.Find(id);
            if (dM_DonViThucHien == null)
            {
                return HttpNotFound();
            }
            return View(dM_DonViThucHien);
        }

        // POST: DM_DonViThucHien/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDDV,TenDV,DiaChi,Khoa,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat")] DM_DonViThucHien dM_DonViThucHien)
        {
            db.Configuration.LazyLoadingEnabled = false;
            try
            {
                if (ModelState.IsValid)
                {
                    List<SelectListItem> ListTTin = _common.getThongTinBang();
                    dM_DonViThucHien.NguoiCapNhat = ListTTin.Where(o => o.Value == "NguoiCapNhat").SingleOrDefault().Text;
                    dM_DonViThucHien.NgayCapNhat = DateTime.Parse(ListTTin.Where(o => o.Value == "NgayCapNhat").SingleOrDefault().Text);
                    db.Entry(dM_DonViThucHien).State = EntityState.Modified;
                    db.SaveChanges();
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                        ChucNang
                        , "UPDATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $" Cập nhật - Tên đơn vị {dM_DonViThucHien.TenDV} ");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(dM_DonViThucHien);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Lỗi ghi dữ liệu.<br/>Lý do:" + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }

        #endregion

        // POST: DM_DonViThucHien/Delete/5
        [HttpPost]
        [CustomAuthorization]
        public ActionResult Delete(int id)
        {
            DM_DonViThucHien dM_DonViThucHien = db.DM_DonViThucHien.Find(id);
            db.DM_DonViThucHien.Remove(dM_DonViThucHien);
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
        public ActionResult DongBo_DanhMucDonViThucHien()
        {
            string err = "";
            try
            {
                //string sqlQuery = "exec GetDTXD_DanhMucDiaDiem @Check OUTPUT";
                string sqlQuery = "EXEC @return_value = GetDTXD_DanhMucDonViThucHien";
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
