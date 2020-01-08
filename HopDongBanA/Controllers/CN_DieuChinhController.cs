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
using System.Data.SqlClient;
using System.Linq.Dynamic;

namespace HopDongMgr.Controllers
{
    public class CN_DieuChinhController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();
        private Common _common = new Common();
        private string err1 = "";
        private string ChucNang = "Chức năng điều chỉnh hợp đồng";

        // GET: CN_DieuChinh
        [CustomAuthorization]
        public ActionResult Index()
        {
          
            var cN_DieuChinh = db.CN_DieuChinh.Include(c => c.CN_HopDong);
            ViewBag.StrorePro_GetThongTinHopDong_DotDieuChinh = db.GetInfoDieuChinh().ToList();
            return View(cN_DieuChinh.ToList());
        }

        // GET: CN_DieuChinh/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CN_DieuChinh cN_DieuChinh = db.CN_DieuChinh.Find(id);
            if (cN_DieuChinh == null)
            {
                return HttpNotFound();
            }
            return View(cN_DieuChinh);
        }

        // GET: CN_DieuChinh/Create
        [CustomAuthorization]
        public ActionResult Create(FormCollection fColection)
        {
            int IDHD;
            bool check = int.TryParse((string)fColection["IDHD_TL"], out IDHD);
            if (check)
            {
                var itemIds = db.CN_DieuChinh.Select(x => x.IDHD).ToArray(); // lấy ID HD diểu chỉnh
                List<CN_HopDong> o_CN_HopDong = db.CN_HopDong.Where(x => !itemIds.Contains(x.IDHD)).ToList(); // lấy danh sách hop dong diêu chỉnh
                ViewBag.IDHD = new SelectList(o_CN_HopDong.Where(o => o.IDHD == IDHD), "IDHD", "SoHopDong", IDHD);
            }
            else
            {

                var itemIds = db.CN_DieuChinh.Select(x => x.IDHD).ToArray();
                //List<CN_HopDong> o_CN_HopDong = db.CN_HopDong.Where(x => !itemIds.Contains(x.IDHD)).ToList();
                var o_CN_HopDong = db.CN_HopDong.Where(x => !itemIds.Contains(x.IDHD))
                  .Select(s => new SelectListItem
                  {
                      Value = s.IDHD.ToString(),
                      Text = s.SoHopDong
                  });
                var NgayKyHD = db.CN_HopDong.Where(x => !itemIds.Contains(x.IDHD))
                  .Select(s => new SelectListItem
                  {
                      Value = s.IDHD.ToString(),
                      Text = s.NgayHetHan.ToString()
                  });
                ViewBag.DSHD = db.CN_HopDong.Where(x => !itemIds.Contains(x.IDHD))
                  .Select(s => new HopDongDC
                  {
                      IDHD = s.IDHD.ToString(),
                      GiaTriThucTe = s.GiaTriThucTe.HasValue == false || s.GiaTriThucTe.Value == 0? s.GiaTriHopDong.ToString() : s.GiaTriThucTe.ToString(),
                      NgayHetHan = s.NgayHetHan.ToString()              
                  }).ToList();
                ViewBag.IDHD = new SelectList(o_CN_HopDong, "Value", "Text");
                ViewBag.NgayKyHD = new SelectList(NgayKyHD, "Value", "Text");

                ViewBag.IDLoaiDieuChinh = DanhSachLoaiDieuChinh();

            }

            return View();
        }

        // POST: CN_DieuChinh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "IDDC,IDHD,NgayDieuChinh,GiaTriDieuChinh,SoNgayGiaHanTienDo,LyDoDieuChinh,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat,NgayHetHanDC,IDLoaiDieuChinh,ChenhLech")] CN_DieuChinh cN_DieuChinh)
        public ActionResult Create( CN_DieuChinh cN_DieuChinh)
        {
            //ViewBag.IDHD = new SelectList(db.CN_HopDong, "IDHD", "SoHopDong", cN_DieuChinh.IDHD);
            //var itemIds = db.CN_DieuChinh.Select(x => x.IDHD).ToArray();
            //ViewBag.DotDieuChinhNew = cN_DieuChinh.DotDieuChinh + 1;
            //ViewBag.IDHD = new SelectList(db.CN_HopDong.Where(o => o.IDHD == cN_DieuChinh.IDHD), "IDHD", "SoHopDong", cN_DieuChinh.IDHD);
            //ViewBag.DotDieuChinh = cN_DieuChinh.DotDieuChinh;
            //ViewBag.ListDotDieuChinh = db.CN_DieuChinh.Where(o => o.IDHD == cN_DieuChinh.IDHD).OrderByDescending(o => o.IDDC).ToList();
            //ViewBag.HopDongDC = db.CN_HopDong.Where(o => o.IDHD == cN_DieuChinh.IDHD).FirstOrDefault();
            //ViewBag.IDLoaiDieuChinh = new SelectList(db.DM_LoaiDieuChinh.Where(x => x.Khoa != false).Select(x => new SelectListItem { Value = x.IDLoaiDieuChinh.ToString(), Text = x.TenLoaiDieuChinh }), "Value", "Text");
            //ViewBag.NgayKyHD = new SelectList(db.CN_HopDong.Where(x => !itemIds.Contains(x.IDHD)).Select(s => new SelectListItem { Value = s.IDHD.ToString(), Text = s.NgayHetHan.ToString() }));
            //ViewBag.DSHD = db.CN_HopDong.Where(x => !itemIds.Contains(x.IDHD)).Select(s => new HopDongDC{
            //                                                          IDHD = s.IDHD.ToString(),
            //                                                          GiaTriThucTe = s.GiaTriThucTe.HasValue == true ? s.GiaTriThucTe.ToString() : s.GiaTriHopDong.ToString(),
            //                                                          ngayKy = s.NgayKy.ToString()
            //                                                      }).ToList();
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                CN_HopDong hopDong = db.CN_HopDong.Find(cN_DieuChinh.IDHD);
                var iTienDoSauDC = db.GetTienDo_SauDieuChinh(cN_DieuChinh.IDHD, cN_DieuChinh.DotDieuChinh, cN_DieuChinh.SoNgayGiaHanTienDo).SingleOrDefault();
                if (iTienDoSauDC.SoNgayThiCong > iTienDoSauDC.SoNgayThucHien) ModelState.AddModelError("SoNgayThiCong", $"Tiến độ thi công” không được lớn hơn “Thời gian thực hiện hợp đồng { iTienDoSauDC.SoNgayThucHien?.ToString("0:##0")}");
                if (ModelState.IsValid)
                {
                    List<SelectListItem> lThongTin = _common.getThongTinBang();
                    #region tham số
                    SqlParameter[] AParameter = {
                                    new SqlParameter
                                    {
                                        ParameterName = "@IDDC",
                                        Value = cN_DieuChinh.IDDC,
                                        DbType = DbType.Int32
                                    },
                                    new SqlParameter
                                    {
                                        ParameterName = "@IDHD",
                                        Value = cN_DieuChinh.IDHD,
                                        DbType = DbType.Int32
                                    },
                                    new SqlParameter
                                    {
                                        ParameterName = "@NgayDieuChinh",
                                        Value = cN_DieuChinh.NgayDieuChinh,
                                        DbType = DbType.Date
                                    },
                                    new SqlParameter
                                    {
                                        ParameterName = "@GiaTriDieuChinh",
                                        Value = cN_DieuChinh.GiaTriDieuChinh,
                                        DbType = DbType.Decimal,
                                        Precision = 18,
                                        Scale = 3
                                    },
                                    new SqlParameter
                                    {
                                        ParameterName = "@ChenhLech",
                                        Value = cN_DieuChinh.ChenhLech,
                                        DbType = DbType.Decimal,
                                        Precision = 18,
                                        Scale = 3
                                    },
                                    new SqlParameter
                                    {
                                        ParameterName = "@SoNgayGiaHanTienDo",
                                        Value = cN_DieuChinh.SoNgayGiaHanTienDo,
                                        DbType = DbType.Int32,
                                    },
                                    new SqlParameter
                                    {
                                        ParameterName = "@LyDoDieuChinh",
                                        Value = cN_DieuChinh.LyDoDieuChinh,
                                        DbType = DbType.String
                                    },
                                    new SqlParameter
                                    {
                                        ParameterName = "@NguoiTao",
                                        Value = lThongTin.Where(o => o.Value == "NguoiTao").SingleOrDefault().Text,
                                        DbType = DbType.String
                                    },
                                    new SqlParameter
                                    {
                                        ParameterName = "@NgayTao",
                                        Value = DateTime.Parse(lThongTin.Where(o => o.Value == "NgayTao").SingleOrDefault().Text),
                                    DbType = DbType.Date
                                    },
                                    new SqlParameter
                                    {
                                        ParameterName = "@NguoiCapNhat",
                                        Value = lThongTin.Where(o => o.Value == "NguoiCapNhat").SingleOrDefault().Text,
                                        DbType = DbType.String
                                    },
                                    new SqlParameter
                                    {
                                        ParameterName = "@NgayCapNhat",
                                        Value = DateTime.Parse(lThongTin.Where(o => o.Value == "NgayCapNhat").SingleOrDefault().Text),
                                        DbType = DbType.Date
                                    },
                                    new SqlParameter
                                    {
                                        ParameterName = "@DotDieuChinh",
                                        Value = 1,
                                        DbType = DbType.Int32
                                    },
                                    new SqlParameter
                                    {
                                        ParameterName = "@NgayHetHanDC",
                                        Value = cN_DieuChinh.NgayHetHanDC,
                                        DbType = DbType.Date
                                    },
                                    new SqlParameter
                                    {
                                        ParameterName = "@IDLoaiDieuChinh",
                                        Value = cN_DieuChinh.IDLoaiDieuChinh,
                                        DbType = DbType.Int32
                                    },
                                    new SqlParameter
                                    {
                                        ParameterName = "@FlagCreate",
                                        Value = "1",
                                        DbType = DbType.String
                                    }
            };
                    #endregion

                    db.Database.ExecuteSqlCommand("Insert_CN_DieuChinh_HopDong  @IDDC, @IDHD, @NgayDieuChinh, @GiaTriDieuChinh, @ChenhLech, @SoNgayGiaHanTienDo, @LyDoDieuChinh, @NguoiTao,@NgayTao, @NguoiCapNhat, @NgayCapNhat, @DotDieuChinh, @NgayHetHanDC, @IDLoaiDieuChinh, @FlagCreate", AParameter);
                    #region luu lich su
                    // luu lich su
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                        ChucNang
                        , "CREATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $" Thêm mới điều chỉnh - Số HD {hopDong.SoHopDong} - lần điều chỉnh {cN_DieuChinh.DotDieuChinh}");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    #endregion
                    return RedirectToAction("Index");
                }
                ViewBag.IDLoaiDieuChinh = DanhSachLoaiDieuChinh(cN_DieuChinh.IDLoaiDieuChinh);
                ViewBag.IDHD = DanhSachHopDongDieuChinh(cN_DieuChinh.IDHD);
                return View(cN_DieuChinh);

            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Không ghi được dữ liệu.<br/>Lý do: " + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }

        // GET: CN_DieuChinh/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(int? id ) //id số hop dong
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CN_DieuChinh cN_DieuChinh = db.CN_DieuChinh.Find(id);
            CN_DieuChinh o_cN_DieuChinh = db.CN_DieuChinh.Where(o => o.IDHD == id).OrderByDescending(o => o.IDDC).ToList().Take(1).SingleOrDefault();
            var l_DotDieuChinh = db.CN_DieuChinh.Where(o => o.IDHD == id).OrderByDescending(o => o.IDDC).Select(o => new SelectListItem { Value = o.DotDieuChinh.ToString(), Text = o.DotDieuChinh.ToString() }).ToList();
            SelectListItem oitem = new SelectListItem();
            oitem.Text = "Thêm mới đợt điều chỉnh.";
            oitem.Value = "1";
            l_DotDieuChinh.Add(oitem);
            if (l_DotDieuChinh == null)
            {
                return HttpNotFound();
            }
            ViewBag.DotDieuChinhNew = o_cN_DieuChinh.DotDieuChinh + 1;

            // nối chuổi so hd va cong trình
            var o_CN_HopDong = db.CN_HopDong.Where(o => o.IDHD == id)
                  .Select(s => new SelectListItem
                  {
                      Value = s.IDHD.ToString(),
                      Text = s.SoHopDong 
                  });
            ViewBag.IDHD = new SelectList(o_CN_HopDong, "Value", "Text", id);
            ViewBag.DotDieuChinh = new SelectList(l_DotDieuChinh, "value", "text", o_cN_DieuChinh.DotDieuChinh);
            ViewBag.ListDotDieuChinh = db.CN_DieuChinh.Where(o => o.IDHD == id).OrderByDescending(o => o.IDDC).ToList();
            ViewBag.HopDongDC = db.CN_HopDong.Where(o => o.IDHD == id).FirstOrDefault();
            ViewBag.IDLoaiDieuChinh = new SelectList(db.DM_LoaiDieuChinh.Where(x => x.Khoa != false).Select(x => new SelectListItem { Value = x.IDLoaiDieuChinh.ToString(), Text = x.TenLoaiDieuChinh }), "Value", "Text");
            var HopDongDieuChinh = db.CN_HopDong.Where(o => o.IDHD == id).Select(s => new
            {
                s.IDHD,
                s.DM_CongTrinh.MaCT,
                s.DM_CongTrinh.TenCT,
                GiaTriThucTe = s.GiaTriThucTe ?? s.GiaTriHopDong,
                s.NgayKy,
                s.NgayHetHan
            }).FirstOrDefault() ;
            ViewBag.HopDongDieuChinh =  Newtonsoft.Json.JsonConvert.SerializeObject(HopDongDieuChinh);
            CN_DieuChinh cN_DieuChinhNull = new CN_DieuChinh();
            return View(cN_DieuChinhNull);//o_cN_DieuChinh
        }

        // POST: CN_DieuChinh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDDC,IDHD,NgayDieuChinh,GiaTriDieuChinh,SoNgayGiaHanTienDo,LyDoDieuChinh,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat,DotDieuChinh,NgayHetHanDC,IDLoaiDieuChinh,ChenhLech")] CN_DieuChinh cN_DieuChinh, FormCollection _FormCollection)
        {
            CN_HopDong hopDong = db.CN_HopDong.Find(cN_DieuChinh.IDHD);
            if (ModelState.IsValid)
            {
                string FlagCreate = _FormCollection["FlagCreate"]; //0: cap nhat 1: them moi
                List<SelectListItem> lThongTin = _common.getThongTinBang();

                string err = "";
                if (cN_DieuChinh.NgayDieuChinh.HasValue == false)
                {
                    TempData["err"] = "< div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng chọn ngày điều chỉnh</div> ";
                }
                var iTienDoSauDC = db.GetTienDo_SauDieuChinh(cN_DieuChinh.IDHD, cN_DieuChinh.DotDieuChinh, cN_DieuChinh.SoNgayGiaHanTienDo).SingleOrDefault();
                if (iTienDoSauDC.SoNgayThiCong > iTienDoSauDC.SoNgayThucHien)
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>“Tiến độ thi công” không được lớn hơn “Thời gian thực hiện hợp đồng”</div> ";
                    ViewBag.DotDieuChinhNew = cN_DieuChinh.DotDieuChinh + 1;
                    ViewBag.IDHD = new SelectList(db.CN_HopDong.Where(o => o.IDHD == cN_DieuChinh.IDHD), "IDHD", "SoHopDong", cN_DieuChinh.IDHD);
                    ViewBag.DotDieuChinh = cN_DieuChinh.DotDieuChinh;
                    ViewBag.ListDotDieuChinh = db.CN_DieuChinh.Where(o => o.IDHD == cN_DieuChinh.IDHD).OrderByDescending(o => o.IDDC).ToList();
                    ViewBag.HopDongDC = db.CN_HopDong.Where(o => o.IDHD == cN_DieuChinh.IDHD).FirstOrDefault();
                    ViewBag.IDLoaiDieuChinh = new SelectList(db.DM_LoaiDieuChinh.Where(x => x.Khoa != false).Select(x => new SelectListItem { Value = x.IDLoaiDieuChinh.ToString(), Text = x.TenLoaiDieuChinh }), "Value", "Text");
                    ViewBag.DSHD = db.CN_HopDong.Where(o => o.IDHD == cN_DieuChinh.IDHD).Select(s => new HopDongDC
                    {
                        IDHD = s.IDHD.ToString(),
                        GiaTriThucTe = s.GiaTriThucTe.HasValue == true ? s.GiaTriThucTe.ToString() : s.GiaTriHopDong.ToString(),
                        ngayKy = s.NgayKy.ToString()
                    }).FirstOrDefault();
                    return View(cN_DieuChinh);
                }
                #region
                //if (FlagCreate == "1")
                //{
                //    CN_DieuChinh oNew = new CN_DieuChinh();
                //    oNew.DotDieuChinh = cN_DieuChinh.DotDieuChinh;
                //    oNew.GiaTriDieuChinh = cN_DieuChinh.GiaTriDieuChinh;
                //    oNew.IDHD = cN_DieuChinh.IDHD;
                //    oNew.LyDoDieuChinh = cN_DieuChinh.LyDoDieuChinh;
                //    oNew.NgayDieuChinh = cN_DieuChinh.NgayDieuChinh;
                //    oNew.NgayTao = DateTime.Parse(lThongTin.Where(o => o.Value == "NgayTao").SingleOrDefault().Text);
                //    oNew.NguoiTao = lThongTin.Where(o => o.Value == "NguoiTao").SingleOrDefault().Text;
                //    oNew.SoNgayGiaHanTienDo = cN_DieuChinh.SoNgayGiaHanTienDo;
                //    oNew.NgayHetHanDC = cN_DieuChinh.NgayHetHanDC;
                //    oNew.IDLoaiDieuChinh = cN_DieuChinh.IDLoaiDieuChinh;
                //    oNew.ChenhLech = cN_DieuChinh.ChenhLech;
                //    db.CN_DieuChinh.Add(oNew);
                //    db.SaveChanges();

                //}
                //else
                //{
                //    List<SelectListItem> list = _common.getThongTinBang();
                //    cN_DieuChinh.NguoiCapNhat = list.Where(o => o.Value == "NguoiCapNhat").SingleOrDefault().Text;
                //    cN_DieuChinh.NgayCapNhat = DateTime.Parse(list.Where(o => o.Value == "NgayCapNhat").SingleOrDefault().Text);
                //    db.Entry(cN_DieuChinh).State = EntityState.Modified;
                //    db.SaveChanges();
                //}
                #endregion
                #region tham số
                SqlParameter[] AParameter = {
                                        new SqlParameter
                                        {
                                         ParameterName = "@IDDC",
                                         Value = cN_DieuChinh.IDDC,
                                         DbType = DbType.Int32
                                        },
                                        new SqlParameter
                                        {
                                         ParameterName = "@IDHD",
                                         Value = cN_DieuChinh.IDHD,
                                         DbType = DbType.Int32
                                        },
                                        new SqlParameter
                                        {
                                         ParameterName = "@NgayDieuChinh",
                                         Value = cN_DieuChinh.NgayDieuChinh,
                                         DbType = DbType.Date
                                        },
                                        new SqlParameter
                                        {
                                         ParameterName = "@GiaTriDieuChinh",
                                         Value = cN_DieuChinh.GiaTriDieuChinh,
                                         DbType = DbType.Decimal,
                                         Precision = 18,
                                         Scale = 3,
                                         IsNullable = true
                                        },
                                        new SqlParameter
                                        {
                                         ParameterName = "@ChenhLech",
                                         Value = cN_DieuChinh.ChenhLech,
                                         DbType = DbType.Decimal,
                                         Precision = 18,
                                         Scale = 3
                                        },
                                        new SqlParameter
                                        {
                                         ParameterName = "@SoNgayGiaHanTienDo",
                                         Value = cN_DieuChinh.SoNgayGiaHanTienDo,
                                         DbType = DbType.Int32,
                                        },
                                        new SqlParameter
                                        {
                                         ParameterName = "@LyDoDieuChinh",
                                         Value = cN_DieuChinh.LyDoDieuChinh ?? string.Empty,
                                        //DbType = DbType.String,
                                         IsNullable = true,
                                         SqlDbType = SqlDbType.NVarChar,
                                         Size = 255
                                         
                                        },
                                        new SqlParameter
                                        {
                                         ParameterName = "@NguoiTao",
                                         Value = lThongTin.Where(o => o.Value == "NguoiTao").SingleOrDefault().Text,
                                         DbType = DbType.String
                                        },
                                        new SqlParameter
                                        {
                                         ParameterName = "@NgayTao",
                                         Value = DateTime.Parse(lThongTin.Where(o => o.Value == "NgayTao").SingleOrDefault().Text),
                                        DbType = DbType.Date
                                        },
                                        new SqlParameter
                                        {
                                         ParameterName = "@NguoiCapNhat",
                                         Value = lThongTin.Where(o => o.Value == "NguoiCapNhat").SingleOrDefault().Text,
                                         DbType = DbType.String
                                        },
                                        new SqlParameter
                                        {
                                         ParameterName = "@NgayCapNhat",
                                         Value = DateTime.Parse(lThongTin.Where(o => o.Value == "NgayCapNhat").SingleOrDefault().Text),
                                         DbType = DbType.Date
                                        },
                                        new SqlParameter
                                        {
                                         ParameterName = "@DotDieuChinh",
                                         Value = cN_DieuChinh.DotDieuChinh,
                                         DbType = DbType.Int32
                                        },
                                        new SqlParameter
                                        {
                                         ParameterName = "@NgayHetHanDC",
                                         Value = cN_DieuChinh.NgayHetHanDC,
                                         DbType = DbType.Date
                                        },
                                        new SqlParameter
                                        {
                                         ParameterName = "@IDLoaiDieuChinh",
                                         Value = cN_DieuChinh.IDLoaiDieuChinh,
                                         DbType = DbType.Int32
                                        },
                                        new SqlParameter
                                        {
                                         ParameterName = "@FlagCreate",
                                         Value = FlagCreate,
                                         DbType = DbType.String
                                        }
                };
                #endregion

                db.Database.ExecuteSqlCommand("Insert_CN_DieuChinh_HopDong  @IDDC, @IDHD, @NgayDieuChinh, @GiaTriDieuChinh, @ChenhLech, @SoNgayGiaHanTienDo, @LyDoDieuChinh, @NguoiTao,@NgayTao, @NguoiCapNhat, @NgayCapNhat, @DotDieuChinh, @NgayHetHanDC, @IDLoaiDieuChinh, @FlagCreate", AParameter);
                #region luu lich su
                // luu lich su
                HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                   ChucNang
                   , "UPDATE"
                   , DateTime.Now, Session["username"]?.ToString()
                   , $"Cập nhật điều chỉnh - Số HD {hopDong?.SoHopDong} - lần điều chỉnh {cN_DieuChinh.DotDieuChinh}");
                db.HT_LichSuHoatDong.Add(ls);
                db.SaveChanges();
                #endregion
                return RedirectToAction("Edit", cN_DieuChinh.IDDC);
            }
            return RedirectToAction("Edit", cN_DieuChinh.IDDC);
        }


        // POST: CN_DieuChinh/Delete/5
        [HttpPost]
        [CustomAuthorization]
        public ActionResult Delete(int id)
        {
            CN_DieuChinh cN_DieuChinh = db.CN_DieuChinh.Find(id);
            CN_HopDong hopDong = db.CN_HopDong.Find(cN_DieuChinh.IDHD);
            //db.CN_DieuChinh.Remove(cN_DieuChinh);
            //db.SaveChanges();
            SqlParameter sParameter = new SqlParameter();
            sParameter.ParameterName= "@IDDC";
            sParameter.Value = id;
            sParameter.SqlDbType = SqlDbType.Int;
            db.Database.ExecuteSqlCommand("Delete_CN_DieuChinh_HopDong @IDDC", sParameter);
            #region luu lich su
            // luu lich su
            HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
            ChucNang
            , "DELETE"
            , DateTime.Now, Session["username"]?.ToString()
            , $" Xóa điều chỉnh - Số HD {hopDong?.SoHopDong} - lần điều chỉnh {cN_DieuChinh.DotDieuChinh}"); db.HT_LichSuHoatDong.Add(ls);
            db.SaveChanges();
            #endregion
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

        /// <summary>
        /// processing-side
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetList()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchVale = Request["search[Value]"];
            string sorColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            List<oGetInfoDieuChinh> list = new List<oGetInfoDieuChinh>();
            list = db.GetInfoDieuChinh().Select(x => new oGetInfoDieuChinh
            {
                DT_RowId = x.IDHD,
                GiatriSauDieuChinh = x.GiatriDieuChinh.ToString(),
                IDHD = x.IDHD,
                MaCT = x.MaCT,
                NgayDieuChinh = x.NgayDieuChinh.ToString(),
                SoHopDong = x.SoHopDong,
                SoNgayGiaHanTienDo = x.SoNgayGiaHanTienDo.ToString(),
                SumGiatriSauDieuChinh = x.SumGiatriSauDieuChinh.ToString(),
                SumSoNgayGiaHanTienDo = x.SumSoNgayGiaHanTienDo.ToString(),
                TenCT = x.TenCT,
                TongDotDC = x.TongDotDC
            }).ToList();
            int TotalRecord = list.Count;
            if (!string.IsNullOrEmpty(searchVale))
            {
                list = list.Where(x => x.MaCT.ToLower().Contains(searchVale.ToLower())
                || x.SoHopDong.ToLower().Contains(searchVale.ToLower())
                ).ToList();
            }
            int totalrowFiter = list.Count;
            list = list.OrderBy(sorColumnName + " " + sortDirection).ToList();
            list = list.Skip(start).Take(length).ToList();

            return Json(new { data = list, draw = Request["draw"], recordsTotal = TotalRecord, recordsFiltered = totalrowFiter }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DongBo_CNDieuChinh()
        {
            string err = "";
            try
            {
                //string sqlQuery = "exec GetDTXD_DanhMucDiaDiem @Check OUTPUT";
                string sqlQuery = "EXEC @return_value = GetDTXD_CNDieuChinh";
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

        [HttpPost]
        public JsonResult LayHDDieuChinh(int IDHD)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var ThongTinHopDong = db.CN_HopDong.Where(p => p.IDHD == IDHD)
                .Select( p => new {
                    IDHD=  p.IDHD,
                    SoNgayThiCong = p.SoNgayThiCong,
                    SoNgayThucHien = p.SoNgayThucHien,
                    NgayKy = p.NgayKy,
                    NgayHetHan=p.NgayHetHan,
                    NgayHetHanThucTe = p.NgayHetHanThucTe,
                    GiaTriGoiThau = p.GiaTriGoiThau,
                    GiaTriHopDong = p.GiaTriHopDong,
                    GiaTriThucTe = p.GiaTriThucTe })
                .FirstOrDefault();
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(ThongTinHopDong);
            return Json(data: result);
        }
        private SelectList DanhSachLoaiDieuChinh(Decimal? selectedValue = -1)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var items = db.DM_LoaiDieuChinh.Where(x => x.Khoa != false)
                    .Select( x => new { x.IDLoaiDieuChinh, ThongTin = x.TenLoaiDieuChinh }).ToList();
            var result = new SelectList(items, "IDLoaiDieuChinh", "ThongTin", selectedValue: selectedValue);
            return result;
        }
        private SelectList DanhSachHopDongDieuChinh(int? selectedValue = -1)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var DSHopdongDieuChinh = db.CN_DieuChinh.Select(x => x.IDHD).ToArray();
            var items = db.CN_HopDong.Where(x => !DSHopdongDieuChinh.Contains(x.IDHD))
              .Select(s => new 
                  {
                      s.IDHD,
                      ThongTin = s.SoHopDong
                  }).ToList();
            items.Insert(0, new { IDHD = -1, ThongTin = "------ Chọn hợp đồng ------ " });
            var result = new SelectList(items, "IDHD", "ThongTin", selectedValue: selectedValue);
            return result;
        }
    }
    //dddd
