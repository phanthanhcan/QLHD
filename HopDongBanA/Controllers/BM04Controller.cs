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
    public class BM04Controller : Controller
    {
        // GET: BM04
        private HopDongMgrEntities db = new HopDongMgrEntities();
        //ReportViewer reportViewer = new ReportViewer();
        //[CustomAuthorization]
        public ActionResult Index()
        {
            if (Session["MaDV"] != null)
            {

                var mdv = Session["MaDV"].ToString();
                IEnumerable<DM_DONVI> dv = db.DM_DONVI.Where(a => a.MA_DVIQLY.Contains(mdv)).ToList();
                ViewBag.Model = new SelectList(dv, "MA_DVIQLY", "TEN_DVIQLY");
                //ViewBag.ReportViewer = reportViewer;
                return View();
            }
            else
                return RedirectToAction("Login","Home");
        }     
    }
}