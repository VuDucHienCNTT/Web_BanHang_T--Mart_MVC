using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Model.EntityFramework;

namespace Web_ThucPham.Areas.Admin.Controllers
{
    public class SlideController : BaseAdminController
    {
        // GET: Admin/TinTuc/AllTin
        public ActionResult AllSlide(string tukhoa, int page = 1, int pageSize = 20)
        {
            var dao = new SlideDAO();
            var model = dao.DsSlide(tukhoa, page, pageSize);
            ViewBag.TuKhoa = tukhoa;
            return View(model);
        }

        public ActionResult Them()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Them(Slide sl)
        {
            SetViewBag();
            if (ModelState.IsValid)
            {
                var dao = new SlideDAO();
                sl.NgayTao = DateTime.Now;
                long id = dao.Them(sl);
                if (id > 0)
                {
                    SetAlert("Thêm mới slide " + sl.Id + " thành công!", "success");
                    logger.Info("Thêm mới slide " + sl.Id + " thành công!");
                    return RedirectToAction("Them");
                }
                else
                {
                    SetAlert("Thêm mới slide " + sl.Id + " không thành công!", "error");
                    logger.Error("Thêm mới slide " + sl.Id + " không thành công!");
                }
            }
            return View();
        }

        public ActionResult Sua(int id)
        {
            SetViewBag();
            if (Request["returnUrl"] != null)
                Session["returnUrl"] = Request["returnUrl"];
            var slide = new SlideDAO().ViewDetailByID(id);
            return View(slide);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sua(Slide sl)
        {
            SetViewBag();
            if (ModelState.IsValid)
            {

                var dao = new SlideDAO();
                var result = dao.Sua(sl);
                if (result)
                {
                    SetAlert("Cập nhật slide " + sl.Id + " thành công!", "success");
                    logger.Info("Cập nhật slide " + sl.Id + " thành công!");
                    if (Session["returnUrl"] == null)
                        return RedirectToAction("Them");
                    else
                        return Redirect(Session["returnUrl"].ToString());
                }
                else
                {
                    SetAlert("Cập nhật slide " + sl.Id + " không thành công!", "error");
                    logger.Error("Cập nhật slide " + sl.Id + " không thành công!");
                    ModelState.AddModelError("", "Sửa không thành công!");
                }
            }
            return View();

        }



        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new SlideDAO();
            var result = dao.Delete(id);
            switch (result)
            {
                case true:
                    {
                        SetAlert("Xóa slide " + id + " thành công!", "success");
                        logger.Info("Xóa slide " + id + " thành công!");
                        return RedirectToAction("Index");
                    }
                case false:
                    {
                        SetAlert("Xóa slide " + id + " không thành công!", "error");
                        logger.Error("Xóa slide " + id + " không thành công!");
                        return RedirectToAction("Index");
                    }
            }
            return RedirectToAction("AllSlide");
        }

        public void SetViewBag(long? SelectedId = null)
        {
            var dao = new TaiKhoanDAO();
            ViewBag.IdNguoiTao = new SelectList(dao.dsTaiKhoan(), "Id", "TenHienThi", SelectedId);
        }

        [HttpPost]
        public JsonResult ThayDoiTrangThai(long id)
        {
            var result = new SlideDAO().ThayDoiTrangThai(id);
            return Json(new
            {
                trangthai = result
            });
        }
    }
}