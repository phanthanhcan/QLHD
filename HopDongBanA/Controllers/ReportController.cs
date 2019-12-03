using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using HopDongMgr.Models;
using HopDongMgr.Class.Common;
using HopDongMgr.DungChung;

namespace HopDongMgr.Controllers
{
    public class ReportController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();
        [CustomAuthorization]
        public ActionResult Report_DanhSachBaoCao()
        {
            return View();
        }
        // GET: Report
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //BM01
        public ActionResult Print(Guid? id, string madv)
        {
            ReportViewer reportViewer = new ReportViewer();
            //reportViewer.ProcessingMode = ProcessingMode.Local;
            //reportViewer.SizeToReportContent = true;
            //reportViewer.Width = Unit.Percentage(1800);
            //reportViewer.Height = Unit.Percentage(1800);
            //reportViewer.ShowPrintButton = true;
            //List<rptBM01_Result> lst = db.rptBM01(id, new Guid()).ToList();

            //DateTime? ngayphathien = lst.First().NgayPhatHien;
            //DM_DONVI a = db.DM_DONVI.Where(b => b.MA_DVIQLY == madv).First();
            //string c = "";
            //if (a.MA_DVIQLY.Length > 4)
            //{
            //    c = a.TEN_DVIQLY;
            //}
            //reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", lst));
            //ReportParameter[] parm = new ReportParameter[3];
            //parm[0] = new ReportParameter("TenTat", a.TenTat);
            //parm[1] = new ReportParameter("NgayPhatHien", string.Format("ngày {0} tháng {1} năm {2}", ngayphathien.Value.Day.ToString("00"), ngayphathien.Value.Month.ToString(), ngayphathien.Value.Year.ToString()));
            //parm[2] = new ReportParameter("TenDienLuc", c.ToUpper());

            ////ViewBag.LstKPH = db.(Session["MaDV"].ToString()).ToList();


            //reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\BM01.rdlc";
            //reportViewer.LocalReport.SetParameters(parm);
            //ViewBag.ReportViewer = reportViewer;

            return PartialView("Print");
        }
        //BM02
        [HttpPost]
        public ActionResult PrintBM02(ParamReport model)
        {
            ReportViewer reportViewer = new ReportViewer();

            //if (model != null)
            //{
            //    reportViewer.ProcessingMode = ProcessingMode.Local;
            //    reportViewer.SizeToReportContent = true;
            //    reportViewer.Width = Unit.Percentage(1800);
            //    reportViewer.Height = Unit.Percentage(1800);
            //    reportViewer.ShowPrintButton = true;
            //    List<rptBM02_Result> lst = db.rptBM02(Convert.ToDateTime(model.TuNgay), Convert.ToDateTime(model.DenNgay), model.MaDonVi).ToList();

            //    //DateTime? ngayphathien = lst.First().NgayPhatHien;
            //    //DM_DONVI a = db.DM_DONVI.Where(b => b.MA_DVIQLY == madv).First();
            //    //string c = "";
            //    //if (a.MA_DVIQLY.Length > 4)
            //    //{
            //    //    c = a.TEN_DVIQLY;
            //    //}
            //    reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", lst));

            //    reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\BM02.rdlc";

            //    ViewBag.ReportViewer = reportViewer;
            //    //ViewBag.Model = new SelectList(db.DM_DONVI, "MA_DVIQLY", "TEN_DVIQLY");
            //}

            return PartialView("Print");
        }
        //BM03

