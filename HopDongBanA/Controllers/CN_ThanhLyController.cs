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
    public class CN_ThanhLyController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();
        private Common _common = new Common();

        // GET: CN_ThanhLy
        [CustomAuthorization]
        public ActionResult Index()
        {
            var cN_ThanhLy = db.CN_ThanhLy.Include(c => c.CN_HopDong);
            return View(cN_ThanhLy.ToList());
        }

        // GET: CN_ThanhLy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CN_ThanhLy cN_ThanhLy = db.CN_ThanhLy.Find(id);
            if (cN_ThanhLy == null)
            {
                return HttpNotFound();
            }
            return View(cN_ThanhLy);
        }
        //[CustomAuthorization]
        //public ActionResult Create()
        //{

        //        ViewBag.IDHD = new SelectList(db.CN_HopDong, "IDHD", "SoHopDong");       

        //    return View();
        //}

        // GET: CN_ThanhLy/Create

        [CustomAuthorization]
        public ActionResult Create(int? idhd)
        {
            int gIDHD;
            bool check = int.TryParse(idhd.ToString(), out gIDHD);
            if (!check)
            {
                ViewBag.IDHD = new SelectList(db.CN_HopDong, "IDHD", "SoHopDong");
            }
            else
            {
                ViewBag.IDHD = new SelectList(db.CN_HopDong.Where(o => o.IDHD == gIDHD), "IDHD", "SoHopDong", gIDHD);
            }
            return View();
        }

        // POST: CN_ThanhLy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTL, IDHD, IsHoanThanh, NgayThanhLy, GiaTriThanhLy, GiaTriQuyetToan, NgayTongNghiemThu, NguoiTao, NgayTao, NguoiCapNhat, NgayCapNhat")] CN_ThanhLy cN_ThanhLy)
        {
            if (ModelState.IsValid)
            {
                List<SelectListItem> list = _common.getThongTinBang();
                int IDHD = cN_ThanhLy.IDHD.Value;
                int IDTL = db.CN_ThanhLy.Where(o => o.IDHD == IDHD).SingleOrDefault() == null ? -1 : db.CN_ThanhLy.Where(o => o.IDHD == IDHD).SingleOrDefault().IDTL;
                CN_ThanhLy newN_ThanhLy = db.CN_ThanhLy.Find(IDTL);
                if (newN_ThanhLy == null)
                {
                    cN_ThanhLy.NguoiTao = list.Where(o => o.Value == "NguoiTao").SingleOrDefault().Text;
                    cN_ThanhLy.NgayTao = DateTime.Parse(list.Where(o => o.Value == "NgayTao").SingleOrDefault().Text);
                    cN_ThanhLy.NguoiCapNhat = list.Where(o => o.Value == "NguoiCapNhat").SingleOrDefault().Text;
                    cN_ThanhLy.NgayCapNhat = DateTime.Parse(list.Where(o => o.Value == "NgayCapNhat").SingleOrDefault().Text);
                    db.CN_ThanhLy.Add(cN_ThanhLy);
                    db.SaveChanges();
                }
                else
                {
                    newN_ThanhLy.IDHD = cN_ThanhLy.IDHD;
                    newN_ThanhLy.IsHoanThanh = cN_ThanhLy.IsHoanThanh;
                    newN_ThanhLy.NgayThanhLy = cN_ThanhLy.NgayThanhLy;
                    newN_ThanhLy.GiaTriThanhLy = cN_ThanhLy.GiaTriThanhLy;
                    newN_ThanhLy.GiaTriQuyetToan = cN_ThanhLy.GiaTriQuyetToan;
                    newN_ThanhLy.NgayTongNghiemThu = cN_ThanhLy.NgayTongNghiemThu;
                    newN_ThanhLy.NguoiCapNhat = list.Where(o => o.Value == "NguoiCapNhat").SingleOrDefault().Text;
                    newN_ThanhLy.NgayCapNhat = DateTime.Parse(list.Where(o => o.Value == "NgayCapNhat").SingleOrDefault().Text);
                    db.Entry(newN_ThanhLy).State = EntityState.Modified;
                    db.SaveChanges();
                }
                //TempData["err"] = "<div class='alert alert-success' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Cập nhật thành công</div> ";
                return RedirectToAction("Index");
            }
            TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Có lỗi xảy ra</div> ";
            ViewBag.IDHD = new SelectList(db.CN_HopDong, "IDHD", "SoHopDong", cN_ThanhLy.IDHD);
            return View(cN_ThanhLy);
        }

        // GET: CN_ThanhLy/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CN_ThanhLy cN_ThanhLy = db.CN_ThanhLy.Find(id);
            if (cN_ThanhLy == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDHD = new SelectList(db.CN_HopDong.Where(o => o.IDHD == cN_ThanhLy.IDHD).ToList(), "IDHD", "SoHopDong", cN_ThanhLy.IDHD);
            return View(cN_ThanhLy);
        }

        // POST: CN_ThanhLy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTL, IDHD, IsHoanThanh, NgayThanhLy, GiaTriThanhLy, GiaTriQuyetToan, NgayTongNghiemThu, NguoiTao, NgayTao, NguoiCapNhat, NgayCapNhat")] CN_ThanhLy cN_ThanhLy, FormCollection fCollection)
        {
            ViewBag.IDHD = new SelectList(db.CN_HopDong.Where(o => o.IDHD == cN_ThanhLy.IDHD).ToList(), "IDHD", "SoHopDong", cN_ThanhLy.IDHD);
            if (ModelState.IsValid)
            {
                List<SelectListItem> list = _common.getThongTinBang();
                cN_ThanhLy.NguoiCapNhat = list.Where(o => o.Value == "NguoiCapNhat").SingleOrDefault().Text;
                cN_ThanhLy.NgayCapNhat = DateTime.Parse(list.Where(o => o.Value == "NgayCapNhat").SingleOrDefault().Text);
                db.Entry(cN_ThanhLy).State = EntityState.Modified;
                db.SaveChanges();
                //TempData["err"] = "<div class='alert alert-success' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Cập nhật thành công</div> ";
                return View(cN_ThanhLy);
            }
            TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Có lỗi xảy ra</div> ";
            return View(cN_ThanhLy);
        }

        // POST: CN_ThanhLy/Delete/5
        [HttpPost]
        [CustomAuthorization]
        public ActionResult Delete(int id)
        {
            CN_ThanhLy cN_ThanhLy = db.CN_ThanhLy.Find(id);
            db.CN_ThanhLy.Remove(cN_ThanhLy);
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
