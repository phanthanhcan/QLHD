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
    
    public partial class HT_CHUCVU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HT_CHUCVU()
        {
            this.HT_NguoiDung = new HashSet<HT_NguoiDung>();
        }
    
        public System.Guid Id { get; set; }
        public string Ten { get; set; }
        public string Ma { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HT_NguoiDung> HT_NguoiDung { get; set; }
    }
}
