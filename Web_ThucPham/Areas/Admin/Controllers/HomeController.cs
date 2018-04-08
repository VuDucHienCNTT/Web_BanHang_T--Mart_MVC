using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_ThucPham.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {        
            return View();
        }
    }
}