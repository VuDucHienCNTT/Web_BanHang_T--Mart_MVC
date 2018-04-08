using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Web_ThucPham.Areas.Admin.Code;

namespace Web_ThucPham.Areas.Admin.Controllers
{
    public class BaseAdminController : Controller
    {
       public readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: /Kiểm tra quyền admin/

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sesA = (UserInfo)Session[CommonConstant.ADMIN_SESSION];
            if (sesA == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Dangnhap", action = "Dangnhap", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }

        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}