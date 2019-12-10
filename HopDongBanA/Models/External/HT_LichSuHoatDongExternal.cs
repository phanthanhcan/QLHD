using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HopDongMgr.Models
{
    public partial class  HT_LichSuHoatDong
    {
        public HT_LichSuHoatDong() { }
        public HT_LichSuHoatDong( string chucNang, string loaiHoatDong, DateTime ngayThucHien, string nguoiThucHien, string chiTietHoatDong)
        {
            ChucNang = chucNang;
            LoaiHoatDong = loaiHoatDong;
            NgayThucHien = ngayThucHien;
            NguoiThucHien = nguoiThucHien;
            ChiTietHoatDong = chiTietHoatDong;
        }
    }
}