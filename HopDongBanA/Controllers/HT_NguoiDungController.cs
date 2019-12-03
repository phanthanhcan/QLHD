using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HopDongMgr.Models;
using HopDongMgr.DungChung;
using HopDongMgr.Class.Common;
using System.Data.SqlClient;

namespace HopDongMgr.Controllers
{
    public class HT_NguoiDungController : Controller
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();

        // GET: HT_NguoiDung
        [CustomAuthorization]
        public ActionResult Index()
        {
            //var hT_NguoiDung = db.HT_NguoiDung.Include(h => h.).Include(h => h.HT_Nhom).OrderBy(a => a.MaDV);
            return View(db.HT_NguoiDung.ToList());
        }

        // GET: HT_NguoiDung/Details/5
        [CustomAuthorization]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HT_NguoiDung hT_NguoiDung = db.HT_NguoiDung.Find(id);
            hT_NguoiDung.MatKhau = Common.EncryptMD5(hT_NguoiDung.MatKhau);
            if (hT_NguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(hT_NguoiDung);
        }

        // GET: HT_NguoiDung/Create
        [CustomAuthorization]
        public ActionResult Create()
        {
            string maDV = Session["MaDV"].ToString();
            ViewBag.MaDV = new SelectList(db.DM_DONVI.Where(a => a.MA_DVIQLY.Contains(maDV)), "MA_DVIQLY", "TEN_DVIQLY");
            ViewBag.IdNhom = new SelectList(db.HT_Nhom.Where(a => a.MaDV == maDV).OrderBy(a => a.Ten), "Id", "Ten");
            ViewBag.IdPhong = new SelectList(db.DM_PHONG.Where(a => a.MaDV == maDV).OrderBy(a => a.Ten), "Id", "Ten");
            ViewBag.GioiTinh = Common.GioiTinh("");
            ViewBag.ChucVu = new SelectList(db.HT_CHUCVU.OrderBy(a => a.Ten), "Id", "Ten");
            return View();
        }

