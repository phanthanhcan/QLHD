using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HopDongMgr.Models;
using HopDongMgr.DungChung;
using HopDongMgr.Class.Json;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace HopDongMgr.Controllers
{
    public class HomeController : Controller
    {
        HopDongMgrEntities db = new HopDongMgrEntities();

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            HT_NguoiDung nguoiDung = CheckCookie();
            if (nguoiDung != null)
            {
                //return View("Login", nguoiDung);
                return LuuTrangThaiDangNhap(nguoiDung.MaNguoiDung, nguoiDung.MatKhau, "on");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string rememberme)
        {
            HT_NguoiDung user = new HT_NguoiDung();
            if (string.IsNullOrEmpty(username))
            {
                ViewBag.error = "Vui lòng nhập mã người dùng";
            }
            else if (string.IsNullOrEmpty(password))
            {
                user.MaNguoiDung = username;
                ViewBag.error = "Vui lòng nhập mật khẩu";
            }
            //else if (!IsValidRecaptcha(Request["g-recaptcha-response"]))
            //{
            //    user.MaNguoiDung = username;
            //    user.MatKhau = password;
            //    ViewBag.error = "Vui lòng xác thực không phải người máy";
            //}
            else
            {
                return LuuTrangThaiDangNhap(username, password, rememberme);
            }
            return View(user);
        }

        private ActionResult LuuTrangThaiDangNhap(string username, string password, string rememberme)
        {
            string passwordMD5 = Common.EncryptMD5(password);
            var user = db.HT_NguoiDung.SingleOrDefault(a => a.MaNguoiDung == username && a.MatKhau == passwordMD5 && a.Active == true);
            if (user != null)
            {
                Session["userid"] = user.oid;
                Session["username"] = user.MaNguoiDung;
                Session["fullname"] = user.HoTen;
                Session["avatar"] = user.AnhDaiDien;
                Session["MaDV"] = user.MaDV == null ? "PB" : user.MaDV;
                Session["IdNhom"] = user.IdNhom;
                Session["IDMaPhongBan"] = user.IdPhong;
                Session["IDChucVu"] = user.ChucVu;
                Response.Cookies.Clear();
                if (rememberme == "on")
                {
                    HttpCookie ckUsername = new HttpCookie("username");
                    ckUsername.Expires = DateTime.Now.AddDays(30);
                    ckUsername.Value = username;
                    Response.Cookies.Add(ckUsername);
                    HttpCookie ckPassword = new HttpCookie("password");
                    ckPassword.Expires = DateTime.Now.AddDays(30);
                    ckPassword.Value = password;
                    Response.Cookies.Add(ckPassword);
                }
                return RedirectToAction("Index");
            }
            ViewBag.error = "Đăng nhập sai hoặc không có quyền vào";
            return View();
        }

        public HT_NguoiDung CheckCookie()
        {
            HT_NguoiDung nguoiDung = null;
            string userName = string.Empty, password = string.Empty;
            if (Response.Cookies["username"] != null)
                userName = Request.Cookies["username"].Value;
            if (Response.Cookies["password"] != null)
                password = Request.Cookies["password"].Value;
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                nguoiDung = new HT_NguoiDung() { MaNguoiDung = userName, MatKhau = password };
            return nguoiDung;
        }

        public ActionResult Logout()
        {
            //Remove cookie
            if (Response.Cookies["username"] != null)
            {
                HttpCookie ckUsername = new HttpCookie("username");
                ckUsername.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(ckUsername);
            }
            if (Response.Cookies["password"] != null)
            {
                HttpCookie ckPassword = new HttpCookie("password");
                ckPassword.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(ckPassword);
            }
            //Remove session
            Session["userid"] = null;
            Session["username"] = null;
            Session["fullname"] = null;
            Session["avatar"] = null;
            Session["MaDV"] = null;
            Session["IdNhom"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult NotificationAuthorize()
        {
            return View();
        }

        //duy trì session
        public EmptyResult Alive()
        {
            return new EmptyResult();
        }

        public ActionResult SideMenu()
        {
            if (Session["IdNhom"] != null)
            {
                Guid idNhom = Guid.Parse(Session["IdNhom"].ToString());
                List<Guid?> dsNhomChucNang = db.HT_Nhom_ChucNang.Where(a => a.IdNhom == idNhom).Select(b => b.IdChucNang).ToList();
                List<HT_DSChucNang> dsChucNang = db.HT_DSChucNang.Where(a => a.IsMenu == true && dsNhomChucNang.Contains(a.oid)).OrderBy(a => a.oidParent).ThenBy(a => a.STT).ToList();
                ViewBag.Menu = dsChucNang;
            }
            return PartialView();
        }

        public ActionResult AuthorizationFailed(bool useLayout)
        {
            return View(useLayout);
        }

        [HttpGet]
        public JsonResult ChartData()
        {
            //string maDV = Session["MaDV"].ToString();
            //List<TK_TrangChu_AreaChart_Result> result = db.TK_TrangChu_AreaChart(maDV, DateTime.Now.AddDays(-7), DateTime.Now).ToList();
            //return Json(result, JsonRequestBehavior.AllowGet);
            return null;
        }

        [HttpGet]
        public JsonResult PieChartData()
        {
            //string maDV = Session["MaDV"].ToString();
            //List<TK_TrangChu_Result> tK_TrangChu = db.TK_TrangChu(maDV).ToList();
            //JsonChartData[] result = new JsonChartData[tK_TrangChu.Count - 1];
            //for (int i = 1; i < tK_TrangChu.Count; i++)
            //{
            //    TK_TrangChu_Result tk = tK_TrangChu[i];
            //    result[i - 1] = new JsonChartData() { label = tk.TenThongKe, value = tk.SoLuong.ToString() };
            //}
            //return Json(result, JsonRequestBehavior.AllowGet);
            return null;
        }
    }
}