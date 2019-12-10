using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HopDongMgr.Models
{
    [MetadataType(typeof(DM_DonViThucHien.DM_DonViThucHienMetaData))]
    public partial class DM_DonViThucHien
    {
        class DM_DonViThucHienMetaData
        {
            [Display(Name = "ID đơn vỉ thực hiện")]
            public int IDDV { get; set; }

            [Display(Name = "Tên đơn vị thực hiện")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "{0} không được để trống")]
            [MaxLength(200, ErrorMessage = "{0} Nhập tối đa {1} ký tự")]
            public string TenDV { get; set; }

            [Display(Name = "Địa chỉ")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "{0} không được để trống")]
            [MaxLength(200, ErrorMessage = "{0} Nhập tối đa {1} ký tự")]
            public string DiaChi { get; set; }

            [Display(Name = "Active")]
            public bool Khoa { get; set; }

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
