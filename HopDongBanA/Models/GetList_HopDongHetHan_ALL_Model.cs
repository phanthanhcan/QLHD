using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HopDongMgr.Models
{
    public class GetList_HopDongHetHan_ALL_Model
    {

            public int IDHD { get; set; }
            public string TenLoai { get; set; }
            public string SoHopDong { get; set; }
            public Nullable<System.DateTime> NgayKy { get; set; }
            public Nullable<decimal> GiaTriThucTe { get; set; }
            public Nullable<System.DateTime> NgayHetHanThucTe { get; set; }
            public Nullable<decimal> IDCT { get; set; }
            public string TenCT { get; set; }
            public string HoTen { get; set; }
            public Nullable<System.DateTime> NgayHetHanThiCong { get; set; }
    }
}