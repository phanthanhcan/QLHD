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
            ViewBag.MA_DVICTREN = new SelectList(db.DM_DONVI, "MA_DVIQLY", "TEN_DVIQLY");
            DM_DONVI dv = db.DM_DONVI.Find(dM_DONVI.MA_DVIQLY);
            if (dv != null)
            {
                TempData["err"] = "<div class='alert alert-info' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Mã đơn vị đã tồn tại</div> ";
                return View(dM_DONVI);
            }
            if (ModelState.IsValid)
            {
                db.DM_DONVI.Add(dM_DONVI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dM_DONVI);
        }

        // GET: DM_DONVI/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(string id)
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
            if (ModelState.IsValid)
            {
                db.Entry(dM_DONVI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MA_DVICTREN = new SelectList(db.DM_DONVI, "MA_DVIQLY", "TEN_DVIQLY", dM_DONVI.MA_DVICTREN);
            return View(dM_DONVI);
        }

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
