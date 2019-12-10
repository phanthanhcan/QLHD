using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HopDongMgr.Models
{
    [MetadataType(typeof(CN_ThanhLy.CN_ThanhLyMetaData))]
    public partial class CN_ThanhLy
    {
        class CN_ThanhLyMetaData
        {
            [Display(Name = "ID thanh lý")]
            public int IDTL { get; set; }

            [Display(Name = "ID hợp đồng")]
            public Nullable<int> IDHD { get; set; }

            [Display(Name = "Hoàn thành")]
            public Nullable<bool> IsHoanThanh { get; set; }

            [Display(Name = "Ngày thanh lý")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:dd/MM/yyyy")]
            public Nullable<System.DateTime> NgayThanhLy { get; set; }

            [Display(Name = "Giá trị thanh lý")]
            [Range(type: typeof(decimal), minimum: "99999", maximum: "999999999999999", ErrorMessage = "{0} có giá trị từ {1} đến {2}")]
            [Required(ErrorMessage = "Nhập {0}")]
            public Nullable<decimal> GiaTriThanhLy { get; set; }

            [Display(Name = "Giá trị quyết toán")]
            [Range(type: typeof(decimal), minimum: "99999", maximum: "999999999999999", ErrorMessage = "{0} có giá trị từ {1} đến {2}")]
            [Required(ErrorMessage = "Nhập {0}")]
            public Nullable<decimal> GiaTriQuyetToan { get; set; }

            [Display(Name = "Ngày tổng nghiệm thu")]
            [Range(type: typeof(DateTime), minimum: "1990/01/01", maximum: "2200/12/31", ErrorMessage = "{0} có giá trị từ {1} đến {2}")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:dd/MM/yyyy")]
            public Nullable<System.DateTime> NgayTongNghiemThu { get; set; }

            [Display(Name = "Người tạo")]
            public string NguoiTao { get; set; }

            [Display(Name = "Ngày tạo")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:dd/MM/yyyy")]
            public Nullable<System.DateTime> NgayTao { get; set; }

            [Display(Name = "Người cập nhật")]
            public string NguoiCapNhat { get; set; }

            [Display(Name = "Ngày cập nhật")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:dd/MM/yyyy")]
            public Nullable<System.DateTime> NgayCapNhat { get; set; }
        }
    }
}