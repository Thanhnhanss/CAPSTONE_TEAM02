//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VanLangDoctor.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BACSI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BACSI()
        {
            this.DON_THUOC = new HashSet<DON_THUOC>();
            this.BENH_NHAN = new HashSet<BENH_NHAN>();
        }
    
        public int ID_BACSI { get; set; }
        public string TEN_BACSI { get; set; }
        public Nullable<int> SDT_BACSI { get; set; }
        public Nullable<System.DateTime> NGAYSINH_BACSI { get; set; }
        public Nullable<int> TUOI { get; set; }
        public string EMAIL { get; set; }
        public Nullable<bool> GIOI_TINH { get; set; }
        public string CHUC_VU { get; set; }
        public string KINH_NGHIEM { get; set; }
        public string NGAY_TRUC { get; set; }
        public string BHYT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DON_THUOC> DON_THUOC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BENH_NHAN> BENH_NHAN { get; set; }
    }
}