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
using HopDongMgr.DungChung;

namespace HopDongMgr.Controllers
{
    public class DM_LoaiHopDongController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();
        private Common _common = new Common();

        // GET: DM_LoaiHopDong
        [CustomAuthorization]
        public ActionResult Index()
        {
            return View(db.DM_LoaiHopDong.ToList());
        }

        // GET: DM_LoaiHopDong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DM_LoaiHopDong dM_LoaiHopDong = db.DM_LoaiHopDong.Find(id);
            if (dM_LoaiHopDong == null)
            {
                return HttpNotFound();
            }
            return View(dM_LoaiHopDong);
        }

        // GET: DM_LoaiHopDong/Create
        [CustomAuthorization]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DM_LoaiHopDong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLoai,TenLoai,Khoa,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat")] DM_LoaiHopDong dM_LoaiHopDong)
        {
            if (ModelState.IsValid)
            {
                List<SelectListItem> list = _common.getThongTinBang();
                dM_LoaiHopDong.NguoiTao = list.Where(o => o.Value == "NguoiTao").SingleOrDefault().Text;
                dM_LoaiHopDong.NgayTao = DateTime.Parse(list.Where(o => o.Value == "NgayTao").SingleOrDefault().Text);
                db.DM_LoaiHopDong.Add(dM_LoaiHopDong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dM_LoaiHopDong);
        }

        // GET: DM_LoaiHopDong/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DM_LoaiHopDong dM_LoaiHopDong = db.DM_LoaiHopDong.Find(id);
            if (dM_LoaiHopDong == null)
            {
                return HttpNotFound();
            }
            return View(dM_LoaiHopDong);
        }

        // POST: DM_LoaiHopDong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDLoai,TenLoai,Khoa,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat")] DM_LoaiHopDong dM_LoaiHopDong)
        {
            if (ModelState.IsValid)
            {
                List<SelectListItem> list = _common.getThongTinBang();
                dM_LoaiHopDong.NguoiCapNhat = list.Where(o => o.Value == "NguoiCapNhat").SingleOrDefault().Text;
                dM_LoaiHopDong.NgayCapNhat = DateTime.Parse(list.Where(o => o.Value == "NgayCapNhat").SingleOrDefault().Text);
                db.Entry(dM_LoaiHopDong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dM_LoaiHopDong);
        }


        // POST: DM_LoaiHopDong/Delete/5
        [HttpPost]
        [CustomAuthorization]
        public ActionResult Delete(int id)
        {
            DM_LoaiHopDong dM_LoaiHopDong = db.DM_LoaiHopDong.Find(id);
            db.DM_LoaiHopDong.Remove(dM_LoaiHopDong);
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
