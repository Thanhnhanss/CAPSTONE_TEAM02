using System.Web.Mvc;

namespace VanLangDoctor.Areas.User
{
    public class UserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                  "luutk",
                  "tai-khoan/cap-nhat/{id}",
                  new { controller = "CapnhatTK", action = "Luu", id = UrlParameter.Optional }
              );
            context.MapRoute(
                  "capnhattk",
                  "tai-khoan/cap-nhat-tai-khoan/{id}",
                  new { controller = "CapnhatTK", action = "CapnhatTK", id = UrlParameter.Optional }
              );
            context.MapRoute(
               "chitietbacsi",
               "trang-chu/bac-si/thong-tin-bac-si-{id}",
               new { controller = "ThongtinBacsi", action = "thongtinbacsi", id = UrlParameter.Optional }
           );

            context.MapRoute(
                "trangchu",
                "trang-chu",
                new { controller = "HomeUser", action = "HomeUser", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "bacsi",
                "trang-chu/bac-si",
                new { controller = "ThongtinBacsi", action = "DanhSachBacsi", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "User_default",
                "User/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}