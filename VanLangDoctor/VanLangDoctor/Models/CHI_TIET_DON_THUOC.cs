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
    
    public partial class CHI_TIET_DON_THUOC
    {
        public int ID_THUOC { get; set; }
        public int ID_DON_THUOC { get; set; }
        public Nullable<int> SO_LUONG { get; set; }
    
        public virtual DON_THUOC DON_THUOC { get; set; }
        public virtual THUOC THUOC { get; set; }
    }
}