        public ActionResult PrintBM03(Guid? id, string madv)
        {
            ReportViewer reportViewer = new ReportViewer();


            //reportViewer.ProcessingMode = ProcessingMode.Local;
            //reportViewer.SizeToReportContent = true;
            //reportViewer.Width = Unit.Percentage(1800);
            //reportViewer.Height = Unit.Percentage(1800);
            //reportViewer.ShowPrintButton = true;
            //List<rptBM03_Result> lst = db.rptBM03(id).ToList();

            //DM_DONVI a = db.DM_DONVI.Where(b => b.MA_DVIQLY == madv).First();
            
            //string c = "";
            //Guid idchucvu = Guid.Parse("5C8139C0-B3C6-4742-A457-EDCECED1116A"); //giam doc cty
            //if (a.MA_DVIQLY.Length > 4)
            //{
            //    c = a.TEN_DVIQLY;
            //    idchucvu = Guid.Parse("918611C4-E183-46B2-A39B-36C7E33A9D69");
            //}
            //HT_NguoiDung nd = db.HT_NguoiDung
            //    .Where(b=>b.ChucVu==idchucvu)
            //    .Where(d=>d.MaDV==madv).First();

            //reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", lst));
            //ReportParameter[] parm = new ReportParameter[3];
            //parm[0] = new ReportParameter("TenTat", a.TenTat);
            //parm[1] = new ReportParameter("GiamDoc", nd.HoTen);
            //parm[2] = new ReportParameter("TenDienLuc", c.ToUpper());

            //reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\BM03.rdlc";
            //reportViewer.LocalReport.SetParameters(parm);
            //ViewBag.ReportViewer = reportViewer;
            ////ViewBag.Model = new SelectList(db.DM_DONVI, "MA_DVIQLY", "TEN_DVIQLY");


            return PartialView("Print");
        }
        //BM04
        [HttpPost]
        public ActionResult PrintBM04(ParamReport model)
        {
            ReportViewer reportViewer = new ReportViewer();

            //if (model != null)
            //{
            //    reportViewer.ProcessingMode = ProcessingMode.Local;
            //    reportViewer.SizeToReportContent = true;
            //    reportViewer.Width = Unit.Percentage(1800);
            //    reportViewer.Height = Unit.Percentage(1800);
            //    reportViewer.ShowPrintButton = true;
            //    List<rptBM04_Result> lst = db.rptBM04(Convert.ToDateTime(model.TuNgay), Convert.ToDateTime(model.DenNgay), model.MaDonVi).ToList();


            //    reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", lst));

            //    reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\BM04.rdlc";

            //    ViewBag.ReportViewer = reportViewer;
            //    //ViewBag.Model = new SelectList(db.DM_DONVI, "MA_DVIQLY", "TEN_DVIQLY");
            //}

            return PartialView("Print");
        }

        [CustomAuthorization]
        public ActionResult PrintBM01_TheoDoiHopDong_GetByNguonVon_View()
        {
            ViewBag.listDM_NguonVon = db.DM_NguonVon.ToList();

            var dmCongTrinh= db.DM_CongTrinh.ToList();
            DM_CongTrinh item = new DM_CongTrinh();
            item.IDCT = -1;
            item.TenCT = " Chọn tất cả Công trình";
            dmCongTrinh.Insert(0, item);

            ViewBag.listDM_CongTrinh = dmCongTrinh.OrderBy(o => o.TenCT).ToList();
            return View();
        }
        public ActionResult PrintBM01_TheoDoiHopDong_GetByNguonVon(string _MaNV, string _IDCT, int? _TrangThai)
        {
            ReportViewer reportViewer = new ReportViewer();

            if (String.IsNullOrEmpty( _MaNV) == false)
            {
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.SizeToReportContent = true;
                reportViewer.Width = Unit.Percentage(1800);
                reportViewer.Height = Unit.Percentage(1800);
                reportViewer.ShowPrintButton = true;
                List<rptBM01_TheoDoiHopDong_GetByNguonVon_Result> lst = db.rptBM01_TheoDoiHopDong_GetByNguonVon(_MaNV, _IDCT, _TrangThai).ToList();
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("rptBM01_TheoDoiHopDong_GetByNguonVon", lst));
                ReportParameter[] parm = new ReportParameter[1];
                parm[0] = new ReportParameter("pNguonVon", _MaNV);
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\BM01_TheoDoiHopDong_TheoNguonVon.rdlc";
                reportViewer.LocalReport.SetParameters(parm);

                reportViewer.SizeToReportContent = true;
                ViewBag.ReportViewer = reportViewer;
                //ViewBag.Model = new SelectList(db.DM_DONVI, "MA_DVIQLY", "TEN_DVIQLY");
            }

