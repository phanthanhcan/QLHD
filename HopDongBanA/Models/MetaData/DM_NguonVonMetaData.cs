using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HopDongMgr.Models
{
    [MetadataType(typeof(DM_NguonVon.DM_NguonVonMetaData))]
    public partial class DM_NguonVon
    {
        class DM_NguonVonMetaData
        {
            [Display(Name = "Mã nguồn vốn")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "{0} không được để trống")]
            [MaxLength(10, ErrorMessage = "{0} Nhập tối đa {1} ký tự")]
            public string MaNV { get; set; }

            [Display(Name = "Tên Nguồn vốn")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "{0} không được để trống")]
            [MaxLength(200, ErrorMessage = "{0} Nhập tối đa {1} ký tự")]
            public string TenNV { get; set; }

            [Display(Name = "Active")]
            public Nullable<bool> Khoa { get; set; }

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
