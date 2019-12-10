using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HopDongMgr.Models
{
    [MetadataType(typeof(CN_DieuChinh.CN_DieuChinhMetaData))]
    public partial class CN_DieuChinh
    {
        class CN_DieuChinhMetaData
        {
            [Display(Name = "ID điều chỉnh")]
            public int IDDC { get; set; }

            [Display(Name = "ID hợp đồng")]
            public Nullable<int> IDHD { get; set; }

            [Display(Name = "Ngày điều chỉnh")]
            [Range(type: typeof(DateTime), minimum: "1990/01/01", maximum: "2200/12/31", ErrorMessage = "{0} có giá trị từ {1} đến {2}")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:dd/MM/yyyy")]
            public Nullable<System.DateTime> NgayDieuChinh { get; set; }


            [Display(Name = "Giá trị điều chỉnh")]
            [Range(type: typeof(decimal), minimum: "99999", maximum: "999999999999999", ErrorMessage = "{0} có giá trị từ {1} đến {2}")]
            public Nullable<decimal> GiaTriDieuChinh { get; set; }

            [Display(Name = "Chênh lệch")]
            public Nullable<decimal> ChenhLech { get; set; }

            [Display(Name = "Số ngày gia hạn tiến độ")]
            [Range(minimum: 1, maximum: 18000, ErrorMessage = "{0} phải từ {1} đến {2}")]
            public Nullable<int> SoNgayGiaHanTienDo { get; set; }

            [Display(Name = "Lý do điều chỉnh")]
            [MaxLength(length: 1024, ErrorMessage ="{0} tối đa {1} ký tự")]
            public string LyDoDieuChinh { get; set; }

            [Display(Name = "Người tạo")]
            public string NguoiTao { get; set; }

            [Display(Name = "Ngày tạo")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:dd/MM/yyyy")]
            public Nullable<System.DateTime> NgayTao { get; set; }

            [Display(Name = "Người Cập nhật")]
            public string NguoiCapNhat { get; set; }

            [Display(Name = "Ngày cập nhạt")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:dd/MM/yyyy")]
            public Nullable<System.DateTime> NgayCapNhat { get; set; }

            [Display(Name = "Đợt điều chỉnh")]
            public int DotDieuChinh { get; set; }

            public Nullable<decimal> DTXD_IDGiaTriPhuLucBoSung { get; set; }

            [Display(Name = "Ngày hết hạn điều chỉnh")]
            [Range(type: typeof(DateTime), minimum: "1990/01/01", maximum: "2200/12/31", ErrorMessage = "{0} có giá trị từ {1} đến {2}")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:dd/MM/yyyy")]
            public Nullable<System.DateTime> NgayHetHanDC { get; set; }

            [Display(Name = "ID loại điều chỉnh")]
            public Nullable<int> IDLoaiDieuChinh { get; set; }
        }
    }
}