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
    
    public partial class DANG_KY
    {
        public int ID { get; set; }
        public string HO_TEN { get; set; }
        public System.DateTime NGAY_SINH { get; set; }
        public string GIOI_TINH { get; set; }
        public string NGHE_NGHIEP { get; set; }
        public string MUC_TIEU { get; set; }
        public string HOC_VAN { get; set; }
        public string EMAIL { get; set; }
        public string CHUNG_CHI { get; set; }
        public string SDT { get; set; }
        public Nullable<int> ID_KHOA { get; set; }
    
        public virtual KHOA KHOA { get; set; }
    }
}