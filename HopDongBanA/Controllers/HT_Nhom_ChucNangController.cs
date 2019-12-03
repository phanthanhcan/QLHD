using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HopDongMgr.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using HopDongMgr.Class.ViewModel;
using HopDongMgr.Class.Json;
using HopDongMgr.Class.Common;

namespace HopDongMgr.Controllers
{
    public class HT_Nhom_ChucNangController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();

        // GET: HT_Nhom_ChucNang
        [CustomAuthorization]
        public ActionResult Index()
        {

            if (Session["Madv"] != null)
            {
                var madv = Session["Madv"].ToString();
                var hT_Nhom = db.HT_Nhom.Include(h => h.DM_DONVI).Where(a => a.MaDV.Contains(madv)).OrderBy(a => a.MaDV);
                return View(hT_Nhom.ToList());
            }
            else
            {
                return View();
            }
            //if (Session["Madv"] != null) {
            //    var madv = Session["Madv"].ToString();

            //    if (Session["userid"] != null)
            //    {
            //        if (Session["userid"].ToString() != "admin")
            //            hT_Nhom.Where(s => s.MaDV == madv);
            //    }
            //}


        }

        [CustomAuthorization]
        public ActionResult GetPhanQuyen(string id)
        {
            var idNhom_Parameter = new SqlParameter("@idNhom", id);
            var result = db.Database.SqlQuery<PhanQuyenViewModel>("GetPhanQuyen @idNhom", idNhom_Parameter).ToList();

            ListPhanQuyenViewModel pq = new ListPhanQuyenViewModel();
            pq.listPQ = result;

            ViewBag.IdNhom = id;
            #region CanPT: lay phan quyền chức năng nhóm loại hợp đồng
            ViewBag.DMLoaiHopDong = db.DM_LoaiHopDong.ToList();
            ViewBag.DMNhomLoaiHopDong = db.HT_Nhom_LoaiHopDong.Where(o => o.IDNhom.ToString() == id).ToList();
            #endregion
            return PartialView("GetPhanQuyen", pq);

        }

        [CustomAuthorization]
        [HttpPost]
        public JsonResult XuLyPhanQuyen(PhanQuyenJson pqJson)
        {
            Guid idNhom = pqJson.idNhom;
            List<JsonArray> list = pqJson.list;
            string error = "";

            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Guid idChucNang = list[i].idChucNang;
                    var count = db.HT_Nhom_ChucNang.Where(s => s.IdChucNang == idChucNang && s.IdNhom == idNhom).ToList().Count;

                    if (list[i].check)
                    {
                        if (count == 0)
                        {
                            HT_Nhom_ChucNang ncn = new HT_Nhom_ChucNang();
                            ncn.Id = Guid.NewGuid();
                            ncn.IdChucNang = idChucNang;
                            ncn.IdNhom = idNhom;
                            db.HT_Nhom_ChucNang.Add(ncn);
                        }
                    }
                    else
                    {
                        if (count == 1)
                        {
                            HT_Nhom_ChucNang ncn = db.HT_Nhom_ChucNang.Where(s => s.IdChucNang == idChucNang && s.IdNhom == idNhom).FirstOrDefault();
                            db.HT_Nhom_ChucNang.Remove(ncn);
                        }
                    }

                    db.SaveChanges();
                }

