using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Model.Dao;

namespace Web_ThucPham.Areas.Admin.Controllers
{
    public class BinhLuanController : BaseAdminController
    {
        // GET: Admin/BinhLuan/BinhLuanSanPham
        public ActionResult BinhLuanSanPham(string tukhoa, int page = 1, int pageSize = 30)
        {
            var dao = new BinhLuanDAO();
            var model = dao.BinhLuanSanPham(tukhoa, page, pageSize);
            ViewBag.TuKhoa = tukhoa;
            return View(model);
        }

        // GET: Admin/BinhLuan/BinhLuanTinTuc
        public ActionResult BinhLuanTinTuc(string tukhoa, int page = 1, int pageSize = 30)
        {
            var dao = new BinhLuanDAO();
            var model = dao.BinhLuanTinTuc(tukhoa, page, pageSize);
            ViewBag.TuKhoa = tukhoa;
            return View(model);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new BinhLuanDAO();
            var result = dao.Delete(id);
            switch (result)
            {
                case true:
                    {
                        SetAlert("Xóa bình luận " + id + " thành công!", "success");
                        logger.Info("Xóa bình luận " + id + " thành công!");
                        return RedirectToAction("Index");
                    }
                case false:
                    {
                        SetAlert("Xóa bình luận " + id + " không thành công!", "error");
                        logger.Error("Xóa bình luận " + id + " không thành công!");
                        return RedirectToAction("Index");
                    }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ThayDoiTrangThai(long id)
        {
            var result = new BinhLuanDAO().ThayDoiTrangThai(id);
            return Json(new
            {
                trangthai = result
            });
        }
    }
}