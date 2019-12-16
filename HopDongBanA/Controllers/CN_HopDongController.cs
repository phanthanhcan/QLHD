using System;
using System.Collections;
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
using System.Linq.Expressions;
using X.PagedList;

namespace HopDongMgr.Controllers
{
    public class CN_HopDongController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();
        private Common _common = new Common();
        private string ChucNang = "Chức năng hợp đồng";

        #region lấy danh sách 
        // GET: CN_HopDong
        [CustomAuthorization]
        public ActionResult Index(int? page = 1)
        {
            db.Configuration.LazyLoadingEnabled = false;
            int pageIndex = (page < 1 ? 1 : page.Value);
            var pageSize = 10;
            int n = (pageIndex - 1) * pageSize;
            int totalData = db.CN_HopDong.Count();
            List<CN_HopDong> items = db.CN_HopDong
                                    .Include(d => d.DM_CongTrinh)
                                    .Include(d => d.DM_DonViThucHien)
                                    .Include(d => d.DM_HinhThucHopDong)
                                    .Include(d => d.DM_LoaiHopDong)
                                    .OrderByDescending(p => p.NgayKy)
                                    .Skip(n).Take(pageSize)
                                    .ToList();
            ViewBag.OnePageOfData = new StaticPagedList<CN_HopDong>(items, pageIndex, pageSize, totalData);
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
            List<CN_HopDong> items;
            int pageIndex = (page < 1 ? 1 : page.Value);
            var pageSize = 10;
            int n = (pageIndex - 1) * pageSize;
            if (string.IsNullOrEmpty(Seach))
            {
                TempData["Search"] = null;
                totalData = db.CN_HopDong.Count();
                items = db.CN_HopDong
                        .Include(d => d.DM_CongTrinh)
                        .Include(d => d.DM_DonViThucHien)
                        .Include(d => d.DM_HinhThucHopDong)
                        .Include(d => d.DM_LoaiHopDong)
                        .OrderByDescending(p => p.NgayKy)
                        .Skip(n)
                        .Take(pageSize)
                        .ToList();
            }
            else
            {
                TempData["Search"] = Seach;
                totalData = db.CN_HopDong
                        .Where(o => o.SoHopDong.Contains(Seach)).Count();
                items = db.CN_HopDong
                        .Include(d => d.DM_CongTrinh)
                        .Include(d => d.DM_DonViThucHien)
                        .Include(d => d.DM_HinhThucHopDong)
                        .Include(d => d.DM_LoaiHopDong)
                        .Where(o => o.SoHopDong.Contains(Seach))
                        .OrderByDescending(p => p.NgayKy)
                        .Skip(n)
                        .Take(pageSize)
                        .ToList();

            }
            ViewBag.OnePageOfData = new StaticPagedList<CN_HopDong>(items, pageIndex, pageSize, totalData);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_IndexPartial");
            }
            return View("Index");
        }
        public ActionResult IndexBackup( int? NamGiaoA, int? NamKyHD, string MaCT, bool? IsHoanThanh)
        {
            Guid IDNguoiDung = (Guid)Session["userid"];
            List<GetList_HopDong_Result> list = new List<GetList_HopDong_Result>();
            if (Session["userid"] != null)
            {
               list = db.GetList_HopDong(NamGiaoA,IsHoanThanh, MaCT, IDNguoiDung, NamKyHD).ToList();
            }
            ViewBag.list = list;
            ViewBag.NamGiaoA = NamGiaoA.ToString();
            ViewBag.NamKyHD = NamKyHD.ToString();
            ViewBag.MaCT = MaCT;
            ViewBag.IsHoanThanh = IsHoanThanh;
            return View();
        }

        public ActionResult Index_HetHanDieuChinh()
        {
            if (Session["userid"] != null)
            {
                #region ajax
                //int RowPerPage =  TempData["RowPerPage"] == null ? 10 : (int)TempData["RowPerPage"];
                //int Page =   Request["page"] == null ? 1 : Int32.Parse( Request["page"].ToString());

                //Guid IDNguoiDung = (Guid)Session["userid"];
                ////IEnumerable<GetList_HopDongHetHan_Result> query = db.GetList_HopDongHetHan(IDNguoiDung, DateTime.Now.Date, -1).AsEnumerable();
                ////List<GetList_HopDongHetHan_Result> query1 = db.GetList_HopDongHetHan(IDNguoiDung, DateTime.Now.Date, -1).Skip((Page-1)*RowPerPage).Take(RowPerPage).ToList();
                //List<GetList_HopDongHetHan_Result> query = db.GetList_HopDongHetHan(IDNguoiDung, DateTime.Now.Date, -1).ToList();

                //List<GetList_HopDongHetHan_Result> lis1 = db.GetList_HopDongHetHan(IDNguoiDung, DateTime.Now.Date, -1).ToList();
                //List<GetList_HopDongHetHan_Result> lis = db.GetList_HopDongHetHan(IDNguoiDung, DateTime.Now.Date, -1).Skip(2).Take(2).ToList();
                ////IQueryable<CN_HopDong> query = db.CN_HopDong.AsQueryable();
                //if (Request.IsAjaxRequest()) //  nếu truy vấn ajac thì chỉ load patial
                //{
                //    return PartialView("_Index_HetHanDieuChinhPartial", query);
                //}

                //return View(model: query); //lần dầu tiên star thì load cả view,
                #endregion
                Guid IDNguoiDung = (Guid)Session["userid"];
                return View(db.GetList_HopDongHetHan(IDNguoiDung, DateTime.Now.Date, -1).ToList());
            }
            else
                return RedirectToAction("Login", "Home");
        }

        public ActionResult _Index_HetHanDieuChinhPartial()
        {
            if (Session["userid"] != null)
            {
                Guid IDNguoiDung = (Guid)Session["userid"];
                return View(db.GetList_HopDongHetHan(IDNguoiDung, DateTime.Now.Date, -1).ToList());
            }
            else
                return RedirectToAction("Login", "Home");
        }

        public ActionResult Index_DanhSachHopDongHetHan()
        {
            try
            {
                Guid IDNguoiDung = (Guid)Session["userid"];
                List<List<GetList_HopDongHetHan_ALL_Model>> List = new List<List<GetList_HopDongHetHan_ALL_Model>>();

                object[] listParaDC = {
                    new SqlParameter { ParameterName = "idNguoiDung", SqlDbType = SqlDbType.UniqueIdentifier, Value = IDNguoiDung },
                    new SqlParameter { ParameterName = "pNgay", SqlDbType = SqlDbType.Date, Value = DateTime.Now.Date },
                    new SqlParameter { ParameterName = "pSoNgay", SqlDbType = SqlDbType.Int, Value = "1" },
                    new SqlParameter { ParameterName = "pLoai", SqlDbType = SqlDbType.VarChar, Value = "DC" } // điều chỉnh
                };
                List<GetList_HopDongHetHan_ALL_Model> lHDHetHanDC = db.Database.SqlQuery<GetList_HopDongHetHan_ALL_Model>($"EXEC [dbo].[GetList_HopDongHetHan_ALL] @idNguoiDung, @pNgay , @pSoNgay, @pLoai ", listParaDC).ToList();
                List.Insert(0, lHDHetHanDC);

                object[] listParaTC = {
                    new SqlParameter { ParameterName = "idNguoiDung", SqlDbType = SqlDbType.UniqueIdentifier, Value = IDNguoiDung },
                    new SqlParameter { ParameterName = "pNgay", SqlDbType = SqlDbType.Date, Value = DateTime.Now.Date },
                    new SqlParameter { ParameterName = "pSoNgay", SqlDbType = SqlDbType.Int, Value = "1" },
                    new SqlParameter { ParameterName = "pLoai", SqlDbType = SqlDbType.VarChar, Value = "TC" } // thi công
                };
                List<GetList_HopDongHetHan_ALL_Model> lHDHetHanTC = db.Database.SqlQuery<GetList_HopDongHetHan_ALL_Model>($"EXEC [dbo].[GetList_HopDongHetHan_ALL] @idNguoiDung, @pNgay , @pSoNgay, @pLoai ", listParaTC).ToList();
                List.Insert(1, lHDHetHanTC);

                object[] listParaTH = {
                    new SqlParameter { ParameterName = "idNguoiDung", SqlDbType = SqlDbType.UniqueIdentifier, Value = IDNguoiDung },
                    new SqlParameter { ParameterName = "pNgay", SqlDbType = SqlDbType.Date, Value = DateTime.Now.Date },
                    new SqlParameter { ParameterName = "pSoNgay", SqlDbType = SqlDbType.Int, Value = "1" },
                    new SqlParameter { ParameterName = "pLoai", SqlDbType = SqlDbType.VarChar, Value = "TH" } // thực hiện
                };
                List<GetList_HopDongHetHan_ALL_Model> lHDHetHanTH = db.Database.SqlQuery<GetList_HopDongHetHan_ALL_Model>($"EXEC [dbo].[GetList_HopDongHetHan_ALL] @idNguoiDung, @pNgay , @pSoNgay, @pLoai ", listParaTH).ToList();
                List.Insert(2, lHDHetHanTH);


                return View(model: List);
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult GetList_HetHanDieuChinh()
        {
            #region
            if (Session["userid"] != null)
            {
                Guid IDNguoiDung = (Guid)Session["userid"];
                return View(db.GetList_HopDongHetHan(IDNguoiDung, DateTime.Now.Date, 20).ToList());
            }
            else
                return RedirectToAction("Login", "Home");
            #endregion
            //int start = Convert.ToInt32(Request["start"]);
            //int length = Convert.ToInt32(Request["length"]);
            //string searchVale = Request["search[Value]"];
            //string sorColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            //string sortDirection = Request["order[0][dir]"];


            //if (Session["userid"] != null)
            //{
            //    Guid IDNguoiDung = (Guid)Session["userid"];
            //    List<oCN_HopDong> list = new List<oCN_HopDong>();
            //    list = db.GetList_HopDongHetHan(IDNguoiDung, DateTime.Now.Date, 20).Select(x => new oCN_HopDong
            //    {
            //        DT_RowId = x.IDHD
            //        ,
            //        TenCT = x.DM_CongTrinh == null ? "-" : x.DM_CongTrinh.TenCT
            //        ,
            //        SoHD = x.SoHopDong
            //        ,
            //        NgayKy = x.NgayKy.Value.ToString() //x.NgayKy.Value.ToString("dd/MM/yyyy")//string.Format("{0}",string.Format("{0:dd-MM-yyyy}", x.NgayKy.Value))//x.NgayKy.HasValue == true ? x.NgayKy.Value.ToLongDateString(): "-"
            //        ,
            //        GiaTriHopDong = x.GiaTriHopDong.ToString()//string.Format("{0:0,0}", x.GiaTriHopDong.Value) // x.GiaTriHopDong
            //        ,
            //        SoNgayThucHien = x.SoNgayThucHien.ToString()
            //        ,
            //        SoNgayThiCong = x.SoNgayThiCong.ToString()
            //        ,
            //        NamGiaoA = x.NamGiaoA
            //        ,
            //        TenDVTH = string.IsNullOrEmpty(x.DM_DonViThucHien.TenDV) ? "-" : x.DM_DonViThucHien.TenDV
            //        ,
            //        GiaTriGoiThau = x.GiaTriGoiThau.ToString()
            //        ,
            //        TenHTHD = string.IsNullOrEmpty(x.DM_HinhThucHopDong.TenHinhThuc) ? "-" : x.DM_HinhThucHopDong.TenHinhThuc
            //        ,
            //        NoiDung = x.NoiDung
            //        ,
            //        NguoiTheoDoi = x.NguoiTao
            //        ,
            //        NgayCapNhat = x.NgayCapNhat

            //    }).ToList();
            //    int TotalRecord = list.Count;
            //    if (!string.IsNullOrEmpty(searchVale))
            //    {
            //        list = list.Where(x => x.SoHD.ToLower().Contains(searchVale.ToLower())
            //        || x.TenCT.ToLower().Contains(searchVale.ToLower())
            //        ).ToList();
            //    }
            //    int totalrowFiter = list.Count;
            //    list = list.OrderBy(sorColumnName + " " + sortDirection).ToList();
            //    list = list.Skip(start).Take(length).ToList();

            //    return Json(new { data = list, draw = Request["draw"], recordsTotal = TotalRecord, recordsFiltered = totalrowFiter }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //    return RedirectToAction("Login", "Home");
        }


        /// <summary>
        /// processing-side
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetList1()
        {
            Guid gIDMaPhongBan = (Guid)Session["IDMaPhongBan"];
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchVale = Request["search[Value]"];
            string sorColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            DateTime deDateTime = new DateTime(1990, 1, 1);
            int Nam = 2018;
            bool IsHoanThanh = false;
            string MaCT = "";
            Guid IDNguoiDung = (Guid)Session["userid"];
            List<GetList_HopDong_Result> list = new List<GetList_HopDong_Result>();
            if (Session["userid"] != null)
            {
                //List<GetList_HopDong_Result> list = new List<GetList_HopDong_Result>();
                //list = db.Database.SqlQuery<GetList_HopDong_Result>("GetList_HopDong @Nam, @IsHoanThanh, @MaCT, @idNguoiDung", Parameter).ToList();
                //List<GetList_HopDong_Result> list = db.GetList_HopDong(Nam, IsHoanThanh, MaCT, IDNguoiDung).ToList();
                //int TotalRecord = list.Count;
                //if (!string.IsNullOrEmpty(searchVale))
                //{
                //    list = list.Where(x => x.SoHD.ToLower().Contains(searchVale.ToLower())
                //    || x.TenCT.ToLower().Contains(searchVale.ToLower())
                //    ).ToList();
                //}
                //int totalrowFiter = list.Count;
                //list = list.OrderBy(sorColumnName + " " + sortDirection).ToList();
                //list = list.Skip(start).Take(length).ToList();

                return Json(new { data = list, draw = Request["draw"], recordsTotal = 1, recordsFiltered = 1 }, JsonRequestBehavior.AllowGet);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        #endregion

        #region create
        // GET: CN_HopDong/Create
        [CustomAuthorization]
        public ActionResult Create()
        {
            try
            {
                ViewBag.IDLoai = DanhSachLoaiHopDongPhanQuyen();
                //Cong trinh
                ViewBag.IDCT = DanhSachCongTrinh();
                //Don vi thuc hien
                ViewBag.IDDV = DanhSachDonViThucHien();
                //Hinh thuc hop dong
                ViewBag.IDHT = DanhSachHinhThucHopDong();
                //
                ViewBag.NguoiTao = DanhSachNguoiTao();

                return View();
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Không lấy được dữ liệu.<br/>Lý do: " + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }

        // POST: CN_HopDong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDHD,IDLoai,MaCT,NoiDung,MaDD,NamGiaoA,IDDV,GiaTriGoiThau,IDHT,SoHopDong,NgayKy,GiaTriHopDong,SoNgayThucHien,SoNgayThiCong,NgayTongNghiemThu,XuLyViPham,GiaTriPhat,XuLyTranhChap,IsHoanThanh,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat,NgayHetHan,IDCT")] CN_HopDong cN_HopDong)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                int dHopDong = db.CN_HopDong.Where(p => !string.IsNullOrEmpty(cN_HopDong.SoHopDong) && string.Compare(p.SoHopDong.Trim().Replace("\n", "").Replace("\r", ""), cN_HopDong.SoHopDong.Trim()) == 0).Count();
                if (dHopDong > 0) ModelState.AddModelError("SoHopDong", "Số hợp đồng đã tồn tại.");
                if (cN_HopDong.IDLoai.GetValueOrDefault(-1) == -1) ModelState.AddModelError("IDLoai", "Chọn loại hợp đồng.");
                if (cN_HopDong.IDCT.GetValueOrDefault(-1) == -1) ModelState.AddModelError("IDCT", "Chọn công trình.");
                if (cN_HopDong.IDHT.GetValueOrDefault(-1) == -1) ModelState.AddModelError("IDHT", "Chọn hình thức hợp đồng.");
                // giá trị hợp đông nhỏ hơn giá trị gói thầu
                if(cN_HopDong.GiaTriHopDong.GetValueOrDefault(-1) > cN_HopDong.GiaTriGoiThau.GetValueOrDefault(-1)) ModelState.AddModelError("GiaTriHopDong", "Giá trị hợp đồng < Giá trị gói thầu.");
                //số ngày thi công nhỏ hơn số ngày thực hiện
                if (cN_HopDong.SoNgayThiCong.GetValueOrDefault(-1) > cN_HopDong.SoNgayThucHien.GetValueOrDefault(-1)) ModelState.AddModelError("GiaTriHopDong", "Số ngày thi công < Số ngày thực hiện.");
                if (cN_HopDong.IDDV == -1) cN_HopDong.IDDV = null;
                if (cN_HopDong.NgayHetHan < cN_HopDong.NgayKy) ModelState.AddModelError("NgayHetHan", "Ngày hết hạn HĐ phải lớn hơn ngày ký HĐ");
                DateTime cNgayHetHan = cN_HopDong.NgayKy.Value;
                if (cN_HopDong.NgayHetHan < cNgayHetHan.AddDays(cN_HopDong.SoNgayThucHien.GetValueOrDefault(0))) ModelState.AddModelError("NgayHetHan", "Ngày hết hạn HĐ phải lớn hơn ngày ký HĐ + Số ngày thực hiện");
                if (ModelState.IsValid)
                {
                    //
                    List<SelectListItem> list = _common.getThongTinBang();
                    cN_HopDong.NguoiTao = Guid.Parse(list.Where(o => o.Value == "NguoiTao").SingleOrDefault().Text);
                    cN_HopDong.NgayTao = DateTime.Parse(list.Where(o => o.Value == "NgayTao").SingleOrDefault().Text);
                    db.CN_HopDong.Add(cN_HopDong);
                    db.SaveChanges();
                    // luu lich su
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                        ChucNang
                        , "CREATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $" Thêm mới - Số hợp đồng {cN_HopDong.SoHopDong} ");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.IDCT = DanhSachCongTrinh(cN_HopDong.IDCT);
                ViewBag.IDDV = DanhSachDonViThucHien(cN_HopDong.IDDV);
                ViewBag.IDHT = DanhSachHinhThucHopDong(cN_HopDong.IDHT);
                ViewBag.NguoiTao = DanhSachNguoiTao( cN_HopDong.NguoiTao?.ToString());
                ViewBag.IDLoai = DanhSachLoaiHopDong( cN_HopDong.IDLoai);
                return View(cN_HopDong);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Không ghi được dữ liệu.<br/>Lý do: " + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }

        #endregion

        #region update
        // GET: CN_HopDong/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                CN_HopDong cN_HopDong = db.CN_HopDong.Find(id);
                if (cN_HopDong == null)
                {
                    return HttpNotFound();
                }
                ViewBag.IDLoai = DanhSachLoaiHopDongPhanQuyen(cN_HopDong.IDLoai);
                //Cong trinh
                ViewBag.IDCT = DanhSachCongTrinh(cN_HopDong.IDCT);
                //Don vi thuc hien
                ViewBag.IDDV = DanhSachDonViThucHien(cN_HopDong.IDDV);
                //Hinh thuc hop dong
                ViewBag.IDHT = DanhSachHinhThucHopDong(cN_HopDong.IDHT);
                //
                ViewBag.NguoiTao = DanhSachNguoiTao(cN_HopDong.NguoiTao.GetValueOrDefault(Guid.Empty).ToString());

                return View(cN_HopDong);
            }
            catch(Exception ex)
            {
                string cauBaoLoi = "Không lấy được dữ liệu.<br/>Lý do: " + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }

        }

        // POST: CN_HopDong/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( CN_HopDong cN_HopDong)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                int dHopDong = db.CN_HopDong.Where(p => !string.IsNullOrEmpty(cN_HopDong.SoHopDong) && p.IDHD != cN_HopDong.IDHD && string.Compare(p.SoHopDong.Trim().Replace("\n", "").Replace("\r", ""), cN_HopDong.SoHopDong.Trim()) == 0).Count();
                if (dHopDong > 0) ModelState.AddModelError("SoHopDong", "Số hợp đồng đã tồn tại.");
                if (cN_HopDong.IDLoai.GetValueOrDefault(-1) == -1) ModelState.AddModelError("IDLoai", "Chọn loại hợp đồng.");
                if (cN_HopDong.IDCT.GetValueOrDefault(-1) == -1) ModelState.AddModelError("IDCT", "Chọn công trình.");
                if (cN_HopDong.IDHT.GetValueOrDefault(-1) == -1) ModelState.AddModelError("IDHT", "Chọn hình thức hợp đồng.");
                // giá trị hợp đông nhỏ hơn giá trị gói thầu
                if (cN_HopDong.GiaTriHopDong.GetValueOrDefault(-1) > cN_HopDong.GiaTriGoiThau.GetValueOrDefault(-1)) ModelState.AddModelError("GiaTriHopDong", "Giá trị hợp đồng < Giá trị gói thầu.");
                //số ngày thi công nhỏ hơn số ngày thực hiện
                if (cN_HopDong.SoNgayThiCong.GetValueOrDefault(-1) > cN_HopDong.SoNgayThucHien.GetValueOrDefault(-1)) ModelState.AddModelError("SoNgayThiCong", "Số ngày thi công < Số ngày thực hiện.");
                if (cN_HopDong.IDDV == -1) cN_HopDong.IDDV = null;
                if (cN_HopDong.NgayHetHan < cN_HopDong.NgayKy) ModelState.AddModelError("NgayHetHan", "Ngày hết hạn HĐ phải lớn hơn ngày ký HĐ");
                DateTime cNgayHetHan = cN_HopDong.NgayKy.Value;
                if (cN_HopDong.NgayHetHan < cNgayHetHan.AddDays(cN_HopDong.SoNgayThucHien.GetValueOrDefault(0))) ModelState.AddModelError("NgayHetHan", "Ngày hết hạn HĐ phải lớn hơn ngày ký HĐ + Số ngày thực hiện");

                if (ModelState.IsValid)
                {
                    List<SelectListItem> list = _common.getThongTinBang();
                    cN_HopDong.NguoiCapNhat = list.Where(o => o.Value == "NguoiTao").SingleOrDefault().Text;
                    cN_HopDong.NgayCapNhat = DateTime.Parse(list.Where(o => o.Value == "NgayTao").SingleOrDefault().Text);

                    var cN_HopDongUpdate = db.CN_HopDong.Find(cN_HopDong.IDHD);
                    TryUpdateModel(cN_HopDongUpdate, new string[] {
                        "IDLoai","SoHopDong","NgayKy","NoiDung","IDCT", "IDDV", "IDHT", "NamGiaoA", "GiaTriHopDong", "GiaTriGoiThau",
                        "SoNgayThiCong", "SoNgayThucHien", "NgayHetHan", "NguoiCapNhat", "NgayCapNhat"
                    });
                    db.SaveChanges();
                    TempData["err"] = "<div class='alert alert-success' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span> Cập nhật thành công</div> ";
                    // luu lich su
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                    ChucNang
                    , "UPDATE"
                    , DateTime.Now, Session["username"]?.ToString()
                    , $" Cập nhật - Tên hợp đồng {cN_HopDong.SoHopDong} ");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.IDLoai = DanhSachLoaiHopDongPhanQuyen(cN_HopDong.IDLoai);
                //Cong trinh
                ViewBag.IDCT = DanhSachCongTrinh(cN_HopDong.IDCT);
                //Don vi thuc hien
                ViewBag.IDDV = DanhSachDonViThucHien(cN_HopDong.IDDV);
                //Hinh thuc hop dong
                ViewBag.IDHT = DanhSachHinhThucHopDong(cN_HopDong.IDHT);
                //nguoi tao
                ViewBag.NguoiTao = DanhSachNguoiTao(cN_HopDong.NguoiTao.GetValueOrDefault(Guid.Empty).ToString());
                return View(cN_HopDong);

            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Không lấy được dữ liệu.<br/>Lý do: " + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }

        #endregion
        
        #region Xử phạt
        public ActionResult Edit_XuPhatTranhChap(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Configuration.LazyLoadingEnabled = false;
            CN_HopDong cN_HopDong = db.CN_HopDong.Find(Id);
            if (cN_HopDong == null)
            {
                return HttpNotFound();
            }
            return View(cN_HopDong);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_XuPhatTranhChap( CN_HopDong cN_HopDong)
        {
            try
            {
                if (cN_HopDong.GiaTriPhat.GetValueOrDefault(0) < 1000) ModelState.AddModelError("GiaTriPhat", "Số tiền phải lớn hơn 1,000VND");
                if (string.IsNullOrWhiteSpace(cN_HopDong.XuLyTranhChap)) ModelState.AddModelError("GiaTriPhat", "Nhập nội dung xử lý tranh chấp");
                if (string.IsNullOrWhiteSpace(cN_HopDong.XuLyViPham)) ModelState.AddModelError("XuLyViPham", "Nhập nội dung xử lý vi phạm");
                if (ModelState.IsValid)
                {
                    var cN_HopDongUpdate = db.CN_HopDong.Find(cN_HopDong.IDHD);
                    TryUpdateModel(cN_HopDongUpdate, new string[] {
                        "GiaTriPhat","XuLyTranhChap","XuLyViPham"
                    });
                    db.SaveChanges();
                    TempData["err"] = "<div class='alert alert-success' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Cập nhật thành công</div> ";
                    // luu lich su
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                         ChucNang
                        , "UPDATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $" Cập nhật - xử phat tranh chấp - Hợp đồng {cN_HopDong.SoHopDong} ");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    return View(cN_HopDong);
                }

                return View(cN_HopDong);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Không lấy được dữ liệu.<br/>Lý do: " + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }
        #endregion

        #region Delete
        // POST: CN_HopDong/Delete/5
        [HttpPost]
        [CustomAuthorization]
        public ActionResult Delete(int id)
        {
            CN_HopDong cN_HopDong = db.CN_HopDong.Find(id);
            db.CN_HopDong.Remove(cN_HopDong);
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

        #region Đồng bộ
        [HttpPost]
        public ActionResult DongBo_CNHopDong()
        {
            string err = "";
            try
            {
                //string sqlQuery = "exec GetDTXD_DanhMucDiaDiem @Check OUTPUT";
                string sqlQuery = "EXEC @return_value = GetDTXD_CNHopDong";
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
        #endregion

        #region private methods
        private SelectList DanhSachCongTrinh(Decimal? selectedValue = -1)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var items = db.DM_CongTrinh.OrderBy(o => o.TenCT).Select(p => new {
                                         p.IDCT,
                                         ThongTin = p.MaCT + " - " + p.TenCT
                                     })
                                     .ToList();
            items.Insert(0, new {  IDCT = Decimal.Parse("-1"), ThongTin = "------ Chọn Công trình------ " });
            var result = new SelectList(items, "IDCT", "ThongTin", selectedValue: selectedValue);
            return result;
        }
        private SelectList DanhSachDonViThucHien(int? selectedValue = -1)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var items = db.DM_DonViThucHien.Select(p => new {
                                                             IDDV = p.IDDV.ToString(),
                                                             ThongTin = p.IDDV + " - " + p.TenDV
                                                            }).ToList();
            items.Insert(0, new { IDDV = "-1", ThongTin = "------ Chọn ĐV thực hiện------ " });
            var result = new SelectList(items, "IDDV", "ThongTin", selectedValue: "1001");
            return result;
        }
        private SelectList DanhSachHinhThucHopDong(int? selectedValue = -1)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var items = db.DM_HinhThucHopDong.Select(p => new{
                                                                p.IDHT,
                                                                ThongTin = p.IDHT + " - " + p.TenHinhThuc
                                                                }).ToList();
            items.Insert(0, new { IDHT = -1, ThongTin = "------ Chọn hình thức HĐ------ " });
            var result = new SelectList(items, "IDHT", "ThongTin", selectedValue: selectedValue);
            return result;
        }
        private SelectList DanhSachNguoiTao(String  selectedValue = "-1")
        {
            db.Configuration.LazyLoadingEnabled = false;
            Guid gucerid = (Guid)Session["userid"];
            var items = db.HT_NguoiDung.Where(p => p.oid == gucerid).Select(p => new {
                                                            oid = p.oid.ToString(),
                                                            ThongTin = p.HoTen
                                                        }).ToList();
            var result = new SelectList(items, "oid", "ThongTin");
            return result;
        }
        private SelectList DanhSachLoaiHopDong(int? selectedValue = -1)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var items = db.DM_LoaiHopDong.Select(p => new {
                                                            p.IDLoai,
                                                            ThongTin = p.IDLoai + " - " + p.TenLoai
                                                        }).ToList();
            items.Insert(0, new { IDLoai = -1, ThongTin = "------ Chọn loại HĐ------ " });
            var result = new SelectList(items, "IDLoai", "ThongTin", selectedValue: selectedValue);
            return result;
        }
        private SelectList DanhSachLoaiHopDongPhanQuyen(int? selectedValue = -1)
        {
            db.Configuration.LazyLoadingEnabled = false;
            List<DM_LoaiHopDong> LiQuyenLoaiHD = new List<DM_LoaiHopDong>();
            Guid gIDMaPhongBan = (Guid)Session["IDMaPhongBan"];
            Guid guserid = (Guid)Session["userid"];
            //Load combobox
            //Loai hop dong
            List<HT_PhongBan_LoaiHopDong> LiQuyen = db.HT_PhongBan_LoaiHopDong.Where(o => o.IDPB.CompareTo(gIDMaPhongBan) == 0).ToList();
            DM_LoaiHopDong LoaiHD = new DM_LoaiHopDong();
            List<dynamic> listHD = new List<dynamic>();
            foreach (var item in LiQuyen)
            {
                LoaiHD = db.DM_LoaiHopDong.Find(item.IDLoaiHopDong);
                dynamic ItemHD = new { LoaiHD.IDLoai, ThongTin = $"{LoaiHD.IDLoai} - {LoaiHD.TenLoai}" };
                listHD.Add(ItemHD);

            }

            listHD.Insert(0, new { IDLoai = -1, ThongTin = "------ Chọn loại HĐ------ " });
            var result = new SelectList(listHD, "IDLoai", "ThongTin", selectedValue: selectedValue);
            return result;
        }
        #endregion
    }

    public class oCN_HopDong
    {
        private string _NgayKy;
        private string _GiaTriHopDong;
        private string _GiaTriGoiThau;
        private string _SoNgayThucHien;
        private string _SoNgayThiCong;
        public int DT_RowId { get; set; }
        public string TenCT { get; set; }
        public string SoHD { get; set; }
        public string NgayKy {
            get { return _NgayKy; }

            set {
                string[] A = new string[] { };
                if (value.Contains("/"))
                    _NgayKy = value.Substring(0, 10);
                else if (value.Contains("-"))
                {
                    A = value.Split('-');
                    _NgayKy = A[2] + "/" + A[1] + "/" + A[0];
                }
                else
                    _NgayKy = value.Substring(0, 10);
                if (value.Contains("1990")) _NgayKy = "";

            }
        }
        public string GiaTriHopDong {
            get { return _GiaTriHopDong; }
            set {
                    if(string.IsNullOrWhiteSpace(value)==true || string.IsNullOrEmpty(value) == true)
                    {
                        _GiaTriHopDong = "0";
                    }
                    else
                    {
                        decimal i = Convert.ToDecimal(value.ToString());
                        _GiaTriHopDong = string.Format("{0:0,0}", i);
                    }
                 }
        }
        public string GiaTriGoiThau
        {
            get { return _GiaTriGoiThau; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) == true || string.IsNullOrEmpty(value) == true)
                {
                    _GiaTriGoiThau = "0";
                }
                else
                {
                    decimal i = Convert.ToDecimal(value.ToString());
                    _GiaTriGoiThau = string.Format("{0:0,0}", i);
                }
            }
        }
        public string SoNgayThucHien
        {
            get { return _SoNgayThucHien; }

            set
            {
                if (string.IsNullOrWhiteSpace(value) == true || string.IsNullOrEmpty(value) == true)
                {
                    _SoNgayThucHien = "0";
                }
                else
                {
                    decimal i = Convert.ToDecimal(value.ToString());
                    _SoNgayThucHien = string.Format("{0:0,0}", i);
                }
            }
        }
        public string SoNgayThiCong
        {
            get { return _SoNgayThiCong; }

            set
            {
                if (string.IsNullOrWhiteSpace(value) == true || string.IsNullOrEmpty(value) == true)
                {
                    _SoNgayThiCong = "0";
                }
                else
                {
                    decimal i = Convert.ToDecimal(value.ToString());
                    _SoNgayThiCong = string.Format("{0:0,0}", i);
                }
            }
        }
        public Nullable<int> NamGiaoA { get; set; }
        public string TenDVTH { get; set; }

        public string TenHTHD { get; set; }
        public string NoiDung { get; set; }
        public Nullable<System.Guid> NguoiTheoDoi { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }

    }

}
