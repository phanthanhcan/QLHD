using HopDongMgr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace HopDongMgr.DungChung
{
    public class Common
    {
        private HopDongMgrEntities db = new HopDongMgrEntities();

        public static string EncryptMD5(string data)
        {
            MD5CryptoServiceProvider myMD5 = new MD5CryptoServiceProvider();
            byte[] b = System.Text.Encoding.UTF8.GetBytes(data);
            b = myMD5.ComputeHash(b);
            StringBuilder s = new StringBuilder();
            foreach (byte p in b)
            {
                s.Append(p.ToString("x").ToLower());
            }
            return s.ToString();
        }

        public static DateTime ConvertDate(DateTime dtp)
        {
            string kq = dtp.ToString("dd/MM/yyyy");
            var date = kq.Split('/');
            kq = date[2] + "/" + date[1] + "/" + date[0];
            return DateTime.Parse(kq);
        }

        public static List<SelectListItem> GetDSLinhVuc()
        {
            List<SelectListItem> q = new List<SelectListItem>();
            q.Add(new SelectListItem { Value = "1", Text = "Hồ sơ" });
            q.Add(new SelectListItem { Value = "2", Text = "Nghiệp vụ" });
            q.Add(new SelectListItem { Value = "3", Text = "Khác" });
            return q;
        }

        public static List<SelectListItem> TrangThaiHopDong()
        {
            List<SelectListItem> q = new List<SelectListItem>();
            q.Add(new SelectListItem { Value = "-1", Text = "Tất cả" });
            q.Add(new SelectListItem { Value = "0", Text = "Chưa hoàn thành" });
            q.Add(new SelectListItem { Value = "1", Text = "Đã hoàn thành" });
            return q;
        }

        public static SelectList GioiTinh(string selected)
        {
            List<SelectListItem> q = new List<SelectListItem>();
            q.Add(new SelectListItem { Value = "Nam", Text = "Nam" });
            q.Add(new SelectListItem { Value = "Nữ", Text = "Nữ" });
            if (string.IsNullOrEmpty(selected))
                return new SelectList(q, "Value", "Text");
            else
                return new SelectList(q, "Value", "Text", selected);
        }
        /// <summary>
        /// lấy thông tin tạo, cập nhật
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> getThongTinBang()
        {
            var dateQuery = db.Database.SqlQuery<DateTime>("SELECT getdate()");
            DateTime serverDate = dateQuery.AsEnumerable().First();

            List<SelectListItem> q = new List<SelectListItem>();
            q.Add(new SelectListItem { Value = "NguoiTao", Text = HttpContext.Current.Session["userid"].ToString() });
            q.Add(new SelectListItem { Value = "NgayTao", Text = serverDate.ToString("yyyy/MM/dd HH:mm:ss") });
            q.Add(new SelectListItem { Value = "NguoiCapNhat", Text = HttpContext.Current.Session["userid"].ToString() });
            q.Add(new SelectListItem { Value = "NgayCapNhat", Text = serverDate.ToString("yyyy/MM/dd") });
            return q;
        }
    }
    public class HopDongDC
    {

        public string IDHD { get; set; }
        public string ngayKy { get; set; }
        public string NgayHetHan { get; set; }
        public string GiaTriThucTe { get; set; }
    }
}