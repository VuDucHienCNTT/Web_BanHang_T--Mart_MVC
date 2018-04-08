using DevExpress.Web.Mvc;
using FlexCel.Report;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ThucPham.Areas.Admin.Report;
using System.IO;
namespace Web_ThucPham.Areas.Admin.Controllers
{
    public class ThongKeController : BaseAdminController
    {
        // POST: Admin/ThongKe
        Web_ThucPhamDbContext db = new Web_ThucPhamDbContext();
        public ActionResult SanPham(string tukhoa, string tungay, string denngay)
        {
            ViewBag.tukhoa = tukhoa; ViewBag.tungay = tungay; ViewBag.denngay = denngay;
            IQueryable<SanPham> model = db.SanPhams;
            try
            {
                if (!string.IsNullOrEmpty(tukhoa))
                {
                    model = model.Where(n => n.TenSanPham.StartsWith(tukhoa) || n.TenSanPham.EndsWith(tukhoa) || n.TenSanPham.Contains(tukhoa));
                }
                if (!string.IsNullOrEmpty(tungay) || !string.IsNullOrEmpty(denngay))
                {
                    string dateFrom = tungay.Split('-')[1] + "/" + tungay.Split('-')[2] + "/" + tungay.Split('-')[0];
                    string dateTo = denngay.Split('-')[1] + "/" + denngay.Split('-')[2] + "/" + denngay.Split('-')[0];
                    var _tungay = Convert.ToDateTime(dateFrom);
                    var _denngay = Convert.ToDateTime(dateTo).AddDays(1).AddMilliseconds(-1);
                    model = model.Where(n => n.NgayTao >= _tungay && n.NgayTao <= _denngay);
                }
                ViewBag.count = model.Count();
                return View(model.Take(100).ToList());
            }
            catch(Exception ex)
            {
                SetAlert("Không có sản phẩm nào được tìm thấy!", "error");
                logger.Error("Không có sản phẩm nào được tìm thấy!" + ex.Message);
                return View();
            }
        }

        [HttpPost]
        public ActionResult ThongKeSP()
        {
            TempData["tukhoa"] = Request.Form["tukhoa"].ToString();
            TempData["tungay"] = Request.Form["tungay"].ToString();
            TempData["denngay"] = Request.Form["denngay"].ToString();
            return View();
        }

        public ActionResult DocumentViewerPartial()
        {
            TempData.Keep("tukhoa");
            TempData.Keep("tungay");
            TempData.Keep("denngay");
            ThongKeSanPham report = new ThongKeSanPham(TempData["tukhoa"].ToString(), TempData["tungay"].ToString(), TempData["denngay"].ToString());
            return PartialView("_DocumentViewerPartial", report);
        }

        public ActionResult DocumentViewerPartialExport()
        {
            TempData.Keep("tukhoa");
            TempData.Keep("tungay");
            TempData.Keep("dengay");
            ThongKeSanPham report = new ThongKeSanPham(TempData["tukhoa"].ToString(), TempData["tungay"].ToString(), TempData["denngay"].ToString());
            return DocumentViewerExtension.ExportTo(report, Request);
        }

        public ActionResult XuatExcel(string tungay, string denngay)
        {
            IQueryable<SanPham> model = db.SanPhams;
            if (!string.IsNullOrEmpty(tungay) || !string.IsNullOrEmpty(denngay))
            {
                string dateFrom = tungay.Split('-')[1] + "/" + tungay.Split('-')[2] + "/" + tungay.Split('-')[0];
                var _tungay = Convert.ToDateTime(dateFrom);
              
                if (!string.IsNullOrEmpty(denngay))
                {
                    string dateTo = denngay.Split('-')[1] + "/" + denngay.Split('-')[2] + "/" + denngay.Split('-')[0];
                    var _denngay = Convert.ToDateTime(dateTo).AddDays(1).AddMilliseconds(-1);
                    model = model.Where(n => n.NgayTao >= _tungay && n.NgayTao <= _denngay);
                }
                else
                {
                    string dateTo = DateTime.Now.ToShortDateString();
                    var _denngay = Convert.ToDateTime(dateTo).AddDays(1).AddMilliseconds(-1);
                    model = model.Where(n => n.NgayTao >= _tungay && n.NgayTao <= _denngay);
                }
                
                ViewBag.dem = model.Count();

                try
                {
                    if (model.Count() >= 1)
                    {
                        using (FlexCelReport fr = new FlexCelReport(true))
                        {
                            fr.SetValue("tungay", tungay);
                            fr.SetValue("denngay", denngay);
                            fr.AddTable("sanpham", model.ToList());
                            string uploadPath = Server.MapPath("~/Content/Template");

                            var luufile = @"E:/STYDY/DATN/Export";
                            //Nếu chưa tồn tại thì tạo ra folder mới
                            if (!Directory.Exists(luufile))
                                Directory.CreateDirectory(luufile);

                            fr.Run(
                                Path.Combine(uploadPath + "/Book.xlsx"),
                                Path.Combine(luufile + "/Sanpham_" + DateTime.Now.ToString("dd_MM_yy_hh_mm_ss") + ".xlsx")
                             );
                        }
                        SetAlert("Xuất thành công " + model.Count() + " sản phẩm!", "success");
                        logger.Info("Xuất thành công " + model.Count() + " sản phẩm!");
                    }
                    else
                    {
                        SetAlert("Không có sản phẩm nào được tìm thấy!", "error");
                        logger.Error("Không có sản phẩm nào được tìm thấy!");
                    }
                }
                catch (Exception ex)
                {
                    SetAlert("Xuất thống kê báo cáo thất bại!", "error");
                    logger.Error("Xuất thống kê báo cáo thất bại!" + ex.Message);
                }
            }
            return View("XuatExcel");
        }
    }
}