using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_ThucPham.Areas.Nhanvien.Controllers
{
    public class HomeController : BaseNhanVienController
    {
        // GET: Nhanvien/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}