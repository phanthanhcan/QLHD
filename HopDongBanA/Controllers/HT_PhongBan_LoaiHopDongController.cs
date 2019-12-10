using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HopDongMgr.Models;
using System.Data.SqlClient;
using HopDongMgr.Class.ViewModel;
using HopDongMgr.Class.Common;
using HopDongMgr.Class.Json;

namespace HopDongMgr.Controllers
{
    public class HT_PhongBan_LoaiHopDongController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();

        // GET: HT_PhongBan_LoaiHopDong
        [CustomAuthorization]
        public ActionResult Index()
        {
            return View(db.DM_PHONG.OrderBy(o => o.Ten).ToList());
        }

        public ActionResult GetPhanQuyen(string id)
        {
            var idPB_Parameter = new SqlParameter("@IDPB", id);
            var result = db.Database.SqlQuery<PhanQuyenPhongBanViewModel>("GetPhanQuyenPhongBan @IDPB", idPB_Parameter).ToList();

            ListPhanQuyenPhongBanViewModel pq = new ListPhanQuyenPhongBanViewModel();
            pq.listPQ = result;

            ViewBag.IDPB = id;

            return PartialView("GetPhanQuyen", pq);
        }

        #region Canpt them chuc nang phan quyền theo loai HD
        //[CustomAuthorization]
        /// <summary>
        /// Canpt them chuc nang phan quyền theo loai HD
        /// </summary>
        /// <param name="pqJson"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult XuLyPhanQuyenTheoLoaiHopDong(PhanQuyenPBJson pqJson)
        {
            //Guid gIDPB = pqJson.IDPB;
            //List<JsonArrayPhanQuyenPBLoaiHD> list = pqJson.list;
            //string error = "";

            //try
            //{
            //    /// xóa tat ca phongban theo loai hop dong
            //    List<HT_PhongBan_LoaiHopDong> liNhomLoaiHopDongRemove = db.HT_PhongBan_LoaiHopDong.Where(o => o.IDPB.CompareTo(gIDPB) == 0).ToList();
            //    foreach (var item in liNhomLoaiHopDongRemove)
            //    {
            //        db.HT_PhongBan_LoaiHopDong.Remove(item);
            //        db.SaveChanges();
            //    }
            //    /// them moi phong ban theo hop dong
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        int idLoaiHopDong = list[i].idLoaiHopdong;
            //        if (list[i].check)
            //        {
            //            HT_PhongBan_LoaiHopDong oPhongBanLoaiHopDong = new HT_PhongBan_LoaiHopDong();
            //            oPhongBanLoaiHopDong.IDPB = gIDPB;
            //            oPhongBanLoaiHopDong.IDLoaiHopDong = list[i].idLoaiHopdong;
            //            db.HT_PhongBan_LoaiHopDong.Add(oPhongBanLoaiHopDong);
            //            db.SaveChanges();
            //        }
            //    }

            //    error = "1";
            //}
            //catch (Exception ex)
            //{
            //    error = ex.Message;
            //}

            //return Json(error);
            Guid idPB = pqJson.idPB;
            List<JsonArrayPhanQuyenPB> list = pqJson.list;
            string error = "";

            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int IDLoai = list[i].IDLoai;
                    var count = db.HT_PhongBan_LoaiHopDong.Where(s => s.IDLoaiHopDong == IDLoai && s.IDPB == idPB).ToList().Count;

                    if (list[i].check)
                    {
                        if (count == 0)
                        {
                            HT_PhongBan_LoaiHopDong ncn = new HT_PhongBan_LoaiHopDong();
                            ncn.IDPB = idPB;
                            ncn.IDLoaiHopDong = IDLoai;
                            db.HT_PhongBan_LoaiHopDong.Add(ncn);
                        }
                    }
                    else
                    {
                        if (count == 1)
                        {
                            HT_PhongBan_LoaiHopDong ncn = db.HT_PhongBan_LoaiHopDong.Where(s => s.IDLoaiHopDong == IDLoai && s.IDPB == idPB).FirstOrDefault();
                            db.HT_PhongBan_LoaiHopDong.Remove(ncn);
                        }
                    }

                    db.SaveChanges();
                }
                HT_LichSuHoatDong ls = new HT_LichSuHoatDong(
                this.ControllerContext.RouteData.Values["controller"].ToString()
                , "UPDATE"
                , DateTime.Now, Session["username"]?.ToString()
                , $" {this.ControllerContext.RouteData.Values["action"]?.ToString()} ");
                db.HT_LichSuHoatDong.Add(ls);
                db.SaveChanges();

