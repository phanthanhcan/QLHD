using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HopDongMgr.Class.ViewModel
{
    public class PhanQuyenPhongBanViewModel
    {
        public int IDLoai { get; set; }
        public string TenLoai { get; set; }
        public bool Used { get; set; }
        public bool boolUsed
        {
            get { return Used == false; }
            set { Used = value ? false : true; }
        }
    }

    public class ListPhanQuyenPhongBanViewModel
    {
        public List<PhanQuyenPhongBanViewModel> listPQ { get; set; }
    }
}