using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HopDongMgr.Class.Json
{
    public class PhanQuyenJson
    {
        public List<JsonArray> list { get; set; }
        public Guid idNhom { get; set; }
    }

    public class JsonArray
    {
        public bool check { get; set; }
        public Guid idChucNang { get; set; }
    }

    public class JsonChartData
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class PhanQuyenPBJson
    {
        public List<JsonArrayPhanQuyenPB> list { get; set; }
        public Guid idPB { get; set; }
    }

    public class JsonArrayPhanQuyenPB
    {
        public bool check { get; set; }
        public int IDLoai { get; set; }
    }











    #region CanPT tao doi tượng phân quyền cho menu loại hợp đồng
    public class PhanQuyenLoaiHDJson
    {
        public List<JsonArrayPhanQuyenLoaiHD> list { get; set; }
        public Guid idNhom { get; set; }
    }

    public class JsonArrayPhanQuyenLoaiHD
    {
        public bool check { get; set; }
        public int idLoaiHopdong { get; set; }
    }
    #endregion
    #region CANPT tao doi tuong phân quyền phòng ban theo loại hop dồng
    public class PhanQuyenPB_LoaiHDJson
    {
        public List<JsonArrayPhanQuyenPBLoaiHD> list { get; set; }
        public Guid IDPB { get; set; }
    }
    public class JsonArrayPhanQuyenPBLoaiHD
    {
        public bool check { get; set; }
        public int idLoaiHopdong { get; set; }
    }

    #endregion
}