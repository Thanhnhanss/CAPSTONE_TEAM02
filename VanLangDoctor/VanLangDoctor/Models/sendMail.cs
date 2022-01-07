using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VanLangDoctor.Models
{
    public class sendMail
    {
        public string from { get; set; }
        public string to { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }
}