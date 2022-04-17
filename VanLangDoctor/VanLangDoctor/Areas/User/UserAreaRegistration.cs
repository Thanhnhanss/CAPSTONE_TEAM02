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
                  "sokhambenh",
                  "tai-khoan/ho-so-benh-an-{id_skb}",
                  new { controller = "HealthRecord", action = "HealthRecord", id_skb = UrlParameter.Optional }
              );
            context.MapRoute(
                  "feedback",
                  "tai-khoan/danh-gia-va-phan-hoi/{id}",
                  new { controller = "FeedBack", action = "Create", id = UrlParameter.Optional }
              );
            context.MapRoute(
                  "luutk",
                  "tai-khoan/cap-nhat/{id}",
                  new { controller = "CapnhatTK", action = "Luu", id = UrlParameter.Optional }
              );
            context.MapRoute(
                  "capnhattk",
                  "tai-khoan/cap-nhat-tai-khoan/{id_user}",
                  new { controller = "CapnhatTK", action = "CapnhatTK", id_user = UrlParameter.Optional }
              );
            context.MapRoute(
                  "chitietthuoc",
                  "trang-chu/thuoc/{tenthuoc}-{ID_THUOC}",
                  new { controller = "Thuoc", action = "Details", ID_THUOC = UrlParameter.Optional }
              );
            context.MapRoute(
                  "thuoc",
                  "trang-chu/thuoc",
                  new { controller = "Thuoc", action = "Index", id = UrlParameter.Optional }
              );
            context.MapRoute(
                "gioithieu",
                "gioi-thieu",
                new { controller = "HomeUser", action = "GioiThieu", id = UrlParameter.Optional }
            );
            context.MapRoute(
                  "chitiet-tintuc",
                  "trang-chu/tin-tuc/{tenbaiviet}-{ID_TIN_TUC}",
                  new { controller = "TinTuc", action = "Details", ID_TIN_TUC = UrlParameter.Optional }
              );
            context.MapRoute(
                  "tintuc",
                  "trang-chu/tin-tuc",
                  new { controller = "TinTuc", action = "Tin_Tuc", id = UrlParameter.Optional }
              );
            context.MapRoute(
                  "formdkttvv",
                  "trang-chu/dang-ky-ung-tuyen",
                  new { controller = "DANG_KY", action = "Create", id = UrlParameter.Optional }
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