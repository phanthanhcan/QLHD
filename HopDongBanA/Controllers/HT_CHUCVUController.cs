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
    public class HT_CHUCVUController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();

        // GET: HT_CHUCVU
        [CustomAuthorization]
        public ActionResult Index()
        {
            return View(db.HT_CHUCVU.OrderBy(a => a.Ten).ToList());
        }

        // GET: HT_CHUCVU/Details/5
        [CustomAuthorization]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HT_CHUCVU hT_CHUCVU = db.HT_CHUCVU.Find(id);
            if (hT_CHUCVU == null)
            {
                return HttpNotFound();
            }
            return View(hT_CHUCVU);
        }

        // GET: HT_CHUCVU/Create
        [CustomAuthorization]
        public ActionResult Create()
        {
            return View();
        }

        // POST: HT_CHUCVU/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ten")] HT_CHUCVU hT_CHUCVU)
        {
            if (ModelState.IsValid)
            {
                hT_CHUCVU.Id = Guid.NewGuid();
                db.HT_CHUCVU.Add(hT_CHUCVU);
                db.SaveChanges();
                HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                this.ControllerContext.RouteData.Values["controller"].ToString()
                , "CREATE"
                , DateTime.Now, Session["username"]?.ToString()
                , $" {this.ControllerContext.RouteData.Values["action"]?.ToString()} - {hT_CHUCVU.Ten} ");
                db.HT_LichSuHoatDong.Add(ls);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hT_CHUCVU);
        }

        // GET: HT_CHUCVU/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HT_CHUCVU hT_CHUCVU = db.HT_CHUCVU.Find(id);
            if (hT_CHUCVU == null)
            {
                return HttpNotFound();
            }
            return View(hT_CHUCVU);
        }

        // POST: HT_CHUCVU/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ten")] HT_CHUCVU hT_CHUCVU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hT_CHUCVU).State = EntityState.Modified;
                db.SaveChanges();
                HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                this.ControllerContext.RouteData.Values["controller"].ToString()
                , "UPDATE"
                , DateTime.Now, Session["username"]?.ToString()
                , $" {this.ControllerContext.RouteData.Values["action"]?.ToString()} - {hT_CHUCVU.Ten} ");
                db.HT_LichSuHoatDong.Add(ls);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hT_CHUCVU);
        }

        // POST: HT_CHUCVU/Delete/5
        [CustomAuthorization]
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            HT_CHUCVU hT_CHUCVU = db.HT_CHUCVU.Find(id);
            db.HT_CHUCVU.Remove(hT_CHUCVU);
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
