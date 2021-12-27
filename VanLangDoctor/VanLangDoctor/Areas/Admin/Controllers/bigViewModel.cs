using System.Collections.Generic;
using VanLangDoctor.Models;

namespace VanLangDoctor.Areas.Admin.Controllers
{
    public class bigViewModel
    {
        public List<AspNetRole> Roles { get; set; }

        public List<AspNetUser> Users { get; set; }
    }
}