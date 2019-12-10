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
using System.Linq.Dynamic;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using X.PagedList;

namespace HopDongMgr.Controllers
{
    public class DM_CongTrinhController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();
        private Common _Common = new Common();
        private string ChucNang = "Danh mục công trình";

        #region index

        // GET: DM_CongTrinh
        [CustomAuthorization]
        public ActionResult Index(int? page = 1)
        {
            db.Configuration.LazyLoadingEnabled = false;
            int pageIndex = (page < 1 ? 1 : page.Value);
            var pageSize = 10;
            int n = (pageIndex - 1) * pageSize;
            int totalData = db.DM_CongTrinh.Count();
            List<DM_CongTrinh> items = db.DM_CongTrinh.Include(d => d.DM_NguonVon).Include(d => d.DM_DiaDiem).OrderBy(p => p.TenCT).Skip(n).Take(pageSize).ToList();
            ViewBag.OnePageOfData = new StaticPagedList<DM_CongTrinh>(items, pageIndex, pageSize, totalData);
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
            List<DM_CongTrinh> items;
            int pageIndex = (page < 1 ? 1 : page.Value);
            var pageSize = 10;
            int n = (pageIndex - 1) * pageSize;
            if (string.IsNullOrEmpty(Seach))
            {
                TempData["Search"] = null;
                totalData = db.DM_CongTrinh.Count();
                items = db.DM_CongTrinh
                    .Include(d => d.DM_NguonVon)
                    .Include(d => d.DM_DiaDiem)
                    .OrderBy(p => p.TenCT)
                    .Skip(n)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                TempData["Search"] = Seach;
                totalData = db.DM_CongTrinh
                            .Where(o => (o.TenCT.Contains(Seach) || Seach == "") || (o.MaCT.Contains(Seach) || Seach == ""))
                            .Count();
                items = db.DM_CongTrinh
                    .Include(d => d.DM_NguonVon)
                    .Include(d => d.DM_DiaDiem)
                    .Where(o => (o.TenCT.Contains(Seach) || Seach == "") || (o.MaCT.Contains(Seach) || Seach == ""))
                    .OrderBy(p => p.TenCT)
                    .Skip(n).Take(pageSize)
                    .ToList();

            }
            ViewBag.OnePageOfData = new StaticPagedList<DM_CongTrinh>(items, pageIndex, pageSize, totalData);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_IndexPartial");
            }
            return View("Index");
        }

        // GET: DM_CongTrinh/Details/5
        [CustomAuthorization]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DM_CongTrinh dM_CongTrinh = db.DM_CongTrinh.Find(id);
            if (dM_CongTrinh == null)
            {
                return HttpNotFound();
            }
            return View(dM_CongTrinh);
        }
        #endregion

        #region Create
        // GET: DM_CongTrinh/Create
        [CustomAuthorization]
        public ActionResult Create()
        {
            ViewBag.MaNV = DanhSachNguonVon();
            ViewBag.MaDD = DanhSachDiaDiem();
            return View();
        }

        // POST: DM_CongTrinh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCT,MaCT,TenCT,MaNV,MaDD,Khoa,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat")] DM_CongTrinh dM_CongTrinh)
        {
            db.Configuration.LazyLoadingEnabled = false;
            try
            {
                int d = db.DM_CongTrinh.Count(p => string.Compare(p.MaCT.Trim().Replace("\n", "").Replace("\r", ""), dM_CongTrinh.MaCT.Trim()) == 0);
                if (d > 0) ModelState.AddModelError("MaCT", $"Mã công trình {dM_CongTrinh.MaCT} đã tồn tại");
                if (dM_CongTrinh.MaDD == "-1") ModelState.AddModelError("MaDD", $"Chưa chọn địa điểm");
                if (dM_CongTrinh.MaNV == "-1") ModelState.AddModelError("MaNV", $"Chưa chọn nguồn vốn");
                if (ModelState.IsValid)
                {
                    List<SelectListItem> list = _Common.getThongTinBang();
                    dM_CongTrinh.NguoiTao = list.Where(o => o.Value == "NguoiTao").SingleOrDefault().Text;
                    dM_CongTrinh.NgayTao = DateTime.Parse(list.Where(o => o.Value == "NgayTao").SingleOrDefault().Text);
                    dM_CongTrinh.MaCT_DA = dM_CongTrinh.MaCT;// mã công trình sử dụng khi nhâp trên chương trình
                    db.DM_CongTrinh.Add(dM_CongTrinh);
                    db.SaveChanges();
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                        ChucNang
                        , "CREATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $" Thêm mới - Mã CT {dM_CongTrinh.MaCT} ");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.MaNV = DanhSachNguonVon(dM_CongTrinh.MaNV);
                ViewBag.MaDD = DanhSachDiaDiem(dM_CongTrinh.MaDD);
                return View(dM_CongTrinh);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Không ghi được dữ liệu.<br/>Lý do: " + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }
        #endregion

        #region Update
        // GET: DM_CongTrinh/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Configuration.LazyLoadingEnabled = false;
            DM_CongTrinh dM_CongTrinh = db.DM_CongTrinh.Find(decimal.Parse(id));
            if (dM_CongTrinh == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNV = DanhSachNguonVon(dM_CongTrinh.MaNV);
            ViewBag.MaDD = DanhSachDiaDiem(dM_CongTrinh.MaDD);
            return View(dM_CongTrinh);
        }

        // POST: DM_CongTrinh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCT,MaCT,TenCT,MaNV,MaDD,Khoa,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat,MaCT_DA")] DM_CongTrinh dM_CongTrinh)
        {
            db.Configuration.LazyLoadingEnabled = false;
            try
            {
                int d = db.DM_CongTrinh.Count(p => p.IDCT != dM_CongTrinh.IDCT && string.Compare(p.MaCT.Trim().Replace("\n", "").Replace("\r", ""), dM_CongTrinh.MaCT.Trim()) == 0);
                if (d > 0) ModelState.AddModelError("MaCT", $"Mã công trình {dM_CongTrinh.MaCT} bị trùng.");
                if (dM_CongTrinh.MaDD == "-1") ModelState.AddModelError("MaDD", $"Chưa chọn địa điểm");
                if (dM_CongTrinh.MaNV == "-1") ModelState.AddModelError("MaNV", $"Chưa chọn nguồn vốn");
                if (ModelState.IsValid)
                {
                    List<SelectListItem> list = _Common.getThongTinBang();
                    dM_CongTrinh.NguoiCapNhat = list.Where(o => o.Value == "NguoiCapNhat").SingleOrDefault().Text;
                    dM_CongTrinh.NgayCapNhat = DateTime.Parse(list.Where(o => o.Value == "NgayCapNhat").SingleOrDefault().Text);
                    db.Entry(dM_CongTrinh).State = EntityState.Modified;
                    db.SaveChanges();
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                        ChucNang
                        , "UPDATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $" Cập nhật - Mã CT {dM_CongTrinh.MaCT} ");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.MaNV = DanhSachNguonVon(dM_CongTrinh.MaNV);
                ViewBag.MaDD = DanhSachDiaDiem(dM_CongTrinh.MaDD);
                return View(dM_CongTrinh);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Lỗi ghi dữ liệu.<br/>Lý do:" + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }
        #endregion

        // POST: DM_CongTrinh/Delete/5
        [HttpPost]
        [CustomAuthorization]
        public ActionResult Delete(int id)
        {
            DM_CongTrinh dM_CongTrinh = db.DM_CongTrinh.Find(id);
            db.DM_CongTrinh.Remove(dM_CongTrinh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult GetList()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchVale = Request["search[Value]"];
            string sorColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];


            List<oCongTrinh> list = new List<oCongTrinh>();
            list = db.DM_CongTrinh.Select(x => new oCongTrinh { DT_RowId = x.IDCT, MaCT = (!string.IsNullOrEmpty(x.MaCT) ? x.MaCT : "-"), TenCT = x.TenCT, TenNV = x.DM_NguonVon.TenNV, TenDD = x.DM_DiaDiem.TenDD, Khoa = x.Khoa }).ToList();
            //List<DM_CongTrinh> list = new List<DM_CongTrinh>();
            //list = db.DM_CongTrinh.ToList();
            int TotalRecord = list.Count;
            if (!string.IsNullOrEmpty(searchVale))
            {
                list = list.Where(x => x.MaCT.ToLower().Contains(searchVale.ToLower())).ToList();
            }
            int totalrowFiter = list.Count;
            list = list.OrderBy(sorColumnName + " " + sortDirection).ToList();
            list = list.Skip(start).Take(length).ToList();

            List<oCongtrinh_Data> JsonCongTrinh = new List<oCongtrinh_Data>();
            foreach (var o in list)
            {
                //oCongtrinh_Data data = new oCongtrinh_Data();
                //data.DT_RowId = o.MaCT;
                //data.oCongTrinh.MaCT = o.MaCT;
                //data.oCongTrinh.TenCT = o.TenCT;
                //data.oCongTrinh.TenDD = o.DM_DiaDiem.TenDD;
                //data.oCongTrinh.TenNV = o.DM_NguonVon.TenNV;
                //data.oCongTrinh.Khoa = o.Khoa;
                //JsonCongTrinh.Add(data);
            }
            return Json(new { data = list, draw = Request["draw"], recordsTotal = TotalRecord, recordsFiltered = totalrowFiter }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DongBo_DanhMucCongTrinh()
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
            catch (Exception ex)
            {
                err = "0"; // thất bại
            }

            return Json(err);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region private methods
        private  SelectList DanhSachNguonVon(string selectedValue = "")
        {
            db.Configuration.LazyLoadingEnabled = false;
            var items =  db.DM_NguonVon.Where(o => o.Khoa.Value.CompareTo(false) == 0)
                                     .Select(p => new {
                                         p.MaNV,
                                         ThongTin = p.MaNV + " - " + p.TenNV
                                     })
                                     .ToList();
            items.Insert(0, new { MaNV = "-1", ThongTin = "------ Chọn loại------ " });
            var result = new SelectList(items, "MaNV", "ThongTin", selectedValue: selectedValue);
            return result;
        }
        private SelectList DanhSachDiaDiem(string selectedValue = "")
        {
            db.Configuration.LazyLoadingEnabled = false;
            var items = db.DM_DiaDiem.Where(o => o.Khoa.Value.CompareTo(false) == 0)
                                     .Select(p => new {
                                         p.MaDD,
                                         ThongTin = p.MaDD + " - " + p.TenDD
                                     })
                                     .ToList();
            items.Insert(0, new { MaDD = "-1", ThongTin = "------ Chọn loại------ " });
            var result = new SelectList(items, "MaDD", "ThongTin", selectedValue: selectedValue);
            return result;
        }
        #endregion
    }
    public class oCongtrinh_Data
    {

        public string DT_RowId { get; set; }
        public oCongTrinh oCongTrinh { get; set; }
    }
    public class oCongTrinh
    {
        public decimal DT_RowId { get; set; }
        public string MaCT { get; set; }
        public string TenCT { get; set; }
        public string TenNV { get; set; }
        public string TenDD { get; set; }
        public Nullable<bool> Khoa { get; set; }
    }
}
