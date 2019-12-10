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

namespace HopDongMgr.Controllers
{
    public class CN_ThanhLyController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();
        private Common _common = new Common();
        private string ChucNang = "Chức năng thanh lý hợp đồng";
        #region Lấy danh sách
        // GET: CN_ThanhLy
        [CustomAuthorization]
        public ActionResult Index()
        {
            var cN_ThanhLy = db.CN_ThanhLy.Include(c => c.CN_HopDong);
            return View(cN_ThanhLy.ToList());
        }

        #endregion

        #region Create
        //[CustomAuthorization]
        //public ActionResult Create()
        //{

        //        ViewBag.IDHD = new SelectList(db.CN_HopDong, "IDHD", "SoHopDong");       

        //    return View();
        //}

        // GET: CN_ThanhLy/Create
        [CustomAuthorization]
        public ActionResult Create(int? idhd)
        {
            int gIDHD;
            bool check = int.TryParse(idhd.ToString(), out gIDHD);
            if (!check)
            {
                ViewBag.IDHD = DanhSachHopDong();
            }
            else
            {
                ViewBag.IDHD = DanhSachHopDong(idhd.Value);
            }
            return View();
        }

        // POST: CN_ThanhLy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTL, IDHD, IsHoanThanh, NgayThanhLy, GiaTriThanhLy, GiaTriQuyetToan, NgayTongNghiemThu, NguoiTao, NgayTao, NguoiCapNhat, NgayCapNhat")] CN_ThanhLy cN_ThanhLy)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                CN_ThanhLy newN_ThanhLy = db.CN_ThanhLy.Where(o => o.IDHD == cN_ThanhLy.IDHD).SingleOrDefault();
                CN_HopDong hopDong = db.CN_HopDong.Find(cN_ThanhLy.IDHD);
                if (cN_ThanhLy.NgayThanhLy < hopDong.NgayHetHan) ModelState.AddModelError("NgayThanhLy", $"Ngày thanh lý hợp đông nhỏ hơn Ngày hết hạn {hopDong.NgayHetHan}");

