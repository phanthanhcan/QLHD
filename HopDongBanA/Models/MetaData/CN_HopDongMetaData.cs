using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HopDongMgr.Models
{
    [MetadataType(typeof(CN_HopDong.CN_HopDongMetaData))]
    public partial class CN_HopDong
    {
        class CN_HopDongMetaData
        {
            [Display(Name = "IDHD")]
            public int IDHD { get; set; }

            [Display(Name = "ID Loại hợp dồng")]
            public Nullable<int> IDLoai { get; set; }

            [Display(Name = "Mã công trình")]
            [MaxLength(length: 50, ErrorMessage = "{0} tối đa {1} ký tự")]
            public string MaCT { get; set; }

            [Display(Name = "Nội dung")]
            [MaxLength(length: 1024, ErrorMessage = "{0} tối đa {1} ký tự")]
            public string NoiDung { get; set; }

            [Display(Name = "Năm giao A")]
            [Range(minimum: 1990, maximum: 2200, ErrorMessage = "{0} có giá tri từ {1} dến {2}")]
            public Nullable<int> NamGiaoA { get; set; }

            [Display(Name = "ID ĐV thực hiện")]
            public Nullable<int> IDDV { get; set; }

            [Display(Name = "Giá trị gói thầu")]
            [Range(type: typeof(decimal), minimum: "99999", maximum: "999999999999999", ErrorMessage = "{0} có giá trị từ {1} đến {2}")]
            [Required(ErrorMessage = "Nhập {0}")]
            public Nullable<decimal> GiaTriGoiThau { get; set; }

            [Display(Name = "ID Hình thức hợp đồng")]
            public Nullable<int> IDHT { get; set; }

            [Display(Name = "Số hợp đồng")]
            [Required(ErrorMessage ="Nhập {0}")]
            [MaxLength(length: 200, ErrorMessage = "{0} tối đa {1} ký tự")]
            public string SoHopDong { get; set; }

            [Display(Name = "Ngày ký ")]
            [Range(type: typeof(DateTime), minimum: "1990/01/01", maximum: "2200/12/31", ErrorMessage = "{0} có giá trị từ {1} đến {2}")]
            [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString =  "0:dd/MM/yyyy")]
            public Nullable<System.DateTime> NgayKy { get; set; }

            [Display(Name = "Giá trị hợp đồng")]
            [Range(type: typeof(decimal), minimum: "99999", maximum: "999999999999999", ErrorMessage = "{0} có giá trị từ {1} đến {2}")]
            [Required(ErrorMessage = "Nhập {0}")]
            public Nullable<decimal> GiaTriHopDong { get; set; }

            [Display(Name = "Số ngày thực hiện")]
            [Range(minimum: 1, maximum: 18000, ErrorMessage = "{0} phải từ {1} đến {2}")]
            [Required(ErrorMessage = "Nhập {0}")]
            public Nullable<int> SoNgayThucHien { get; set; }

            [Display(Name = "Số ngày thi công")]
            [Range(minimum: 1, maximum: 18000, ErrorMessage = "{0} phải từ {1} đến {2}")]
            [Required(ErrorMessage = "Nhập {0}")]
            public Nullable<int> SoNgayThiCong { get; set; }

            [Display(Name = "Xử lý vi phạm")]
            [MaxLength(length: 1024, ErrorMessage = "{0} tối đa {1} ký tự")]
            public string XuLyViPham { get; set; }

            [Display(Name = "Giá trị phạt")]
            [Range(type: typeof(decimal), minimum: "99999", maximum: "999999999999999", ErrorMessage = "{0} có giá trị từ {1} đến {2}")]
            public Nullable<decimal> GiaTriPhat { get; set; }

            [Display(Name = "xử lý tranh chấp")]
            [MaxLength(length: 1024, ErrorMessage = "{0} tối đa {1} ký tự")]
            public string XuLyTranhChap { get; set; }

            [Display(Name = "Người tạo")]
            public Nullable<System.Guid> NguoiTao { get; set; }

            [Display(Name = "Ngày Tạo")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:dd/MM/yyyy")]
            public Nullable<System.DateTime> NgayTao { get; set; }

            [Display(Name = "Người cập nhật")]
            public string NguoiCapNhat { get; set; }

            [Display(Name = "Ngày cập nhật")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:dd/MM/yyyy")]
            public Nullable<System.DateTime> NgayCapNhat { get; set; }

            public Nullable<decimal> DTXD_IDHopDong { get; set; }

            [Display(Name = "Ngày hết hạn")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:dd/MM/yyyy")]
            [Range(type: typeof(DateTime), minimum: "1990/01/01", maximum: "2200/12/31", ErrorMessage = "{0} có giá trị từ {1} đến {2}")]
            public Nullable<System.DateTime> NgayHetHan { get; set; }

            [Display(Name = "ID Công trình")]
            public Nullable<decimal> IDCT { get; set; }

            [Display(Name = "Giá trị thực tế")]
            public Nullable<decimal> GiaTriThucTe { get; set; }

            [Display(Name = "Ngày hết hạn")]
            [Range(type: typeof(DateTime), minimum: "1990/01/01", maximum: "2200/12/31", ErrorMessage = "{0} có giá trị từ {1} đến {2}")]
            public Nullable<System.DateTime> NgayHetHanThucTe { get; set; }
        }
    }
}