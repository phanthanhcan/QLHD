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

namespace HopDongMgr.Controllers
{
    public class CN_HopDongController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();
        private Common _common = new Common();

        #region lấy danh sách 
        // GET: CN_HopDong
        [CustomAuthorization]
        public ActionResult Index( int? NamGiaoA, int? NamKyHD, string MaCT, bool? IsHoanThanh)
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
        #endregion
        // GET: CN_HopDong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CN_HopDong cN_HopDong = db.CN_HopDong.Find(id);
            if (cN_HopDong == null)
            {
                return HttpNotFound();
            }
            return View(cN_HopDong);
        }
        

        // GET: CN_HopDong/Create
        [CustomAuthorization]
        public ActionResult Create()
        {

            List<DM_LoaiHopDong> LiQuyenLoaiHD = new List<DM_LoaiHopDong>();
            Guid gIDMaPhongBan = (Guid)Session["IDMaPhongBan"];
            Guid guserid = (Guid)Session["userid"];
            //Load combobox
            //Loai hop dong
            List<HT_PhongBan_LoaiHopDong> LiQuyen = db.HT_PhongBan_LoaiHopDong.Where(o => o.IDPB.CompareTo(gIDMaPhongBan) == 0).ToList();
            List<HT_NguoiDung> LiNguoiDung = db.HT_NguoiDung.Where(o => o.oid.CompareTo(guserid) == 0).ToList();
            DM_LoaiHopDong iLoaiHD = new DM_LoaiHopDong();
            foreach (var item in LiQuyen)
            {
                iLoaiHD = new DM_LoaiHopDong();
                iLoaiHD = db.DM_LoaiHopDong.Find(item.IDLoaiHopDong);
                LiQuyenLoaiHD.Add(iLoaiHD);

            }
            iLoaiHD = new DM_LoaiHopDong();
            iLoaiHD.IDLoai = -1;
            iLoaiHD.TenLoai = "-- Chọn loại hợp đồng --";
            LiQuyenLoaiHD.Insert(0, iLoaiHD);
            ViewBag.IDLoai = new SelectList(LiQuyenLoaiHD.OrderBy(o => o.TenLoai), "IDLoai", "TenLoai");
            //Cong trinh
            List<DM_CongTrinh> liCongTrinh = db.DM_CongTrinh.OrderBy(o => o.TenCT).ToList();
            DM_CongTrinh iCongTrinh = new DM_CongTrinh();
            iCongTrinh.MaCT = "";
            iCongTrinh.TenCT = "-- Chọn công trình --";
            liCongTrinh.Insert(0, iCongTrinh);
            ViewBag.MaCT = new SelectList(liCongTrinh, "IDCT", "TenCT");
            //Don vi thuc hien
            List<DM_DonViThucHien> liDonViThuHien = db.DM_DonViThucHien.Where(o => o.Khoa.Value.CompareTo(false) == 0).OrderBy(o => o.TenDV).ToList();
            DM_DonViThucHien iDonViThucHien = new DM_DonViThucHien();
            iDonViThucHien.IDDV = -1000;
            iDonViThucHien.TenDV = "-- Chọn Đơn vị thực hiện --";
            liDonViThuHien.Insert(0, iDonViThucHien);
            ViewBag.IDDV = new SelectList(liDonViThuHien, "IDDV", "TenDV");
            //Hinh thuc hop dong
            List<DM_HinhThucHopDong> liHinhThuc = db.DM_HinhThucHopDong.Where(o => o.Khoa.Value.CompareTo(false) == 0).OrderBy(o => o.TenHinhThuc).ToList();
            DM_HinhThucHopDong iHinhThuc = new DM_HinhThucHopDong();
            iHinhThuc.IDHT = -1;
            iHinhThuc.TenHinhThuc = "-- Chọn hình thức hợp đồng --";
            liHinhThuc.Insert(0, iHinhThuc);
            ViewBag.IDHT = new SelectList(liHinhThuc, "IDHT", "TenHinhThuc");
            //
            ViewBag.NguoiTao = new SelectList(LiNguoiDung, "oid", "HoTen");

            return View();
        }


        // POST: CN_HopDong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDHD,IDLoai,MaCT,NoiDung,MaDD,NamGiaoA,IDDV,GiaTriGoiThau,IDHT,SoHopDong,NgayKy,GiaTriHopDong,SoNgayThucHien,SoNgayThiCong,NgayTongNghiemThu,XuLyViPham,GiaTriPhat,XuLyTranhChap,IsHoanThanh,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat,NgayHetHan,IDCT")] CN_HopDong cN_HopDong)
        {
            ViewBag.IDCT = new SelectList(db.DM_CongTrinh, "IDCT", "TenCT", cN_HopDong.IDCT);
            ViewBag.IDDV = new SelectList(db.DM_DonViThucHien, "IDDV", "TenDV", cN_HopDong.IDDV);
            ViewBag.IDHT = new SelectList(db.DM_HinhThucHopDong, "IDHT", "TenHinhThuc", cN_HopDong.IDHT);
            ViewBag.NguoiTao = new SelectList(db.HT_NguoiDung, "oid", "MaNguoiDung", cN_HopDong.NguoiTao);
            ViewBag.IDLoai = new SelectList(db.DM_LoaiHopDong, "IDLoai", "TenLoai", cN_HopDong.IDLoai);
            if (ModelState.IsValid)
            {
                //Check data
                if (db.CN_HopDong.Where(X => X.SoHopDong.ToLower().Trim().Contains(cN_HopDong.SoHopDong.ToLower().Trim())).Count() > 0)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Số Hợp đồng đã tồn tại. Vui lòng kiểm tra lại</div> ";
                    //return RedirectToAction("Create", "CN_HopDong");
                    return View(cN_HopDong);
                }
                if (cN_HopDong.IDLoai.Value == -1)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng chọn loại hợp đồng</div> ";
                    return View(cN_HopDong);
                }
                if (string.IsNullOrEmpty(cN_HopDong.SoHopDong))
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng nhập số hợp đồng</div> ";
                    return View(cN_HopDong);
                }
                if (cN_HopDong.IDCT.HasValue == false)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng chọn công trình</div> ";
                    return View(cN_HopDong);
                }
                if (cN_HopDong.IDHT.Value == -1)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng chọn hình thức hợp đồng</div> ";
                    return View(cN_HopDong);
                }
                if (cN_HopDong.GiaTriGoiThau.HasValue == false || cN_HopDong.GiaTriGoiThau.GetValueOrDefault(0) <= 0)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng nhập giá trị gói thầu</div> ";
                    return View(cN_HopDong);
                }
                if (cN_HopDong.GiaTriHopDong.HasValue == false || cN_HopDong.GiaTriHopDong.GetValueOrDefault(0) <= 0)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng nhập giá trị hợp đồng</div> ";
                    return View(cN_HopDong);
                }
                else
                {
                    if (cN_HopDong.GiaTriHopDong.Value> cN_HopDong.GiaTriGoiThau.Value)
                    {
                        TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>o	“Giá trị hợp đồng” không được lớn hơn “Giá gói thầu”</div> ";
                        return View(cN_HopDong);
                    }
                }
                if (cN_HopDong.SoNgayThucHien.HasValue == false || cN_HopDong.SoNgayThucHien.GetValueOrDefault(0) <= 0)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng nhập thời gian thực hiện hợp đồng</div> ";
                    return View(cN_HopDong);
                }
                if (cN_HopDong.SoNgayThiCong.HasValue == false || cN_HopDong.SoNgayThiCong.GetValueOrDefault(0) <=0)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng nhập tiến độ thi công</div> ";
                    return View(cN_HopDong);
                }
                if (cN_HopDong.SoNgayThiCong.Value > cN_HopDong.SoNgayThucHien.Value)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>“Tiến độ thi công” không được lớn hơn “Thời gian thực hiện hợp đồng”</div> ";
                    return View(cN_HopDong);
                }
                if(cN_HopDong.IDDV < -999)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng nhập tên Đơn vị thực hiện (Chọn \"Không xác định\" nếu để trống)</div> ";
                    return View(cN_HopDong);
                }
                //
                List<SelectListItem> list = _common.getThongTinBang();
                cN_HopDong.NguoiTao = Guid.Parse(list.Where(o => o.Value == "NguoiTao").SingleOrDefault().Text);
                cN_HopDong.NgayTao = DateTime.Parse(list.Where(o => o.Value == "NgayTao").SingleOrDefault().Text);
                db.CN_HopDong.Add(cN_HopDong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cN_HopDong);
        }


        // GET: CN_HopDong/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CN_HopDong cN_HopDong = db.CN_HopDong.Find(id);
            if (cN_HopDong == null)
            {
                return HttpNotFound();
            }
            List<DM_LoaiHopDong> LiQuyenLoaiHD = new List<DM_LoaiHopDong>();
            Guid gIDMaPhongBan = (Guid)Session["IDMaPhongBan"];
            Guid guserid = (Guid)Session["userid"];
            List<HT_PhongBan_LoaiHopDong> LiQuyen = db.HT_PhongBan_LoaiHopDong.Where(o => o.IDPB.CompareTo(gIDMaPhongBan) == 0).ToList();
            foreach (var item in LiQuyen)
            {
                DM_LoaiHopDong o = db.DM_LoaiHopDong.Find(item.IDLoaiHopDong);
                LiQuyenLoaiHD.Add(o);

            }
            List<HT_NguoiDung> LiNguoiDung = db.HT_NguoiDung.Where(o => o.oid.CompareTo(guserid) == 0).ToList();
            ViewBag.MaCT = new SelectList(db.DM_CongTrinh.OrderBy(o => o.TenCT), "IDCT", "TenCT", cN_HopDong.IDCT);

            ViewBag.IDDV = new SelectList(db.DM_DonViThucHien.OrderBy(o => o.TenDV), "IDDV", "TenDV", cN_HopDong.IDDV);
            ViewBag.IDHT = new SelectList(db.DM_HinhThucHopDong.OrderBy(o => o.TenHinhThuc), "IDHT", "TenHinhThuc", cN_HopDong.IDHT);
            ViewBag.NguoiTao = new SelectList(LiNguoiDung, "oid", "HoTen", cN_HopDong.NguoiTao);
            ViewBag.IDLoai = new SelectList(LiQuyenLoaiHD.OrderBy(o => o.TenLoai), "IDLoai", "TenLoai", cN_HopDong.IDLoai);
            return View(cN_HopDong);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDHD,IDLoai,MaCT,NoiDung,MaDD,NamGiaoA,IDDV,GiaTriGoiThau,IDHT,SoHopDong,NgayKy,GiaTriHopDong,SoNgayThucHien,SoNgayThiCong,NgayTongNghiemThu,XuLyViPham,GiaTriPhat,XuLyTranhChap,IsHoanThanh,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat, NgayHetHan,IDCT")] CN_HopDong cN_HopDong)
        {
            ViewBag.MaCT = new SelectList(db.DM_CongTrinh, "IDCT", "TenCT", cN_HopDong.MaCT);
            ViewBag.IDDV = new SelectList(db.DM_DonViThucHien, "IDDV", "TenDV", cN_HopDong.IDDV);
            ViewBag.IDHT = new SelectList(db.DM_HinhThucHopDong, "IDHT", "TenHinhThuc", cN_HopDong.IDHT);
            ViewBag.NguoiTao = new SelectList(db.HT_NguoiDung, "oid", "MaNguoiDung", cN_HopDong.NguoiTao);
            ViewBag.IDLoai = new SelectList(db.DM_LoaiHopDong, "IDLoai", "TenLoai", cN_HopDong.IDLoai);
            ViewBag.MaCT = new SelectList(db.DM_CongTrinh.OrderBy(o => o.TenCT), "IDCT", "TenCT", cN_HopDong.IDCT);
            if (ModelState.IsValid)
            {
                //Check data
                //Check data
                if (db.CN_HopDong.Where(X => X.SoHopDong.ToLower().Trim().Contains(cN_HopDong.SoHopDong.ToLower().Trim()) && X.IDHD != cN_HopDong.IDHD).Count() > 0)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Số Hợp đồng đã tồn tại. Vui lòng kiểm tra lại</div> ";
                    return View(cN_HopDong);
                }
                if (cN_HopDong.IDLoai.Value == -1)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng chọn loại hợp đồng</div> ";
                    return View(cN_HopDong);
                }
                if (string.IsNullOrEmpty(cN_HopDong.SoHopDong))
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng nhập số hợp đồng</div> ";
                    return View(cN_HopDong);
                }
                if (cN_HopDong.IDCT.HasValue == false)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng chọn công trình</div> ";
                    return View(cN_HopDong);
                }
                if (cN_HopDong.IDHT.Value == -1)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng chọn hình thức hợp đồng</div> ";
                    return View(cN_HopDong);
                }
                if (cN_HopDong.GiaTriGoiThau.HasValue == false || cN_HopDong.GiaTriGoiThau.GetValueOrDefault(0) <= 0)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng nhập giá trị gói thầu</div> ";
                    return View(cN_HopDong);
                }
                if (cN_HopDong.GiaTriHopDong.HasValue == false || cN_HopDong.GiaTriHopDong.GetValueOrDefault(0) <= 0)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng nhập giá trị hợp đồng</div> ";
                    return View(cN_HopDong);
                }
                else
                {
                    if (cN_HopDong.GiaTriHopDong.Value > cN_HopDong.GiaTriGoiThau.Value)
                    {
                        TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>o	“Giá trị hợp đồng” không được lớn hơn “Giá gói thầu”</div> ";
                        return View(cN_HopDong);
                    }
                }
                if (cN_HopDong.SoNgayThucHien.HasValue == false || cN_HopDong.SoNgayThucHien.GetValueOrDefault(0) <= 0)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng nhập thời gian thực hiện hợp đồng</div> ";
                    return View(cN_HopDong);
                }
                if (cN_HopDong.SoNgayThiCong.HasValue == false || cN_HopDong.SoNgayThiCong.GetValueOrDefault(0) <= 0)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng nhập tiến độ thi công</div> ";
                    return View(cN_HopDong);
                }
                if (cN_HopDong.SoNgayThiCong.Value > cN_HopDong.SoNgayThucHien.Value)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>“Tiến độ thi công” không được lớn hơn “Thời gian thực hiện hợp đồng”</div> ";
                    return View(cN_HopDong);
                }
                //
                List<SelectListItem> list = _common.getThongTinBang();
                cN_HopDong.NguoiCapNhat = list.Where(o => o.Value == "NguoiTao").SingleOrDefault().Text;
                cN_HopDong.NgayCapNhat = DateTime.Parse(list.Where(o => o.Value == "NgayTao").SingleOrDefault().Text);

                //db.Entry(cN_HopDong).State = EntityState.Modified;
                db.CN_HopDong.Attach(cN_HopDong);
                db.Entry(cN_HopDong).Property(o => o.IDLoai).IsModified = true;
                db.Entry(cN_HopDong).Property(o => o.SoHopDong).IsModified = true;
                db.Entry(cN_HopDong).Property(o => o.NgayKy).IsModified = true;
                db.Entry(cN_HopDong).Property(o => o.NoiDung).IsModified = true;
                db.Entry(cN_HopDong).Property(o => o.IDCT).IsModified = true;
                db.Entry(cN_HopDong).Property(o => o.IDDV).IsModified = true;
                db.Entry(cN_HopDong).Property(o => o.IDHT).IsModified = true;
                db.Entry(cN_HopDong).Property(o => o.NamGiaoA).IsModified = true;
                db.Entry(cN_HopDong).Property(o => o.IDCT).IsModified = true;
                db.Entry(cN_HopDong).Property(o => o.GiaTriHopDong).IsModified = true;
                db.Entry(cN_HopDong).Property(o => o.GiaTriGoiThau).IsModified = true;
                db.Entry(cN_HopDong).Property(o => o.SoNgayThucHien).IsModified = true;
                db.Entry(cN_HopDong).Property(o => o.NgayHetHan).IsModified = true;
                db.Entry(cN_HopDong).Property(o => o.NguoiCapNhat).IsModified = true;
                db.Entry(cN_HopDong).Property(o => o.NgayCapNhat).IsModified = true;
                db.SaveChanges();
                TempData["err"] = "<div class='alert alert-success' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span> Cập nhật thành công</div> ";
                return RedirectToAction("Index");
            }
            return View(cN_HopDong);
        }


        public ActionResult Edit_XuPhatTranhChap(int? IDHD)
        {
            if (IDHD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CN_HopDong cN_HopDong = db.CN_HopDong.Find(IDHD);
            if (cN_HopDong == null)
            {
                return HttpNotFound();
            }
            return View(cN_HopDong);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_XuPhatTranhChap([Bind(Include = "IDHD,IDLoai,MaCT,NoiDung,MaDD,NamGiaoA,IDDV,GiaTriGoiThau,IDHT,SoHopDong,NgayKy,GiaTriHopDong,SoNgayThucHien,SoNgayThiCong,NgayTongNghiemThu,XuLyViPham,GiaTriPhat,XuLyTranhChap,IsHoanThanh,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat")] CN_HopDong cN_HopDong)
        {
            string err = "";
            if (ModelState.IsValid)
            {
                if (cN_HopDong.GiaTriPhat.HasValue == false || cN_HopDong.GiaTriPhat.GetValueOrDefault(0) <= 0)
                {
                    err = err + "Vui lòng nhập giá trị phạt";
                }
                if (err.Length > 1)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>" + err + "</div> ";
                    return View(cN_HopDong);
                }
                var Hopdong = new CN_HopDong();
                Hopdong.IDHD = cN_HopDong.IDHD;
                Hopdong.GiaTriPhat = cN_HopDong.GiaTriPhat;
                Hopdong.XuLyTranhChap = cN_HopDong.XuLyTranhChap;
                Hopdong.XuLyViPham = cN_HopDong.XuLyViPham;

                db.CN_HopDong.Attach(Hopdong);
                db.Entry(Hopdong).Property(o => o.GiaTriPhat).IsModified = true;
                db.Entry(Hopdong).Property(o => o.XuLyTranhChap).IsModified = true;
                db.Entry(Hopdong).Property(o => o.XuLyViPham).IsModified = true;

                //db.Entry(cN_HopDong).State = EntityState.Modified;
                db.SaveChanges();
                TempData["err"] = "<div class='alert alert-success' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Cập nhật thành công</div> ";

                return View(cN_HopDong);
            }

            return View(cN_HopDong);
        }
        
        
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
