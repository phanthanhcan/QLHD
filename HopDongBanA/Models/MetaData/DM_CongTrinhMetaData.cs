using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HopDongMgr.Models
{
    [MetadataType(typeof(DM_CongTrinh.DM_CongTrinhMetaData))]
    public partial class DM_CongTrinh
    {
        class DM_CongTrinhMetaData
        {
            [Display(Name ="ID Công trình")]
            public decimal IDCT { get; set; }

            [Display(Name = "Mã Công trình")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "{0} không được để trống")]
            [MaxLength(50,ErrorMessage ="{0}Nhập tối đa {1} ký tự")]
            public string MaCT { get; set; }

            [Display(Name = "Tên Công trình")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "{0} không được để trống")]
            [MaxLength(500, ErrorMessage = "{0}Nhập tối đa {1} ký tự")]
            public string TenCT { get; set; }

            [Display(Name = "Mã nguồn vốn")]
            public string MaNV { get; set; }

            [Display(Name = "Mã địa điểm")]
            public string MaDD { get; set; }

            [Display(Name = "Active")]
            public Nullable<bool> Khoa { get; set; }

            [Display(Name = "Người tạo")]
            public string NguoiTao { get; set; }

            [Display(Name = "Ngày tạo")]
            public Nullable<System.DateTime> NgayTao { get; set; }

            [Display(Name = "Người cập nhật")]
            public string NguoiCapNhat { get; set; }

            [Display(Name = "Ngày cập nhật")]
            public Nullable<System.DateTime> NgayCapNhat { get; set; }




        }
    }
}