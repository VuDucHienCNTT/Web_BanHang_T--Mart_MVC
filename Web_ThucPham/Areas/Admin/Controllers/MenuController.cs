using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using PagedList;
using PagedList.Mvc;
using Model.EntityFramework;

namespace Web_ThucPham.Areas.Admin.Controllers
{
    public class MenuController : BaseAdminController
    {
        // GET: Admin/Menu
        public ActionResult DsMenu(string tukhoa, int page = 1, int pageSize = 20)
        {
            var dao = new MenuDAO();
            var model = dao.DsMenu(tukhoa, page, pageSize);
            ViewBag.TuKhoa = tukhoa;
            return View(model);
        }

        public ActionResult Them()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Them(Menu mn)
        {
            if (ModelState.IsValid)
            {
                var dao = new MenuDAO();
                long id = dao.Them(mn);
                if (id > 0)
                {
                    SetAlert("Thêm mới menu " + mn.TenMenu + " thành công!", "success");
                    logger.Info("Thêm mới menu " + mn.TenMenu + " thành công!");
                    return RedirectToAction("Them");
                }
                else
                {
                    SetAlert("Thêm mới menu " + mn.TenMenu + " không thành công!", "error");
                    logger.Error("Thêm mới menu " + mn.TenMenu + " không thành công!");
                }
            }
            return View();
        }

        public ActionResult Sua(int id)
        {
            if (Request["returnUrl"] != null)
                Session["returnUrl"] = Request["returnUrl"];
            var menu = new MenuDAO().ViewDetailByID(id);
            return View(menu);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sua(Menu mn)
        {

            if (ModelState.IsValid)
            {
                var dao = new MenuDAO();
                var result = dao.Sua(mn);
                if (result)
                {
                    SetAlert("Cập nhật menu " + mn.TenMenu + " thành công!", "success");
                    logger.Info("Cập nhật menu " + mn.TenMenu + " thành công!");
                    if (Session["returnUrl"] == null)
                        return RedirectToAction("Them");
                    else
                        return Redirect(Session["returnUrl"].ToString());
                }
                else
                {
                    SetAlert("Cập nhật menu " + mn.TenMenu + " không thành công!", "error");
                    logger.Error("Cập nhật menu " + mn.TenMenu + " không thành công!");
                    ModelState.AddModelError("", "Sửa không thành công!");
                }
            }
            return View();

        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new MenuDAO();
            var result = dao.Delete(id);
            switch (result)
            {
                case true:
                    {
                        SetAlert("Xóa menu " + id + " thành công", "success");
                        logger.Info("Xóa menu " + id + " thành công!");
                        return RedirectToAction("Index");
                    }
                case false:
                    {
                        SetAlert("Xóa menu " + id + " không thành công", "error");
                        logger.Error("Xóa menu " + id + " không thành công!");
                        return RedirectToAction("Index");
                    }
            }
            return RedirectToAction("DsMenu");
        }

        [HttpPost]
        public JsonResult ThayDoiTrangThai(long id)
        {
            var result = new MenuDAO().ThayDoiTrangThai(id);
            return Json(new
            {
                trangthai = result
            });
        }
    }
}