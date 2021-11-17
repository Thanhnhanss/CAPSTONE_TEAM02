using System.Web.Mvc;

namespace VanLangDoctor.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "thembs",
                "trang-chu-quan-ly/bac-si/them-moi",
                new { controller = "BACSIs", action = "Create", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "dsbs",
                "trang-chu-quan-ly/bac-si/danh-sach",
                new { controller = "BACSIs", action = "DanhSachBacsi", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "trangchuql",
                "trang-chu-quan-ly",
                new { controller = "HomeAdmin", action = "HomeAdmin", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}