using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HopDongMgr.Models
{
    [MetadataType(typeof(HT_LichSuHoatDong.HT_LichSuHoatDongMetaData))]
    public partial class HT_LichSuHoatDong
    {
        class HT_LichSuHoatDongMetaData
        {
            [DataType(DataType.DateTime)]
            [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}")]
            public Nullable<System.DateTime> NgayThucHien;


        }
    }
}