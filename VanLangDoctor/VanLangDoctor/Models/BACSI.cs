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
            this.BENH_NHAN = new HashSet<BENH_NHAN>();
            this.DON_THUOC = new HashSet<DON_THUOC>();
        }
    
        public int ID_BACSI { get; set; }
        public string TEN_BACSI { get; set; }
        public System.DateTime NGAYSINH_BS { get; set; }
        public string GIOI_TINH { get; set; }
        public string SDT { get; set; }
        public string EMAIL { get; set; }
        public string HINH_ANH { get; set; }
        public string NGHE_NGHIEP { get; set; }
        public Nullable<int> ID_KHOA { get; set; }
        public Nullable<int> KINH_NGHIEM { get; set; }
        public Nullable<System.DateTime> NGAY_TRUC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BENH_NHAN> BENH_NHAN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DON_THUOC> DON_THUOC { get; set; }
        public virtual KHOA KHOA { get; set; }
    }
}
