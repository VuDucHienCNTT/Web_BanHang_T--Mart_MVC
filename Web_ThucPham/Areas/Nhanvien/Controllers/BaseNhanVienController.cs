using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Web_ThucPham.Areas.Admin.Code;

namespace Web_ThucPham.Areas.Nhanvien.Controllers
{
    public class BaseNhanVienController : Controller
    {
        // GET: /Kiểm tra quyền nhân viên/
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sesU = (UserInfo)Session[CommonConstant.USER_SESSION];
            if (sesU == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Dangnhap", action = "Dangnhap", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}