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
            this.DANH_GIA = new HashSet<DANH_GIA>();
            this.DAT_LICH = new HashSet<DAT_LICH>();
        }
    
        public int ID_BACSI { get; set; }
        public string TEN_BACSI { get; set; }
        public Nullable<System.DateTime> NGAYSINH_BS { get; set; }
        public string GIOI_TINH { get; set; }
        public string SDT { get; set; }
        public string HINH_ANH { get; set; }
        public string NGHE_NGHIEP { get; set; }
        public Nullable<int> ID_KHOA { get; set; }
        public Nullable<int> KINH_NGHIEM { get; set; }
        public string ID_Email { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DON_THUOC> DON_THUOC { get; set; }
        public virtual KHOA KHOA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DANH_GIA> DANH_GIA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DAT_LICH> DAT_LICH { get; set; }
    }
}
