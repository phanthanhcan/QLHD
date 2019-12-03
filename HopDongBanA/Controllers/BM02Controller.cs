using HopDongMgr.Class.Common;
using HopDongMgr.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace HopDongMgr.Controllers
{
    public class BM02Controller : Controller
    {
        // GET: BM02
        private HopDongMgrEntities db = new HopDongMgrEntities();
        ReportViewer reportViewer = new ReportViewer();
        //[CustomAuthorization]
        public ActionResult Index()
        {
            if (Session["MaDV"] != null)
            {
                var mdv = Session["MaDV"].ToString();
                IEnumerable<DM_CongTrinh> dv = db.DM_CongTrinh.ToList();
                ViewBag.Model = new SelectList(dv, "MaCT", "TenCT");
                ViewBag.ReportViewer = reportViewer;
                return View();
            }
            else
                return RedirectToAction("Login", "Home");
        }
        [HttpPost]
        public ActionResult Print(string IDCT)
        {
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(1800);
            reportViewer.Height = Unit.Percentage(1800);
            reportViewer.ShowPrintButton = true;
            List<rptBM01_TheoDoiHopDong_GetByCongTrinh_Result> lst = db.rptBM01_TheoDoiHopDong_GetByCongTrinh(IDCT, 0).ToList();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("rptBM01_TheoDoiHopDong_GetByCongTrinh", lst));

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\rptBM01_TheoDoiHopDong_GetByCongTrinh.rdlc";

            ViewBag.ReportViewer = reportViewer;
            ViewBag.Model = new SelectList(db.DM_CongTrinh, "MaCT", "TenCT");

            return PartialView("Print");
        }
    }
}