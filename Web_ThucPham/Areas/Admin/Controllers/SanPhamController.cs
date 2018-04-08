using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Model.Dao;
using Model.EntityFramework;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Data;
using ClosedXML.Excel;
using System.Text;

namespace Web_ThucPham.Areas.Admin.Controllers
{
    public class SanPhamController : BaseAdminController
    {

        // GET: Admin/SanPham
        public ActionResult AllSanPham(string tukhoa, int page = 1, int pageSize = 20)
        {
            var dao = new SanPhamDAO();
            var model = dao.AllSanPham(tukhoa, page, pageSize);
            ViewBag.TuKhoa = tukhoa;
            return View(model);
        }

        // GET: Admin/SanPham
        public ActionResult SanPhamDaDuyet(string tukhoa, int page = 1, int pageSize = 20)
        {
            var dao = new SanPhamDAO();
            var model = dao.SanPhamDaDuyet(tukhoa, page, pageSize);
            ViewBag.TuKhoa = tukhoa;
            return View(model);
        }
        // GET: Admin/SanPham
        public ActionResult SanPhamChoDuyet(string tukhoa, int page = 1, int pageSize = 20)
        {
            var dao = new SanPhamDAO();
            var model = dao.SanPhamChoDuyet(tukhoa, page, pageSize);
            ViewBag.TuKhoa = tukhoa;
            return View(model);
        }

        // GET: Admin/SanPham
        public ActionResult SanPhamTop(string tukhoa, int page = 1, int pageSize = 20)
        {
            var dao = new SanPhamDAO();
            var model = dao.SanPhamTop(tukhoa, page, pageSize);
            ViewBag.TuKhoa = tukhoa;
            return View(model);
        }

        Web_ThucPhamDbContext db = new Web_ThucPhamDbContext();

        public ActionResult Them()
        {
            SetViewBag();
            SetViewBag1();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Them(SanPham sp)
        {
            SetViewBag();
            SetViewBag1();
            if (ModelState.IsValid)
            {
                var dao = new SanPhamDAO();
                long id = dao.Them(sp);
                if (id > 0)
                {
                    SetAlert("Thêm mới sản phẩm " + sp.Id + " thành công!", "success");
                    logger.Info("Thêm mới sản phẩm " + sp.Id + " thành công!");              
                }
                else
                {
                    SetAlert("Thêm mới sản phẩm " + sp.Id + " không thành công!", "error");
                    logger.Error("Thêm mới sản phẩm " + sp.Id + " không thành công!");                  
                }
            }
            return View();
        }

        public ActionResult Sua(int id)
        {
            SetViewBag();
            SetViewBag1();
            if (Request["returnUrl"] != null)
                Session["returnUrl"] = Request["returnUrl"];
            var sanpham = new SanPhamDAO().ViewDetailByID(id);
            return View(sanpham);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sua(SanPham sp)
        {
            SetViewBag();
            SetViewBag1();
            if (ModelState.IsValid)
            {

                var dao = new SanPhamDAO();
                var result = dao.Sua(sp);
                if (result)
                {
                    logger.Info("Cập nhật sản phẩm " + sp.Id + " thành công!");
                    SetAlert("Cập nhật sản phẩm " + sp.Id + " thành công!", "success");
                    if (Session["returnUrl"] == null)
                        return RedirectToAction("Them");
                    else
                        return Redirect(Session["returnUrl"].ToString());
                }
                else
                {
                    logger.Error("Cập nhật sản phẩm " + sp.Id + " không thành công!");
                    SetAlert("Cập nhật sản phẩm không thành công!", "error");
                }
            }
            return View();

        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new SanPhamDAO();
            var result = dao.Delete(id);
            switch (result)
            {
                case true:
                    {
                        SetAlert("Xóa sản phẩm " + id + " thành công!", "success");
                        logger.Info("Xóa sản phẩm " + id + " thành công!");
                        return RedirectToAction("Index");
                    }
                case false:
                    {
                        SetAlert("Xóa sản phẩm " + id + " không thành công!", "error");
                        logger.Error("Xóa sản phẩm " + id + " không thành công!");
                        return RedirectToAction("Index");
                    }
            }
            return View("AllSanPham");
        }

        public void SetViewBag(long? selectedID = null)
        {
            var dao = new TaiKhoanDAO();
            ViewBag.IdNguoiTao = new SelectList(dao.dsTaiKhoan(), "Id", "TenHienThi", selectedID);
        }

        public void SetViewBag1(long? selectedID = null)
        {
            var dao = new DanhMucSanPhamDAO();
            ViewBag.IdDanhMucSanPham = new SelectList(dao.DsDanhMucSP(), "Id", "TenDanhMuc", selectedID);
        }

        [HttpPost]
        public JsonResult ThayDoiTrangThai(long id)
        {
            var result = new SanPhamDAO().ThayDoiTrangThai(id);
            return Json(new
            {
                trangthai = result
            });
        }
    }
}