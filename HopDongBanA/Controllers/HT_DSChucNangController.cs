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
    public class HT_DSChucNangController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();

        // GET: HT_DSChucNang
        [CustomAuthorization]
        public ActionResult Index()
        {
            var hT_DSChucNang = db.HT_DSChucNang.Include(h => h.HT_DSChucNang2).OrderBy(a => a.TenController).ThenBy(a => a.STT);
            return View(hT_DSChucNang.ToList());
        }

        // GET: HT_DSChucNang/Details/5
        [CustomAuthorization]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HT_DSChucNang hT_DSChucNang = db.HT_DSChucNang.Find(id);
            if (hT_DSChucNang == null)
            {
                return HttpNotFound();
            }
            return View(hT_DSChucNang);
        }

        // GET: HT_DSChucNang/Create
        [CustomAuthorization]
        public ActionResult Create()
        {
            ViewBag.oidParent = new SelectList(db.HT_DSChucNang.Where(a => a.IsMenu == true).OrderBy(a => a.TenMenu).ThenBy(a => a.STT), "oid", "TenMenu");
            return View();
        }

        // POST: HT_DSChucNang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "oid,TenController,TenAction,TenHienThi,TenMenu,STT,oidParent,IsMenu")] HT_DSChucNang hT_DSChucNang)
        {
            if (ModelState.IsValid)
            {
                hT_DSChucNang.oid = Guid.NewGuid();
                db.HT_DSChucNang.Add(hT_DSChucNang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.oidParent = new SelectList(db.HT_DSChucNang.Where(a => a.IsMenu == true).OrderBy(a => a.TenMenu).ThenBy(a => a.STT), "oid", "TenMenu", hT_DSChucNang.oidParent);
            return View(hT_DSChucNang);
        }

        // GET: HT_DSChucNang/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HT_DSChucNang hT_DSChucNang = db.HT_DSChucNang.Find(id);
            if (hT_DSChucNang == null)
            {
                return HttpNotFound();
            }
            ViewBag.oidParent = new SelectList(db.HT_DSChucNang.Where(a => a.IsMenu == true).OrderBy(a => a.TenMenu).ThenBy(a => a.STT), "oid", "TenMenu", hT_DSChucNang.oidParent);
            return View(hT_DSChucNang);
        }

        // POST: HT_DSChucNang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "oid,TenController,TenAction,TenHienThi,TenMenu,STT,oidParent,IsMenu")] HT_DSChucNang hT_DSChucNang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hT_DSChucNang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.oidParent = new SelectList(db.HT_DSChucNang.Where(a => a.IsMenu == true).OrderBy(a => a.TenMenu).ThenBy(a => a.STT), "oid", "TenMenu", hT_DSChucNang.oidParent);
            return View(hT_DSChucNang);
        }

        // POST: HT_DSChucNang/Delete/5
        [CustomAuthorization]
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            HT_DSChucNang hT_DSChucNang = db.HT_DSChucNang.Find(id);
            db.HT_DSChucNang.Remove(hT_DSChucNang);
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

        [CustomAuthorization]
        public ActionResult RefreshBusiness()
        {
            ReflectionController rc = new ReflectionController();
            List<ReflectionController.ReflectionResult> dsReflection = rc.GetControllerAndAction("HopDongMgr.Controllers");
            List<HT_DSChucNang> dsChucNang = db.HT_DSChucNang.ToList();

            for (int i = 0; i < dsReflection.Count; i++)
            {
                ReflectionController.ReflectionResult reflection = dsReflection[i];
                string tenController = reflection.Controller.Replace("Controller", "");
                string tenAction = reflection.Action;
                string tenHienThi;
                if (!string.IsNullOrEmpty(reflection.Attributes))
                    tenHienThi = tenController + "-" + tenAction + "-" + reflection.Attributes;
                else
                    tenHienThi = tenController + "-" + tenAction;
                if (!tenHienThi.Contains("Home-") && !tenHienThi.Contains("Details") && reflection.Attributes.Contains("CustomAuthorization"))
                {
                    List<HT_DSChucNang> chucNangTonTai = dsChucNang.Where(a => a.TenController == tenController && a.TenAction == tenAction && a.TenHienThi == tenHienThi).ToList();
                    if (chucNangTonTai.Count == 0)
                    {
                        HT_DSChucNang chucNang = new HT_DSChucNang() { oid = Guid.NewGuid(), IsMenu = false, STT = i, TenController = tenController, TenAction = tenAction, TenHienThi = tenHienThi };
                        db.HT_DSChucNang.Add(chucNang);
                    }
                }
            }
            db.SaveChanges();
            TempData["err"] = "<div class='alert alert-info' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Cập nhật thành công</div> ";
            return RedirectToAction("Index");
        }
    }
}