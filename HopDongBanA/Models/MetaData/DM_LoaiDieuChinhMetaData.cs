using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HopDongMgr.Models
{
    [MetadataType(typeof(DM_LoaiDieuChinh.DM_LoaiDieuChinhMetaData))]
    public partial class DM_LoaiDieuChinh
    {
        class DM_LoaiDieuChinhMetaData
        {
            [Display(Name = "ID loại điều chỉnh")]
            public int IDLoaiDieuChinh { get; set; }

            [Display(Name = "Mã loại điều chỉnh")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "{0} không được để trống")]
            [MaxLength(50, ErrorMessage = "{0} Nhập tối đa {1} ký tự")]
            public string MaLoaiDieuChinh { get; set; }

            [Display(Name = "Tên loại điều chỉnh")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "{0} không được để trống")]
            [MaxLength(255, ErrorMessage = "{0} Nhập tối đa {1} ký tự")]
            public string TenLoaiDieuChinh { get; set; }

            [Display(Name = "Active")]
            public Nullable<bool> Khoa { get; set; }




        }
    }
}