            return PartialView("Print");
        }

        [CustomAuthorization]
        public ActionResult PrintBM01_TheoDoiHopDong_GetByCongTrinh_View()
        {
            var dmCongTrinh = db.DM_CongTrinh.ToList();
            DM_CongTrinh item = new DM_CongTrinh();
            item.IDCT = -1;
            item.TenCT = " Chọn tất cả Công trình";
            dmCongTrinh.Insert(0, item);

            ViewBag.listDM_CongTrinh = dmCongTrinh.OrderBy(o => o.TenCT).ToList();
            return View();
        }
        public ActionResult PrintBM01_TheoDoiHopDong_GetByCongTrinh(string _IDCT, int? _TrangThai, string _TenCongTrinh)
        {
            if (_IDCT == "-1" ) _TenCongTrinh = "";
            ReportViewer reportViewer = new ReportViewer();

            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(1800);
            reportViewer.Height = Unit.Percentage(1800);
            reportViewer.ShowPrintButton = true;
            List<rptBM01_TheoDoiHopDong_GetByCongTrinh_Result> lst = db.rptBM01_TheoDoiHopDong_GetByCongTrinh(_IDCT, _TrangThai).ToList();
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("rptBM01_TheoDoiHopDong_GetByCongTrinh_Result", lst));
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\BM01_TheoDoiHopDong_TheoCongTrinh.rdlc";
            ReportParameter[] parm = new ReportParameter[1];
            parm[0] = new ReportParameter("pTenCongTrinh", _TenCongTrinh.ToUpper());
            reportViewer.LocalReport.SetParameters(parm);
            ViewBag.ReportViewer = reportViewer;
            return PartialView("Print");
        }

        [CustomAuthorization]
        public ActionResult PrintBM01_TheoDoiHopDong_GetByLoaiHopDong_View()
        {

            ViewBag.listDM_LoaiHopDong = db.DM_LoaiHopDong.OrderBy(o => o.STT).ToList();
            return View();
        }
        public ActionResult PrintBM01_TheoDoiHopDong_GetByLoaiHopDong(int? _MaLHD, int? _TrangThai, string _TenLoaiHopDong)
        {
            ReportViewer reportViewer = new ReportViewer();

            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(1800);
            reportViewer.Height = Unit.Percentage(1800);
            reportViewer.ShowPrintButton = true;
            List<rptBM01_TheoDoiHopDong_GetByLoaiHopDong_Result> lst = db.rptBM01_TheoDoiHopDong_GetByLoaiHopDong(_MaLHD, _TrangThai).ToList();
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("rptBM01_TheoDoiHopDong_GetByCongTrinh_Result", lst));
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\BM01_TheoDoiHopDong_TheoLoaiHopDong.rdlc";
            ReportParameter[] parm = new ReportParameter[1];
            parm[0] = new ReportParameter("pTenLoaiHopDong", _TenLoaiHopDong.ToUpper());
            reportViewer.LocalReport.SetParameters(parm);
            ViewBag.ReportViewer = reportViewer;

            return PartialView("Print");
        }

        [CustomAuthorization]
        public ActionResult PrintBM11_TheoDoiHopDong_GetByDiaDiem_View()
        {

            ViewBag.listDM_DiaDiem = db.DM_DiaDiem.OrderBy(o => o.TenDD).ToList();
            return View();
        }
        public ActionResult PrintBM11_TheoDoiHopDong_GetByDiaDiem(string _MaDD, int? _TrangThai, string _TenDD)
        {
            ReportViewer reportViewer = new ReportViewer();

            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(1800);
            reportViewer.Height = Unit.Percentage(1800);
            reportViewer.ShowPrintButton = true;
            List<rptBM11_TheoDoiHopDong_GetByDiaDiem_Result> lst = db.rptBM11_TheoDoiHopDong_GetByDiaDiem(_MaDD, _TrangThai).ToList();
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("rptBM11_TheoDoiHopDong_GetByDiaDiem", lst));

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\BM11_TheoDoiHopDong_TheoDiaDiem.rdlc";
            ReportParameter[] parm = new ReportParameter[1];
            parm[0] = new ReportParameter("pDiaDiem", _TenDD.ToUpper());
            reportViewer.LocalReport.SetParameters(parm);

            ViewBag.ReportViewer = reportViewer;
            return PartialView("Print");
        }


        [CustomAuthorization]
        public ActionResult PrintBM12_TheoDoiHopDong_GetByNamKy_View()
        {
            List<SelectListItem> Li = new List<SelectListItem>();
            for (int i = DateTime.Now.Year - 15; i < DateTime.Now.Year + 5; i++)
            {
                SelectListItem o = new SelectListItem() { Value = i.ToString(), Text = i.ToString() };
                Li.Add(o);
            }

            ViewBag.liNamKy = new SelectList(Li, "Value", "Text", DateTime.Now.Year);
            ViewBag.listDM_DiaDiem = db.DM_DiaDiem.OrderBy(o => o.TenDD).ToList();
            return View();
        }
        public ActionResult PrintBM12_TheoDoiHopDong_GetByNamKy(int _NamKy, int? _TrangThai)
        {
            ReportViewer reportViewer = new ReportViewer();

            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(1800);
            reportViewer.Height = Unit.Percentage(1800);
            reportViewer.ShowPrintButton = true;
            List<rptBM12_TheoDoiHopDong_GetByNamKy_Result> lst = db.rptBM12_TheoDoiHopDong_GetByNamKy(_NamKy, _TrangThai).ToList();
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("rptBM12_TheoDoiHopDong_GetByNamKy", lst));
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\BM12_TheoDoiHopDong_TheoNamKy.rdlc";
            ReportParameter[] parm = new ReportParameter[1];
            parm[0] = new ReportParameter("pNamKy", _NamKy.ToString());
            reportViewer.LocalReport.SetParameters(parm);
            ViewBag.ReportViewer = reportViewer;
            return PartialView("Print");
        }

    }
}