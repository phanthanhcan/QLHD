using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HopDongMgr.Models
{
    [MetadataType(typeof(DM_HinhThucHopDong.DM_HinhThucHopDongMetaData))]
    public partial class DM_HinhThucHopDong
    {
        class DM_HinhThucHopDongMetaData
        {
            [Display(Name = "ID hình thức hợp đồng")]
            public int IDHT { get; set; }

            [Display(Name = "Tên hình thức hợp đồng")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "{0} không được để trống")]
            [MaxLength(200, ErrorMessage = "{0} Nhập tối đa {1} ký tự")]
            public string TenHinhThuc { get; set; }

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
