using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HopDongMgr.Class.ViewModel
{
    public class PhanQuyenViewModel
    {
        public System.Guid oid { get; set; }
        public string TenController { get; set; }
        public string TenAction { get; set; }
        public string TenHienThi { get; set; }
        public string TenMenu { get; set; }
        public Nullable<int> STT { get; set; }
        public Nullable<bool> IsMenu { get; set; }
        public int Used { get; set; }
        public bool boolUsed {
            get { return Used == 1; }
            set { Used = value ? 1 : 0; }
        }
    }

    public class ListPhanQuyenViewModel
    {
        public List<PhanQuyenViewModel> listPQ { get; set; }
    }
}