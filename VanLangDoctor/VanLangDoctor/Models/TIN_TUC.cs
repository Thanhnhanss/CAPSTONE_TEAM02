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
    
    public partial class TIN_TUC
    {
        public int ID_TIN_TUC { get; set; }
        public string TEN_BAI_VIET { get; set; }
        public System.DateTime NGAY_DANG { get; set; }
        public string NOI_DUNG { get; set; }
        public string TAC_GIA { get; set; }
        public string SEO_TITLE { get; set; }
        public string HINH_ANH { get; set; }
        public int CountViews { get; set; }
        public Nullable<int> ID_Danhmuc_tin { get; set; }
    
        public virtual DANH_MUC_TIN DANH_MUC_TIN { get; set; }
    }
}