                error = "1";
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(error);
        }

        #region Canpt them chuc nang phan quyền theo loai HD
        //[CustomAuthorization]
        /// <summary>
        /// Canpt them chuc nang phan quyền theo loai HD
        /// </summary>
        /// <param name="pqJson"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult XuLyPhanQuyenTheoLoaiHopDong(PhanQuyenLoaiHDJson pqJson)
        {
            Guid idNhom = pqJson.idNhom;
            List<JsonArrayPhanQuyenLoaiHD> list = pqJson.list;
            string error = "";

            try
            {
                /// xóa tat ca nhom theo loai hop dong
                List<HT_Nhom_LoaiHopDong> liNhomLoaiHopDongRemove = db.HT_Nhom_LoaiHopDong.Where(o => o.IDNhom.CompareTo(idNhom) == 0).ToList();
                foreach (var item in liNhomLoaiHopDongRemove)
                {
                    db.HT_Nhom_LoaiHopDong.Remove(item);
                    db.SaveChanges();
                }
                /// them moi nhom theo hop dong
                for (int i = 0; i < list.Count; i++)
                {
                    int idLoaiHopDong = list[i].idLoaiHopdong;
                    var count = db.HT_Nhom_LoaiHopDong.Where(s => s.IDLoaiHD == idLoaiHopDong && s.IDNhom == idNhom).ToList().Count;

                    if (list[i].check)
                    {
                        HT_Nhom_LoaiHopDong oNhomLoaiHopDong = new HT_Nhom_LoaiHopDong();
                        oNhomLoaiHopDong.ID = Guid.NewGuid();
                        oNhomLoaiHopDong.IDNhom = idNhom;
                        oNhomLoaiHopDong.IDLoaiHD = idLoaiHopDong;
                        db.HT_Nhom_LoaiHopDong.Add(oNhomLoaiHopDong);
                        db.SaveChanges();
                    }
                }

                error = "1";
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(error);
        }
        #endregion


        // GET: HT_Nhom_ChucNang/Details/5
        [CustomAuthorization]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HT_Nhom_ChucNang hT_Nhom_ChucNang = db.HT_Nhom_ChucNang.Find(id);
            if (hT_Nhom_ChucNang == null)
            {
                return HttpNotFound();
            }
            return View(hT_Nhom_ChucNang);
        }

        // GET: HT_Nhom_ChucNang/Create
        [CustomAuthorization]
        public ActionResult Create()
        {
            ViewBag.IdNhom = new SelectList(db.HT_Nhom, "Id", "Ten");
            ViewBag.IdChucNang = new SelectList(db.HT_DSChucNang, "oid", "TenHienThi");
            return View();
        }

        // POST: HT_Nhom_ChucNang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdChucNang,IdNhom")] HT_Nhom_ChucNang hT_Nhom_ChucNang)
        {
            if (ModelState.IsValid)
            {
                hT_Nhom_ChucNang.Id = Guid.NewGuid();
                db.HT_Nhom_ChucNang.Add(hT_Nhom_ChucNang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdNhom = new SelectList(db.HT_Nhom, "Id", "Ten", hT_Nhom_ChucNang.IdNhom);
            ViewBag.IdChucNang = new SelectList(db.HT_DSChucNang, "oid", "TenController", hT_Nhom_ChucNang.IdChucNang);
            return View(hT_Nhom_ChucNang);
        }

        // GET: HT_Nhom_ChucNang/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HT_Nhom_ChucNang hT_Nhom_ChucNang = db.HT_Nhom_ChucNang.Find(id);
            if (hT_Nhom_ChucNang == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdNhom = new SelectList(db.HT_Nhom, "Id", "Ten", hT_Nhom_ChucNang.IdNhom);
            ViewBag.IdChucNang = new SelectList(db.HT_DSChucNang, "oid", "TenController", hT_Nhom_ChucNang.IdChucNang);
            return View(hT_Nhom_ChucNang);
        }

        // POST: HT_Nhom_ChucNang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdChucNang,IdNhom")] HT_Nhom_ChucNang hT_Nhom_ChucNang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hT_Nhom_ChucNang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdNhom = new SelectList(db.HT_Nhom, "Id", "Ten", hT_Nhom_ChucNang.IdNhom);
            ViewBag.IdChucNang = new SelectList(db.HT_DSChucNang, "oid", "TenController", hT_Nhom_ChucNang.IdChucNang);
            return View(hT_Nhom_ChucNang);
        }

        // POST: HT_Nhom_ChucNang/Delete/5
        [CustomAuthorization]
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            HT_Nhom_ChucNang hT_Nhom_ChucNang = db.HT_Nhom_ChucNang.Find(id);
            db.HT_Nhom_ChucNang.Remove(hT_Nhom_ChucNang);
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