        // POST: HT_NguoiDung/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "oid,MaNguoiDung,MatKhau,HoTen,Email,AnhDaiDien,MaDV,IdNhom,IdPhong,GioiTinh,ChucVu,Active,BiDanh")] HT_NguoiDung hT_NguoiDung, HttpPostedFileBase photo)
        {
            string maDV = Session["MaDV"].ToString();
            ViewBag.MaDV = new SelectList(db.DM_DONVI.Where(a => a.MA_DVIQLY.Contains(maDV)), "MA_DVIQLY", "TEN_DVIQLY");
            ViewBag.IdNhom = new SelectList(db.HT_Nhom.Where(a => a.MaDV == maDV).OrderBy(a => a.Ten), "Id", "Ten");
            ViewBag.IdPhong = new SelectList(db.DM_PHONG.Where(a => a.MaDV == maDV).OrderBy(a => a.Ten), "Id", "Ten");
            ViewBag.GioiTinh = Common.GioiTinh("");
            ViewBag.ChucVu = new SelectList(db.HT_CHUCVU.OrderBy(a => a.Ten), "Id", "Ten", hT_NguoiDung.ChucVu);
            if (string.IsNullOrEmpty(hT_NguoiDung.MaNguoiDung))
            {
                TempData["err"] = "<div class='alert alert-info' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng điền mã người dùng</div> ";
                return View(hT_NguoiDung);
            }
            List<HT_NguoiDung> nd = db.HT_NguoiDung.Where(a => a.MaNguoiDung == hT_NguoiDung.MaNguoiDung).ToList();
            if (nd.Count > 0)
            {
                TempData["err"] = "<div class='alert alert-info' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Mã người dùng đã tồn tại</div> ";
                return View(hT_NguoiDung);
            }
            if (string.IsNullOrEmpty(hT_NguoiDung.HoTen))
            {
                TempData["err"] = "<div class='alert alert-info' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng điền đầy đủ họ tên</div> ";
                return View(hT_NguoiDung);
            }
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(hT_NguoiDung.MatKhau))
                {
                    TempData["err"] = "<div class='alert alert-info' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng nhập mật khẩu</div> ";
                    return View(hT_NguoiDung);
                }
                hT_NguoiDung.oid = Guid.NewGuid();
                hT_NguoiDung.MatKhau = Common.EncryptMD5(hT_NguoiDung.MatKhau);
                if (photo != null)
                {
                    var fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    hT_NguoiDung.AnhDaiDien = fileName;
                    UploadPhoto(fileName, photo);
                }
                else
                    hT_NguoiDung.AnhDaiDien = "avatar.png";
                Session["avatar"] = hT_NguoiDung.AnhDaiDien;
                db.HT_NguoiDung.Add(hT_NguoiDung);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hT_NguoiDung);
        }

        // GET: HT_NguoiDung/Edit/5
        [CustomAuthorization]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string maDV = Session["MaDV"].ToString();
            HT_NguoiDung hT_NguoiDung = db.HT_NguoiDung.Find(id);
            if (hT_NguoiDung == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDV = new SelectList(db.DM_DONVI.Where(a => a.MA_DVIQLY.Contains(maDV)), "MA_DVIQLY", "TEN_DVIQLY", hT_NguoiDung.MaDV);
            ViewBag.IdNhom = new SelectList(db.HT_Nhom.Where(a => a.MaDV == hT_NguoiDung.MaDV).OrderBy(a => a.Ten), "Id", "Ten", hT_NguoiDung.IdNhom);
            ViewBag.IdPhong = new SelectList(db.DM_PHONG.Where(a => a.MaDV == hT_NguoiDung.MaDV).OrderBy(a => a.Ten), "Id", "Ten", hT_NguoiDung.IdPhong);

            ViewBag.GioiTinh = Common.GioiTinh(hT_NguoiDung.GioiTinh);
            ViewBag.ChucVu = new SelectList(db.HT_CHUCVU.OrderBy(a => a.Ten), "Id", "Ten", hT_NguoiDung.ChucVu);
            if (string.IsNullOrEmpty(hT_NguoiDung.AnhDaiDien))
                hT_NguoiDung.AnhDaiDien = "avatar.png";
            Session["avatar"] = hT_NguoiDung.AnhDaiDien;
            hT_NguoiDung.MatKhau = "";
            return View(hT_NguoiDung);
        }

        // POST: HT_NguoiDung/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "oid,MaNguoiDung,MatKhau,HoTen,Email,AnhDaiDien,MaDV,IdNhom,IdPhong,GioiTinh,ChucVu,Active,BiDanh")] HT_NguoiDung hT_NguoiDung, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                string maDV = Session["MaDV"].ToString();
                ViewBag.MaDV = new SelectList(db.DM_DONVI.Where(a => a.MA_DVIQLY.Contains(maDV)), "MA_DVIQLY", "TEN_DVIQLY", hT_NguoiDung.MaDV);
                ViewBag.IdNhom = new SelectList(db.HT_Nhom.Where(a => a.MaDV == hT_NguoiDung.MaDV).OrderBy(a => a.Ten), "Id", "Ten", hT_NguoiDung.IdNhom);
                ViewBag.IdPhong = new SelectList(db.DM_PHONG.Where(a => a.MaDV == hT_NguoiDung.MaDV).OrderBy(a => a.Ten), "Id", "Ten", hT_NguoiDung.IdPhong);
                ViewBag.GioiTinh = Common.GioiTinh(hT_NguoiDung.GioiTinh);
                ViewBag.ChucVu = new SelectList(db.HT_CHUCVU.OrderBy(a => a.Ten), "Id", "Ten", hT_NguoiDung.ChucVu);
                if (string.IsNullOrEmpty(hT_NguoiDung.MaNguoiDung))
                {
                    TempData["err"] = "<div class='alert alert-info' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng điền mã người dùng</div> ";
                    return View(hT_NguoiDung);
                }
                if (string.IsNullOrEmpty(hT_NguoiDung.MatKhau))
                {
                    TempData["err"] = "<div class='alert alert-info' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng nhập mật khẩu</div> ";
                    return View(hT_NguoiDung);
                }
                if (string.IsNullOrEmpty(hT_NguoiDung.HoTen))
                {
                    TempData["err"] = "<div class='alert alert-info' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Vui lòng điền đầy đủ họ tên</div> ";
                    return View(hT_NguoiDung);
                }
                hT_NguoiDung.MatKhau = Common.EncryptMD5(hT_NguoiDung.MatKhau);
                if (photo != null)
                {
                    var fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    hT_NguoiDung.AnhDaiDien = fileName;
                    UploadPhoto(fileName, photo);
                }
                else
                {
                    hT_NguoiDung.AnhDaiDien = "avatar.png";
                }
                Session["avatar"] = hT_NguoiDung.AnhDaiDien;
                db.Entry(hT_NguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                Response.Redirect(Url.Action("Index"));
                return RedirectToAction("Index");
            }
            return View(hT_NguoiDung);
        }

        // POST: HT_NguoiDung/Delete/5
        [CustomAuthorization]
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            HT_NguoiDung hT_NguoiDung = db.HT_NguoiDung.Find(id);
            db.HT_NguoiDung.Remove(hT_NguoiDung);
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

        [HttpGet]
        public JsonResult DonViOnChange(string maDonVi)
        {
            return Json(db.HT_Nhom.Where(a => a.MaDV == maDonVi).Select(x => new { Id = x.Id, Ten = x.Ten }).OrderBy(a => a.Ten), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DonViOnChangePhong(string maDonVi)
        {
            return Json(db.DM_PHONG.Where(a => a.MaDV == maDonVi).Select(x => new { Id = x.Id, Ten = x.Ten }).OrderBy(a => a.Ten), JsonRequestBehavior.AllowGet);
        }

        public void UploadPhoto(string fileName, HttpPostedFileBase photo)
        {
            try
            {
                if (photo != null && photo.ContentLength > 0)
                {
                    var stream = photo.InputStream;
                    var path = System.IO.Path.Combine(Request.MapPath("/Content/images/avatars/"), fileName);
                    using (var fileStream = System.IO.File.Create(path))
                    {
                        stream.CopyTo(fileStream);
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }

        [HttpPost]
        public JsonResult DeletePhoto(string nguoiDung, string photoFileName)
        {
            if (photoFileName == "avatar.png")
            {
                TempData["err"] = "<div class='alert alert-info' role='alert'><span class='glyphicon glyphicon-exclamation - sign' aria-hidden='true'></span><span class='sr - only'></span>Không thể xóa hình mặc định</div> ";
                return Json("Ảnh mặc định");
            }
            string fullPath = Request.MapPath("/Content/images/avatars/" + photoFileName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                HT_NguoiDung nd = db.HT_NguoiDung.Find(Guid.Parse(nguoiDung));
                nd.AnhDaiDien = "avatar.png";
                db.SaveChanges();
                Session["avatar"] = nd.AnhDaiDien;
            }
            return Json("Deleted");
        }

        [HttpPost]
        public JsonResult CapNhatMatKhau(string password_new, string password_old, string password_repair)
        {
            string result = "";
            string pass = Common.EncryptMD5(password_old);
            string username = Session["username"].ToString();
            //string sqlQuery = "exec GetDTXD_DanhMucDiaDiem @Check OUTPUT";
            if (password_new != password_repair) return Json("Mật khẩu mới không giống nhau");
            if(db.HT_NguoiDung.Where( o => o.MaNguoiDung == username && o.MatKhau == pass).FirstOrDefault() == null) return Json("Mật khẩu cũ không đúng");
            try
            {
                SqlParameter[] AParameter = {
                                        new SqlParameter
                                        {
                                         ParameterName = "@oid",
                                         Value = Session["userid"],
                                         SqlDbType = SqlDbType.UniqueIdentifier
                                        },
                                                                                new SqlParameter
                                        {
                                            ParameterName = "@matkhau",
                                            Value = Common.EncryptMD5(password_new),
                                            SqlDbType = SqlDbType.NVarChar
                                        },

            };
                string sqlQuery = "UpdateMatKhau @oid , @matkhau";
                db.Database.ExecuteSqlCommand(sqlQuery, AParameter);
                result = "Cập nhật thành công";
            }
            catch (Exception ex)
            {
                result = "Có lỗi xãy ra";
            }
            return Json(result);
        }
    }
}