                error = "1";
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(error);
        }
        #endregion

        // GET: HT_PhongBan_LoaiHopDong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HT_PhongBan_LoaiHopDong hT_PhongBan_LoaiHopDong = db.HT_PhongBan_LoaiHopDong.Find(id);
            if (hT_PhongBan_LoaiHopDong == null)
            {
                return HttpNotFound();
            }
            return View(hT_PhongBan_LoaiHopDong);
        }

        // GET: HT_PhongBan_LoaiHopDong/Create
        public ActionResult Create()
        {
            ViewBag.IDLoaiHopDong = new SelectList(db.DM_LoaiHopDong, "IDLoai", "TenLoai");
            ViewBag.IDPB = new SelectList(db.DM_PHONG, "Id", "Ten");
            return View();
        }

        // POST: HT_PhongBan_LoaiHopDong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPQPB,IDPB,IDLoaiHopDong")] HT_PhongBan_LoaiHopDong hT_PhongBan_LoaiHopDong)
        {
            if (ModelState.IsValid)
            {
                db.HT_PhongBan_LoaiHopDong.Add(hT_PhongBan_LoaiHopDong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDLoaiHopDong = new SelectList(db.DM_LoaiHopDong.Where(o => o.Khoa.CompareTo(false) == 0), "IDLoai", "TenLoai", hT_PhongBan_LoaiHopDong.IDLoaiHopDong);
            ViewBag.IDPB = new SelectList(db.DM_PHONG, "Id", "Ten", hT_PhongBan_LoaiHopDong.IDPB);
            return View(hT_PhongBan_LoaiHopDong);
        }

        // GET: HT_PhongBan_LoaiHopDong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HT_PhongBan_LoaiHopDong hT_PhongBan_LoaiHopDong = db.HT_PhongBan_LoaiHopDong.Find(id);
            if (hT_PhongBan_LoaiHopDong == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDLoaiHopDong = new SelectList(db.DM_LoaiHopDong.Where(o => o.Khoa.CompareTo(false) == 0), "IDLoai", "TenLoai", hT_PhongBan_LoaiHopDong.IDLoaiHopDong);
            ViewBag.IDPB = new SelectList(db.DM_PHONG, "Id", "Ten", hT_PhongBan_LoaiHopDong.IDPB);
            return View(hT_PhongBan_LoaiHopDong);
        }

        // POST: HT_PhongBan_LoaiHopDong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPQPB,IDPB,IDLoaiHopDong")] HT_PhongBan_LoaiHopDong hT_PhongBan_LoaiHopDong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hT_PhongBan_LoaiHopDong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDLoaiHopDong = new SelectList(db.DM_LoaiHopDong.Where(o => o.Khoa.CompareTo(false) == 0), "IDLoai", "TenLoai", hT_PhongBan_LoaiHopDong.IDLoaiHopDong);
            ViewBag.IDPB = new SelectList(db.DM_PHONG, "Id", "Ten", hT_PhongBan_LoaiHopDong.IDPB);
            return View(hT_PhongBan_LoaiHopDong);
        }

        // GET: HT_PhongBan_LoaiHopDong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HT_PhongBan_LoaiHopDong hT_PhongBan_LoaiHopDong = db.HT_PhongBan_LoaiHopDong.Find(id);
            if (hT_PhongBan_LoaiHopDong == null)
            {
                return HttpNotFound();
            }
            return View(hT_PhongBan_LoaiHopDong);
        }

        // POST: HT_PhongBan_LoaiHopDong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HT_PhongBan_LoaiHopDong hT_PhongBan_LoaiHopDong = db.HT_PhongBan_LoaiHopDong.Find(id);
            db.HT_PhongBan_LoaiHopDong.Remove(hT_PhongBan_LoaiHopDong);
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
