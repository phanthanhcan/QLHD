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
    public class HT_ThamSoController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();

        // GET: HT_ThamSo
        [CustomAuthorization]
        public ActionResult Index()
        {
            return View(db.HT_ThamSo.ToList());
        }

        // GET: HT_ThamSo/Details/5
        [CustomAuthorization]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HT_ThamSo hT_ThamSo = db.HT_ThamSo.Find(id);
            if (hT_ThamSo == null)
            {
                return HttpNotFound();
            }
            return View(hT_ThamSo);
        }

        // GET: HT_ThamSo/Create
        [CustomAuthorization]
        public ActionResult Create()
        {
            return View();
        }

        // POST: HT_ThamSo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Ten,GiaTri")] HT_ThamSo hT_ThamSo)
        {
            if (ModelState.IsValid)
            {
                db.HT_ThamSo.Add(hT_ThamSo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hT_ThamSo);
        }

        // GET: HT_ThamSo/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HT_ThamSo hT_ThamSo = db.HT_ThamSo.Find(id);
            if (hT_ThamSo == null)
            {
                return HttpNotFound();
            }
            return View(hT_ThamSo);
        }

        // POST: HT_ThamSo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Ten,GiaTri")] HT_ThamSo hT_ThamSo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hT_ThamSo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hT_ThamSo);
        }

        // POST: HT_ThamSo/Delete/5
        [CustomAuthorization]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            HT_ThamSo hT_ThamSo = db.HT_ThamSo.Find(id);
            db.HT_ThamSo.Remove(hT_ThamSo);
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
