using DevExpress.Web.Mvc;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ThucPham.Areas.Nhanvien.Report;

namespace Web_ThucPham.Areas.Nhanvien.Controllers
{
    public class DatHangController : BaseNhanVienController
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        Web_ThucPhamDbContext db = new Web_ThucPhamDbContext();
        // GET: Nhanvien/DatHang
        public ActionResult Index()
        {
            var model = db.ChiTietDatHangs.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult HoaDon()
        {
            logger.Info("Xuất hóa đơn thành công!");
            TempData["MaDatHang"] = Request.Form["txtIDDatHang"].ToString();
            return View();
        }

        public ActionResult DocumentViewerPartial()
        {
            TempData.Keep("MaDatHang");
            HoaDonDatHang report = new HoaDonDatHang(long.Parse(TempData["MaDatHang"].ToString()));
            return PartialView("_DocumentViewerPartial", report);
        }

        public ActionResult DocumentViewerPartialExport()
        {
            TempData.Keep("MaDatHang");
            HoaDonDatHang report = new HoaDonDatHang(long.Parse(TempData["MaDatHang"].ToString()));
            return DocumentViewerExtension.ExportTo(report, Request);
        }
    }
}