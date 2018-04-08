using Model.Dao;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_ThucPham.Areas.Admin.Controllers
{
    public class DanhMucTinTucController : BaseAdminController
    {
        // GET: Admin/DanhMucTinTuc
        public ActionResult DmucTin(string tukhoa, int page = 1, int pageSize = 20)
        {
            var dao = new DanhMucTinTucDAO();
            var model = dao.DsDanhMuc(tukhoa, page, pageSize);
            return View(model);
        }

        public ActionResult Them()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Them(DanhmucTinTuc dmtt)
        {
            SetViewBag();
            if (ModelState.IsValid)
            {
                var dao = new DanhMucTinTucDAO();
                long id = dao.Them(dmtt);
                if (id > 0)
                {
                    SetAlert("Thêm mới danh mục tin tức " + dmtt.TenDanhMuc + " thành công!", "success");
                    logger.Info("Thêm mới danh mục tin tức " + dmtt.TenDanhMuc + " thành công!");
                    return RedirectToAction("Them");
                }
                else
                {
                    SetAlert("Thêm mới danh mục tin tức không thành công!", "error");
                    logger.Error("Thêm mới danh mục tin tức " + dmtt.TenDanhMuc + " không thành công!");
                }
            }
            return View();
        }

        public ActionResult Sua(int id)
        {
            SetViewBag();
            if (Request["returnUrl"] != null)
                Session["returnUrl"] = Request["returnUrl"];
            var dmuctintuc = new DanhMucTinTucDAO().ViewDetailByID(id);
            return View(dmuctintuc);

        }

        [HttpPost]
        public ActionResult Sua(DanhmucTinTuc dmtt)
        {
            SetViewBag();
            if (ModelState.IsValid)
            {
                
                var dao = new DanhMucTinTucDAO();
                var result = dao.Sua(dmtt);
                if (result)
                {
                    SetAlert("Cập nhật danh mục tin tức " + dmtt.TenDanhMuc + " thành công!", "success");
                    logger.Info("Cập nhật danh mục tin tức " + dmtt.TenDanhMuc + " thành công!");
                    if (Session["returnUrl"] == null)
                        return RedirectToAction("Them");
                    else
                        return Redirect(Session["returnUrl"].ToString());
                }
                else
                {
                    SetAlert("Cập nhật danh mục tin tức " + dmtt.TenDanhMuc + " không thành công!", "error");
                    logger.Error("Cập nhật danh mục tin tức " + dmtt.TenDanhMuc + " không thành công!");
                }
            }
            return View();

        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new DanhMucTinTucDAO();
            var result = dao.Delete(id);
            switch (result)
            {
                case true:
                    {
                        SetAlert("Xóa danh mục tin tức " + id + " thành công!", "success");
                        logger.Info("Xóa danh mục tin tức " + id + " thành công!");
                        return RedirectToAction("Index");
                    }
                case false:
                    {
                        SetAlert("Xóa danh mục tin tức " + id + " không thành công!", "error");
                        logger.Error("Xóa danh mục tin tức " + id + " không thành công!");
                        return RedirectToAction("Index");
                    }
            }
            return RedirectToAction("DmucTin");
        }

        public void SetViewBag(long? selectedID = null)
        {
            var dao = new DanhMucTinTucDAO();
            ViewBag.ParentId = new SelectList(dao.DsDanhMucTinParentID(), "Id", "TenDanhMuc", selectedID);
        }

        [HttpPost]
        public JsonResult ThayDoiTrangThai(long id)
        {
            var result = new DanhMucTinTucDAO().ThayDoiTrangThai(id);
            return Json(new
            {
                trangthai = result
            });
        }
    }
}