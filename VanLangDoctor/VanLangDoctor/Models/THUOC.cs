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
    
    public partial class THUOC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public THUOC()
        {
            this.DON_THUOC = new HashSet<DON_THUOC>();
        }
    
        public int ID_THUOC { get; set; }
        public string TEN_THUOC { get; set; }
        public string LIEU_LUONG { get; set; }
        public string MO_TA { get; set; }
        public string HINH_ANH { get; set; }
        public Nullable<int> ID_NSX { get; set; }
        public Nullable<int> ID_DANHMUC { get; set; }
    
        public virtual DANH_MUC_THUOC DANH_MUC_THUOC { get; set; }
        public virtual NHA_SAN_XUAT NHA_SAN_XUAT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DON_THUOC> DON_THUOC { get; set; }
    }
}
