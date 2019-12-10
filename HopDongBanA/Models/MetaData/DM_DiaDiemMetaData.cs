using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HopDongMgr.Models
{
    [MetadataType(typeof(DM_DiaDiem.DM_DiaDiemMetaData))]
    public partial class DM_DiaDiem
    {
        class DM_DiaDiemMetaData
        {
            [Display(Name = "Mã địa điểm")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "{0} không được để trống")]
            [MaxLength(10, ErrorMessage = "Nhập tối đa 10 ký tự")]
            public string MaDD { get; set; }

            [Display(Name = "Tên địa điểm")]
            [MaxLength(10, ErrorMessage = "{0}Nhập tối đa {1} ký tự")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "{0} không được để trống")]
            public string TenDD { get; set; }

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