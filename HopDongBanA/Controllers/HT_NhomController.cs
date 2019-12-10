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
    public class HT_NhomController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();

        // GET: HT_Nhom
        [CustomAuthorization]
        public ActionResult Index()
        {
            var hT_Nhom = db.HT_Nhom.Include(h => h.DM_DONVI).OrderBy(a => a.MaDV);
            return View(hT_Nhom.ToList());
        }

        // GET: HT_Nhom/Details/5
        [CustomAuthorization]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HT_Nhom hT_Nhom = db.HT_Nhom.Find(id);
            if (hT_Nhom == null)
            {
                return HttpNotFound();
            }
            return View(hT_Nhom);
        }

        // GET: HT_Nhom/Create
        [CustomAuthorization]
        public ActionResult Create()
        {
            ViewBag.MaDV = new SelectList(db.DM_DONVI, "MA_DVIQLY", "TEN_DVIQLY");
            return View();
        }

        // POST: HT_Nhom/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ten,MaDV,Quyen")] HT_Nhom hT_Nhom)
        {
            if (ModelState.IsValid)
            {
                hT_Nhom.Id = Guid.NewGuid();
                db.HT_Nhom.Add(hT_Nhom);
                db.SaveChanges();
                HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                    this.ControllerContext.RouteData.Values["controller"].ToString()
                    , "CREATE"
                    , DateTime.Now, Session["username"]?.ToString()
                    , $" {this.ControllerContext.RouteData.Values["action"]?.ToString()} - {hT_Nhom.Ten} ");
                db.HT_LichSuHoatDong.Add(ls);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDV = new SelectList(db.DM_DONVI, "MA_DVIQLY", "TEN_DVIQLY", hT_Nhom.MaDV);
            return View(hT_Nhom);
        }

        // GET: HT_Nhom/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HT_Nhom hT_Nhom = db.HT_Nhom.Find(id);
            if (hT_Nhom == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDV = new SelectList(db.DM_DONVI, "MA_DVIQLY", "TEN_DVIQLY", hT_Nhom.MaDV);
            return View(hT_Nhom);
        }

        // POST: HT_Nhom/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ten,MaDV,Quyen")] HT_Nhom hT_Nhom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hT_Nhom).State = EntityState.Modified;
                db.SaveChanges();
                HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                    this.ControllerContext.RouteData.Values["controller"].ToString()
                    , "UPDATE"
                    , DateTime.Now, Session["username"]?.ToString()
                    , $" {this.ControllerContext.RouteData.Values["action"]?.ToString()} - {hT_Nhom.Ten} ");
                db.HT_LichSuHoatDong.Add(ls);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDV = new SelectList(db.DM_DONVI, "MA_DVIQLY", "TEN_DVIQLY", hT_Nhom.MaDV);
            return View(hT_Nhom);
        }

        // POST: HT_Nhom/Delete/5
        [CustomAuthorization]
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            HT_Nhom hT_Nhom = db.HT_Nhom.Find(id);
            db.HT_Nhom.Remove(hT_Nhom);
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
    }
}
