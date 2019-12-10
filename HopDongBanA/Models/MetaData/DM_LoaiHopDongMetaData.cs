using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HopDongMgr.Models
{
    [MetadataType(typeof(DM_LoaiHopDong.DM_LoaiHopDongMetaData))]
    public partial class DM_LoaiHopDong
    {
        class DM_LoaiHopDongMetaData
        {
            [Display(Name = "ID loại hợp đồng")]
            public int IDLoai { get; set; }

            [Display(Name = "Tên loại hợp đồng")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "{0} không được để trống")]
            [MaxLength(200, ErrorMessage = "{0} Nhập tối đa {1} ký tự")]
            public string TenLoai { get; set; }

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

            [Display(Name = "Số thứ tự")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "{0} không được để trống")]
            [Range(1,1024,ErrorMessage ="{0} phải lớn hơn {1}")]
            public Nullable<int> STT { get; set; }
        }
    }
}
