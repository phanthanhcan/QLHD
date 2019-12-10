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

namespace HopDongMgr.Controllers
{
    public class DM_DONVIController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();

        // GET: DM_DONVI
        [CustomAuthorization]
        public ActionResult Index()
        {
            return View(db.DM_DONVI.OrderBy(a => a.MA_DVIQLY).ToList());
        }

        // GET: DM_DONVI/Details/5
        [CustomAuthorization]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DM_DONVI dM_DONVI = db.DM_DONVI.Find(id);
            if (dM_DONVI == null)
            {
                return HttpNotFound();
            }
            return View(dM_DONVI);
        }

        // GET: DM_DONVI/Create
        [CustomAuthorization]
        public ActionResult Create()
        {
            db.Configuration.LazyLoadingEnabled = false;
            ViewBag.MA_DVICTREN = new SelectList(db.DM_DONVI, "MA_DVIQLY", "TEN_DVIQLY");
            return View();
        }

        // POST: DM_DONVI/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MA_DVIQLY,TEN_DVIQLY,MA_DVICTREN,CAP_DVI,DIA_CHI,ID_DIA_CHINH,DIEN_THOAI,DTHOAI_KDOANH,DTHOAI_NONG,DTHOAI_TRUC,FAX,EMAIL,MA_STHUE,DAI_DIEN,CHUC_VU,SO_UQUYEN,NGAY_UQUYEN,TEN_DVIUQ,DCHI_DVIUQ,CVU_UQUYEN,TNGUOI_UQUYEN,TEN_TINH,WEBSITE,MaDiaChinh,TenTat")] DM_DONVI dM_DONVI)
        {

            db.Configuration.LazyLoadingEnabled = false;
            try
            {
                DM_DONVI dv = db.DM_DONVI.Find(dM_DONVI.MA_DVIQLY);
                if(dv != null) ModelState.AddModelError("MA_DVIQLY", $" Mã {dM_DONVI.MA_DVIQLY} đã tồn tại");
                if (ModelState.IsValid)
                {
                    db.DM_DONVI.Add(dM_DONVI);
                    db.SaveChanges();
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                        this.ControllerContext.RouteData.Values["controller"].ToString()
                        , "CREATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $" {this.ControllerContext.RouteData.Values["action"]?.ToString()} - {dM_DONVI.TEN_DVIQLY} ");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.MA_DVICTREN = new SelectList(db.DM_DONVI, "MA_DVIQLY", "TEN_DVIQLY");
                return View(dM_DONVI);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Không ghi được dữ liệu.<br/>Lý do: " + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }

        #region Update
        // GET: DM_DONVI/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Configuration.LazyLoadingEnabled = false;
            DM_DONVI dM_DONVI = db.DM_DONVI.Find(id);
            if (dM_DONVI == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_DVICTREN = new SelectList(db.DM_DONVI, "MA_DVIQLY", "TEN_DVIQLY", dM_DONVI.MA_DVICTREN);
            return View(dM_DONVI);
        }

        // POST: DM_DONVI/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_DVIQLY,TEN_DVIQLY,MA_DVICTREN,CAP_DVI,DIA_CHI,ID_DIA_CHINH,DIEN_THOAI,DTHOAI_KDOANH,DTHOAI_NONG,DTHOAI_TRUC,FAX,EMAIL,MA_STHUE,DAI_DIEN,CHUC_VU,SO_UQUYEN,NGAY_UQUYEN,TEN_DVIUQ,DCHI_DVIUQ,CVU_UQUYEN,TNGUOI_UQUYEN,TEN_TINH,WEBSITE,MaDiaChinh,TenTat")] DM_DONVI dM_DONVI)
        {
            db.Configuration.LazyLoadingEnabled = false;
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(dM_DONVI).State = EntityState.Modified;
                    db.SaveChanges();
                    HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                        this.ControllerContext.RouteData.Values["controller"].ToString()
                        , "UPDATE"
                        , DateTime.Now, Session["username"]?.ToString()
                        , $" {this.ControllerContext.RouteData.Values["action"]?.ToString()} - {dM_DONVI.TEN_DVIQLY} ");
                    db.HT_LichSuHoatDong.Add(ls);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.MA_DVICTREN = new SelectList(db.DM_DONVI, "MA_DVIQLY", "TEN_DVIQLY", dM_DONVI.MA_DVICTREN);
                return View(dM_DONVI);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = "Lỗi ghi dữ liệu.<br/>Lý do:" + ex.Message;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, cauBaoLoi);
            }
        }
        #endregion
        // POST: DM_DONVI/Delete/5
        [CustomAuthorization]
        [HttpPost]
        public ActionResult Delete(string id)
        {
            DM_DONVI dM_DONVI = db.DM_DONVI.Find(id);
            db.DM_DONVI.Remove(dM_DONVI);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [CustomAuthorization]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
