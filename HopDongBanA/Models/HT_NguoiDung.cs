//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HopDongMgr.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HT_NguoiDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HT_NguoiDung()
        {
            this.CN_HopDong = new HashSet<CN_HopDong>();
        }
    
        public System.Guid oid { get; set; }
        public string MaNguoiDung { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string AnhDaiDien { get; set; }
        public string MaDV { get; set; }
        public Nullable<System.Guid> IdNhom { get; set; }
        public Nullable<System.Guid> IdPhong { get; set; }
        public string GioiTinh { get; set; }
        public Nullable<System.Guid> ChucVu { get; set; }
        public Nullable<int> NamSinh { get; set; }
        public bool Active { get; set; }
        public string BiDanh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CN_HopDong> CN_HopDong { get; set; }
        public virtual HT_CHUCVU HT_CHUCVU { get; set; }
    }
}