//    ViewBag.IDHD = new SelectList(db.CN_HopDong, "IDHD", "SoHopDong", cN_DieuChinh.IDHD);
//    var itemIds = db.CN_DieuChinh.Select(x => x.IDHD).ToArray();
//    ViewBag.DotDieuChinhNew = cN_DieuChinh.DotDieuChinh + 1;
//            ViewBag.IDHD = new SelectList(db.CN_HopDong.Where(o => o.IDHD == cN_DieuChinh.IDHD), "IDHD", "SoHopDong", cN_DieuChinh.IDHD);
//            ViewBag.DotDieuChinh = cN_DieuChinh.DotDieuChinh;
//            ViewBag.ListDotDieuChinh = db.CN_DieuChinh.Where(o => o.IDHD == cN_DieuChinh.IDHD).OrderByDescending(o => o.IDDC).ToList();
//      ViewBag.HopDongDC = db.CN_HopDong.Where(o => o.IDHD == cN_DieuChinh.IDHD).FirstOrDefault();
//    ViewBag.IDLoaiDieuChinh = new SelectList(db.DM_LoaiDieuChinh.Where(x => x.Khoa != false).Select(x => new SelectListItem { Value = x.IDLoaiDieuChinh.ToString(), Text = x.TenLoaiDieuChinh
//}), "Value", "Text");
//            ViewBag.NgayKyHD = new SelectList(db.CN_HopDong.Where(x => !itemIds.Contains(x.IDHD)).Select(s => new SelectListItem { Value = s.IDHD.ToString(), Text = s.NgayHetHan.ToString() }));
//            ViewBag.DSHD = db.CN_HopDong.Where(x => !itemIds.Contains(x.IDHD)).Select(s => new HopDongDC{
//                                                                      IDHD = s.IDHD.ToString(),
//                                                                      GiaTriThucTe = s.GiaTriThucTe.HasValue == true ? s.GiaTriThucTe.ToString() : s.GiaTriHopDong.ToString(),
//                                                                      ngayKy = s.NgayKy.ToString()
//                                                                  }).ToList();
//    ddd
public partial class oGetInfoDieuChinh
    {
        private string _NgayDieuChinh;
        private string _GiatriSauDieuChinh;
        private string _SoNgayGiaHanTienDo;
        private string _SumGiatriSauDieuChinh;
        private string _SumSoNgayGiaHanTienDo;

        public Nullable<int> DT_RowId { get; set; }
        public Nullable<int> IDHD { get; set; }
        public string NgayDieuChinh {
            get { return _NgayDieuChinh; }

            set
            {
                string[] A = new string[] { };
                if (value.Contains("/"))
                    _NgayDieuChinh = value.Substring(0, 10);
                else if (value.Contains("-"))
                {
                    A = value.Split('-');
                    _NgayDieuChinh = A[2] + "/" + A[1] + "/" + A[0];
                }
                else
                    _NgayDieuChinh = value.Substring(0, 10);

            }
        }
        //public Nullable<decimal> GiatriSauDieuChinh { get; set; }
        public string GiatriSauDieuChinh {
            get { return _GiatriSauDieuChinh; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) == true || string.IsNullOrEmpty(value) == true)
                {
                    _GiatriSauDieuChinh = "0";
                }
                else
                {
                    decimal i = Convert.ToDecimal(value.ToString());
                    _GiatriSauDieuChinh = string.Format("{0:0,0}", i);
                }
            }
        }
        //public Nullable<int> SoNgayGiaHanTienDo { get; set; }
        public string SoNgayGiaHanTienDo {
            get { return _SoNgayGiaHanTienDo; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) == true || string.IsNullOrEmpty(value) == true)
                {
                    _SoNgayGiaHanTienDo = "0";
                }
                else
                {
                    Int64 i = Convert.ToInt64(value.ToString());
                    _SoNgayGiaHanTienDo = string.Format("{0:0,0}", i);
                }
            }
        }
        public Nullable<int> TongDotDC { get; set; }
        public string SoHopDong { get; set; }
        public string SumGiatriSauDieuChinh
        {
            get { return _SumGiatriSauDieuChinh; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) == true || string.IsNullOrEmpty(value) == true)
                {
                    _SumGiatriSauDieuChinh = "0";
                }
                else
                {
                    decimal i = Convert.ToDecimal(value.ToString());
                    _SumGiatriSauDieuChinh = string.Format("{0:0,0}", i);
                }
            }
        }
        public string SumSoNgayGiaHanTienDo
        {
            get { return _SumSoNgayGiaHanTienDo; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) == true || string.IsNullOrEmpty(value) == true)
                {
                    _SumSoNgayGiaHanTienDo = "0";
                }
                else
                {
                    Int64 i = Convert.ToInt64(value.ToString());
                    _SumSoNgayGiaHanTienDo = string.Format("{0:0,0}", i);
                }
            }
        }
        public string MaCT { get; set; }
        public string TenCT { get; set; }
    }
}
