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
using System.Linq.Dynamic;
using System.Data.SqlClient;

namespace HopDongMgr.Controllers
{
    public class DM_CongTrinhController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();
        private Common _Common = new Common();
        // GET: DM_CongTrinh
        [CustomAuthorization]
        public ActionResult Index()
        {
            var dM_CongTrinh = db.DM_CongTrinh.Include(d => d.DM_NguonVon).Include(d => d.DM_DiaDiem);
            return View(dM_CongTrinh.ToList());
        }

        // GET: DM_CongTrinh/Details/5
        [CustomAuthorization]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DM_CongTrinh dM_CongTrinh = db.DM_CongTrinh.Find(id);
            if (dM_CongTrinh == null)
            {
                return HttpNotFound();
            }
            return View(dM_CongTrinh);
        }

        // GET: DM_CongTrinh/Create
        [CustomAuthorization]
        public ActionResult Create()
        {
            ViewBag.MaNV = new SelectList(db.DM_NguonVon.Where(o => o.Khoa.Value.CompareTo(false) == 0).OrderBy(o => o.TenNV), "MaNV", "TenNV");
            ViewBag.MaDD = new SelectList(db.DM_DiaDiem.Where(o => o.Khoa.Value.CompareTo(false) == 0).OrderBy(o => o.TenDD), "MaDD", "TenDD");
            return View();
        }

        // POST: DM_CongTrinh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCT,MaCT,TenCT,MaNV,MaDD,Khoa,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat")] DM_CongTrinh dM_CongTrinh)
        {
            ViewBag.MaNV = new SelectList(db.DM_NguonVon.Where(o => o.Khoa.Value.CompareTo(false) == 0).OrderBy(o => o.TenNV), "MaNV", "TenNV", dM_CongTrinh.MaNV);
            ViewBag.MaDD = new SelectList(db.DM_DiaDiem.Where(o => o.Khoa.Value.CompareTo(false) == 0).OrderBy(o => o.TenDD), "MaDD", "TenDD", dM_CongTrinh.MaDD);
            if (ModelState.IsValid)
            {//Check data
                if (string.IsNullOrEmpty(dM_CongTrinh.MaCT) || string.IsNullOrWhiteSpace(dM_CongTrinh.MaCT))
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Nhập mã Công trình</div> ";
                    return View(dM_CongTrinh);
                }
                if (string.IsNullOrEmpty(dM_CongTrinh.TenCT) || string.IsNullOrWhiteSpace(dM_CongTrinh.TenCT))
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Nhập mã tên Công trình</div> ";
                    return View(dM_CongTrinh);
                }
                List<SelectListItem> list = _Common.getThongTinBang();
                dM_CongTrinh.NguoiTao = list.Where(o => o.Value == "NguoiTao").SingleOrDefault().Text;
                dM_CongTrinh.NgayTao = DateTime.Parse(list.Where(o => o.Value == "NgayTao").SingleOrDefault().Text);
                dM_CongTrinh.MaCT_DA = dM_CongTrinh.MaCT;// mã công trình sử dụng khi nhâp trên chương trình
                db.DM_CongTrinh.Add(dM_CongTrinh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dM_CongTrinh);
        }

        // GET: DM_CongTrinh/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DM_CongTrinh dM_CongTrinh = db.DM_CongTrinh.Find(decimal.Parse(id));
            if (dM_CongTrinh == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNV = new SelectList(db.DM_NguonVon.Where(o => o.Khoa.Value.CompareTo(false) == 0).OrderBy(o => o.TenNV), "MaNV", "TenNV", dM_CongTrinh.MaNV);
            ViewBag.MaDD = new SelectList(db.DM_DiaDiem.Where(o => o.Khoa.Value.CompareTo(false) == 0).OrderBy(o => o.TenDD), "MaDD", "TenDD", dM_CongTrinh.MaDD);
            return View(dM_CongTrinh);
        }

        // POST: DM_CongTrinh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCT,MaCT,TenCT,MaNV,MaDD,Khoa,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat,MaCT_DA")] DM_CongTrinh dM_CongTrinh)
        {
            ViewBag.MaNV = new SelectList(db.DM_NguonVon.Where(o => o.Khoa.Value.CompareTo(false) == 0).OrderBy(o => o.TenNV), "MaNV", "TenNV", dM_CongTrinh.MaNV);
            ViewBag.MaDD = new SelectList(db.DM_DiaDiem.Where(o => o.Khoa.Value.CompareTo(false) == 0).OrderBy(o => o.TenDD), "MaDD", "TenDD", dM_CongTrinh.MaDD);

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(dM_CongTrinh.TenCT) || string.IsNullOrWhiteSpace(dM_CongTrinh.TenCT))
                {
                    TempData["err"] = "<div class='alert alert-danger' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Nhập mã tên Công trình</div> ";
                    return View(dM_CongTrinh);
                }
                List<SelectListItem> list = _Common.getThongTinBang();
                dM_CongTrinh.NguoiCapNhat = list.Where(o => o.Value == "NguoiCapNhat").SingleOrDefault().Text;
                dM_CongTrinh.NgayCapNhat = DateTime.Parse(list.Where(o => o.Value == "NgayCapNhat").SingleOrDefault().Text);
                db.Entry(dM_CongTrinh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dM_CongTrinh);
        }

        // POST: DM_CongTrinh/Delete/5
        [HttpPost]
        [CustomAuthorization]
        public ActionResult Delete(int id)
        {
            DM_CongTrinh dM_CongTrinh = db.DM_CongTrinh.Find(id);
            db.DM_CongTrinh.Remove(dM_CongTrinh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult GetList()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchVale = Request["search[Value]"];
            string sorColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];


            List<oCongTrinh> list = new List<oCongTrinh>();
            list = db.DM_CongTrinh.Select(x => new oCongTrinh { DT_RowId = x.IDCT, MaCT = (!string.IsNullOrEmpty(x.MaCT) ? x.MaCT : "-"), TenCT = x.TenCT, TenNV = x.DM_NguonVon.TenNV, TenDD = x.DM_DiaDiem.TenDD, Khoa = x.Khoa }).ToList();
            //List<DM_CongTrinh> list = new List<DM_CongTrinh>();
            //list = db.DM_CongTrinh.ToList();
            int TotalRecord = list.Count;
            if (!string.IsNullOrEmpty(searchVale))
            {
                list = list.Where(x => x.MaCT.ToLower().Contains(searchVale.ToLower())).ToList();
            }
            int totalrowFiter = list.Count;
            list = list.OrderBy(sorColumnName + " " + sortDirection).ToList();
            list = list.Skip(start).Take(length).ToList();

            List<oCongtrinh_Data> JsonCongTrinh = new List<oCongtrinh_Data>();
            foreach (var o in list)
            {
                //oCongtrinh_Data data = new oCongtrinh_Data();
                //data.DT_RowId = o.MaCT;
                //data.oCongTrinh.MaCT = o.MaCT;
                //data.oCongTrinh.TenCT = o.TenCT;
                //data.oCongTrinh.TenDD = o.DM_DiaDiem.TenDD;
                //data.oCongTrinh.TenNV = o.DM_NguonVon.TenNV;
                //data.oCongTrinh.Khoa = o.Khoa;
                //JsonCongTrinh.Add(data);
            }
            return Json(new { data = list, draw = Request["draw"], recordsTotal = TotalRecord, recordsFiltered = totalrowFiter }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DongBo_DanhMucCongTrinh()
        {
            string err = "";
            try
            {
                //string sqlQuery = "exec GetDTXD_DanhMucDiaDiem @Check OUTPUT";
                string sqlQuery = "EXEC @return_value = GetDTXD_DanhMucDiaDiem";
                SqlParameter return_Pra = new SqlParameter("@return_value", SqlDbType.Int);
                return_Pra.Direction = ParameterDirection.Output;
                var i = db.Database.ExecuteSqlCommand(sqlQuery, return_Pra);
                int return_value = (int)return_Pra.Value;
                err = return_value.ToString(); // 1 thành công, 0 thất bại
            }
            catch (Exception ex)
            {
                err = "0"; // thất bại
            }

            return Json(err);
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
    public class oCongtrinh_Data
    {

        public string DT_RowId { get; set; }
        public oCongTrinh oCongTrinh { get; set; }
    }
    public class oCongTrinh
    {
        public decimal DT_RowId { get; set; }
        public string MaCT { get; set; }
        public string TenCT { get; set; }
        public string TenNV { get; set; }
        public string TenDD { get; set; }
        public Nullable<bool> Khoa { get; set; }
    }
}
