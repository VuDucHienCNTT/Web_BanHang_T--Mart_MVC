using Model.Dao;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_ThucPham.Areas.Admin.Controllers
{
    public class DanhMucSanPhamController : BaseAdminController
    {
        // GET: Admin/DanhMucTinTuc
        public ActionResult DmucSanpham(string tukhoa, int page = 1, int pageSize = 20)
        {
            var dao = new DanhMucSanPhamDAO();
            var model = dao.DsDanhMuc(tukhoa, page, pageSize);
            ViewBag.TuKhoa = tukhoa;
            return View(model);
        }

        public ActionResult Them()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Them(DanhmucSanPham dmsp)
        {
            SetViewBag();
            if (ModelState.IsValid)
            {

                var dao = new DanhMucSanPhamDAO();
                long id = dao.Them(dmsp);
                if (id > 0)
                {
                    SetAlert("Thêm mới danh mục sản phẩm " + dmsp.TenDanhMuc + " thành công!", "success");
                    logger.Info("Thêm mới danh mục sản phẩm " + dmsp.TenDanhMuc + " thành công!");
                    return RedirectToAction("Them");
                }
                else
                {
                    SetAlert("Thêm mới danh mục sản phẩm " + dmsp.TenDanhMuc + " không thành công!", "error");
                    logger.Error("Thêm mới danh mục sản phẩm " + dmsp.TenDanhMuc + " không thành công!");
                }
            }
            return View();
        }

        public ActionResult Sua(int id)
        {
            SetViewBag();
            if (Request["returnUrl"] != null)
                Session["returnUrl"] = Request["returnUrl"];
            var dmucsanpham = new DanhMucSanPhamDAO().ViewDetailByID(id);
            return View(dmucsanpham);

        }

        [HttpPost]
        public ActionResult Sua(DanhmucSanPham dmsp)
        {
            SetViewBag();
            if (ModelState.IsValid)
            {

                var dao = new DanhMucSanPhamDAO();
                var result = dao.Sua(dmsp);
                if (result)
                {
                    SetAlert("Cập nhật danh mục sản phẩm " + dmsp.TenDanhMuc + " thành công!", "success");
                    logger.Info("Cập nhật danh mục sản phẩm " + dmsp.TenDanhMuc + " thành công!");
                    if (Session["returnUrl"] == null)
                        return RedirectToAction("Them");
                    else
                        return Redirect(Session["returnUrl"].ToString());
                }
                else
                {
                    SetAlert("Cập nhật danh mục sản phẩm " + dmsp.TenDanhMuc + " không thành công!", "error");
                    logger.Error("Cập nhật danh mục sản phẩm " + dmsp.TenDanhMuc + " không thành công!");
                }
            }
            return View();

        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new DanhMucSanPhamDAO();
            var result = dao.Delete(id);
            switch (result)
            {
                case true:
                    {
                        SetAlert("Xóa danh mục sản phẩm " + id + " thành công!", "success");
                        logger.Info("Xóa danh mục sản phẩm " + id + " thành công!");
                        return RedirectToAction("Index");
                    }
                case false:
                    {
                        SetAlert("Xóa danh mục sản phẩm " + id + " không thành công!", "error");
                        logger.Error("Xóa danh mục sản phẩm " + id + " không thành công!");
                        return RedirectToAction("Index");
                    }
            }
            return View("DmucSanpham");
        }

        public void SetViewBag(long? selectedID = null)
        {
            var dao = new DanhMucSanPhamDAO();
            ViewBag.ParentId = new SelectList(dao.DsDanhMucSPParentID(), "Id", "TenDanhMuc", selectedID);
        }

        [HttpPost]
        public JsonResult ThayDoiTrangThai(long id)
        {
            var result = new DanhMucSanPhamDAO().ThayDoiTrangThai(id);
            return Json(new
            {
                trangthai = result
            });
        }
    }
}