                if (ModelState.IsValid)
                {
                    List<SelectListItem> list = _common.getThongTinBang();
                    //CN_HopDong hopDong = db.CN_HopDong.Find(IDHD);
                    if (newN_ThanhLy == null)
                    {
                        cN_ThanhLy.NguoiTao = list.Where(o => o.Value == "NguoiTao").SingleOrDefault().Text;
                        cN_ThanhLy.NgayTao = DateTime.Parse(list.Where(o => o.Value == "NgayTao").SingleOrDefault().Text);
                        cN_ThanhLy.NguoiCapNhat = list.Where(o => o.Value == "NguoiCapNhat").SingleOrDefault().Text;
                        cN_ThanhLy.NgayCapNhat = DateTime.Parse(list.Where(o => o.Value == "NgayCapNhat").SingleOrDefault().Text);
                        db.CN_ThanhLy.Add(cN_ThanhLy);
                        db.SaveChanges();
                        #region luu lich su
                        // luu lich su
                        HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                            ChucNang
                            , "CREATE"
                            , DateTime.Now, Session["username"]?.ToString()
                            , $"Thêm mới - Thanh lý - số hợp đồng {hopDong?.SoHopDong} ");
                        db.HT_LichSuHoatDong.Add(ls);
                        db.SaveChanges();
                        #endregion
                    }
                    else
                    {
                        newN_ThanhLy.IDHD = cN_ThanhLy.IDHD;
                        newN_ThanhLy.IsHoanThanh = cN_ThanhLy.IsHoanThanh;
                        newN_ThanhLy.NgayThanhLy = cN_ThanhLy.NgayThanhLy;
                        newN_ThanhLy.GiaTriThanhLy = cN_ThanhLy.GiaTriThanhLy;
                        newN_ThanhLy.GiaTriQuyetToan = cN_ThanhLy.GiaTriQuyetToan;
                        newN_ThanhLy.NgayTongNghiemThu = cN_ThanhLy.NgayTongNghiemThu;
                        newN_ThanhLy.NguoiCapNhat = list.Where(o => o.Value == "NguoiCapNhat").SingleOrDefault().Text;
                        newN_ThanhLy.NgayCapNhat = DateTime.Parse(list.Where(o => o.Value == "NgayCapNhat").SingleOrDefault().Text);
                        db.Entry(newN_ThanhLy).State = EntityState.Modified;
                        db.SaveChanges();
                        #region luu lich su
                        // luu lich su
                        HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                        ChucNang
                        , "UPDATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $"Cập nhật - Thanh lý - số hợp đồng { hopDong?.SoHopDong }");
                        db.HT_LichSuHoatDong.Add(ls);
                        db.SaveChanges();
                        #endregion
                    }
                    //TempData["err"] = "<div class='alert alert-success' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Cập nhật thành công</div> ";
                    return RedirectToAction("Index");
                }
                TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Có lỗi xảy ra</div> ";
                if (newN_ThanhLy == null)
                {
                    ViewBag.IDHD = DanhSachHopDong();
                }
                else
                {
                    ViewBag.IDHD = DanhSachHopDong(cN_ThanhLy.IDHD.Value);
                }
                return View(cN_ThanhLy);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Không ghi được dữ liệu.<br/>Lý do: " + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }

        #endregion

        #region Edit

        // GET: CN_ThanhLy/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CN_ThanhLy cN_ThanhLy = db.CN_ThanhLy.Find(id);
            if (cN_ThanhLy == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDHD = DanhSachHopDong(cN_ThanhLy.IDHD.Value);
            return View(cN_ThanhLy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTL, IDHD, IsHoanThanh, NgayThanhLy, GiaTriThanhLy, GiaTriQuyetToan, NgayTongNghiemThu, NguoiTao, NgayTao, NguoiCapNhat, NgayCapNhat")] CN_ThanhLy cN_ThanhLy, FormCollection fCollection)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                CN_HopDong hopDong = db.CN_HopDong.Find(cN_ThanhLy.IDHD);
                if (cN_ThanhLy.NgayThanhLy < hopDong.NgayHetHan) ModelState.AddModelError("NgayThanhLy", $"Ngày thanh lý hợp đổng nhỏ hơn Ngày hết hạn {hopDong.NgayHetHan}");
                if (ModelState.IsValid)
                {
                    List<SelectListItem> list = _common.getThongTinBang();
                    cN_ThanhLy.NguoiCapNhat = list.Where(o => o.Value == "NguoiCapNhat").SingleOrDefault().Text;
                    cN_ThanhLy.NgayCapNhat = DateTime.Parse(list.Where(o => o.Value == "NgayCapNhat").SingleOrDefault().Text);
                    db.Entry(cN_ThanhLy).State = EntityState.Modified;
                    db.SaveChanges();
                    //TempData["err"] = "<div class='alert alert-success' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Cập nhật thành công</div> ";
                    #region luu lich su
                    // luu lich su
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                        ChucNang
                        , "UPDATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $"Cập nhật - Thanh lý - số hợp đồng { hopDong?.SoHopDong }");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    #endregion
                    return RedirectToAction("Index");
                }
                TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Có lỗi xảy ra</div> ";
                ViewBag.IDHD = DanhSachHopDong( cN_ThanhLy.IDHD.Value);
                return View(cN_ThanhLy);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Không ghi được dữ liệu.<br/>Lý do: " + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }

        #endregion

        #region Delete
        // POST: CN_ThanhLy/Delete/5
        [HttpPost]
        [CustomAuthorization]
        public ActionResult Delete(int id)
        {
            CN_ThanhLy cN_ThanhLy = db.CN_ThanhLy.Find(id);
            db.CN_ThanhLy.Remove(cN_ThanhLy);
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

        #region Private Menthod
        private SelectList DanhSachHopDong(int selectedValue = -1)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                SelectList result;
                if (selectedValue == -1)
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    var items = db.CN_HopDong.Select(p => new
                    {
                        p.IDHD,
                        ThongTin = p.SoHopDong
                    }).ToList();
                    items.Insert(0, new { IDHD = -1, ThongTin = "------ Chọn hợp đồng ------ " });
                    result = new SelectList(items, "IDHD", "ThongTin", selectedValue: selectedValue);

                }
                else
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    var items = db.CN_HopDong.Where(p => p.IDHD == selectedValue).Select(p => new
                    {
                        p.IDHD,
                        ThongTin = p.SoHopDong
                    }).ToList();
                    result = new SelectList(items, "IDHD", "ThongTin", selectedValue: selectedValue);
                }

                return result;
            }
            catch (Exception ex) {
                throw new Exception(ex.ToString());
            }

        }
        #endregion
    }
}
