using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HopDongMgr.Models
{
    [MetadataType(typeof(DM_PHONG.DM_PHONGMetaData))]
    public partial class DM_PHONG
    {
        class DM_PHONGMetaData
        {
            [Display(Name = "ID Phòng")]
            public System.Guid Id { get; set; }

            [Display(Name = "Tên phòng")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "{0} không được để trống")]
            [MaxLength(length: 250, ErrorMessage = "{0} tối đa {1} ký tự")]
            public string Ten { get; set; }

            [Display(Name = "Mã đơn vị")]
            public string MaDV { get; set; }

            [Display(Name = "Tên viết tắt")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "{0} không được để trống")]
            [MaxLength(length: 50, ErrorMessage = "{0} tối đa {1} ký tự")]
            public string TenTat { get; set; }
        }
    }
}
