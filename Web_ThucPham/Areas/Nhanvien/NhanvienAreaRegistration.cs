using System.Web.Mvc;

namespace Web_ThucPham.Areas.Nhanvien
{
    public class NhanvienAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Nhanvien";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Nhanvien_default",
                "Nhanvien/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Web_ThucPham.Areas.Nhanvien.Controllers" }
            );
        }
    }
}