using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EntityFramework;


namespace Web_ThucPham.Areas.Admin.Controllers
{
    public class TinTucController : BaseAdminController
    {

        // GET: Admin/TinTuc/AllTin
        public ActionResult AllTin(string tukhoa, int page = 1, int pageSize = 20)
        {
            var dao = new TinTucDAO();
            var model = dao.AllTin(tukhoa, page, pageSize);
            ViewBag.TuKhoa = tukhoa;
            return View(model);
        }

        // GET: Admin/TinTuc/TinDaDuyet
        public ActionResult TinDaDuyet(string tukhoa, int page = 1, int pageSize = 20)
        {
            var dao = new TinTucDAO();
            var model = dao.TinDaDuyet(tukhoa, page, pageSize);
            ViewBag.TuKhoa = tukhoa;
            return View(model);
        }

        // GET: Admin/TinTuc/TinChoDuyet
        public ActionResult TinChoDuyet(string tukhoa, int page = 1, int pageSize = 20)
        {
            var dao = new TinTucDAO();
            var model = dao.TinChoDuyet(tukhoa, page, pageSize);
            ViewBag.TuKhoa = tukhoa;
            return View(model);
        }

        public ActionResult Them()
        {

            SetViewBag1();
            SetViewBag();

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Them(TinTuc tt)
        {
            SetViewBag1();
            SetViewBag();
            if (ModelState.IsValid)
            {

                var dao = new TinTucDAO();
                tt.LuotXem = 0;
                tt.NgayTao = DateTime.Now;
                tt.NgaySua = null;
                long id = dao.Them(tt);
                if (id > 0)
                {
                    logger.Info("Thêm mới tin tức " + tt.Id + " thành công!");
                    SetAlert("Thêm mới tin thành công!", "success");
                    return RedirectToAction("Them");
                }
                else
                {
                    logger.Info("Thêm mới tin tức không thành công!");
                    SetAlert("Thêm mới không thành công!", "error");
                }
            }
            return View();
        }

        public ActionResult Sua(int id)
        {
            SetViewBag1();
            SetViewBag();
            if (Request["returnUrl"] != null)
                Session["returnUrl"] = Request["returnUrl"];
            var tintuc = new TinTucDAO().ViewDetailByID(id);
            return View(tintuc);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sua(TinTuc tt)
        {
            SetViewBag1();
            SetViewBag();
            if (ModelState.IsValid)
            {
                var dao = new TinTucDAO();
                var result = dao.Sua(tt);
                if (result)
                {
                    logger.Info("Cập nhật tin tức " + tt.Id + " thành công!");
                    SetAlert("Sửa tin tức thành công!", "success");
                    if (Session["returnUrl"] == null)
                        return RedirectToAction("Them");
                    else
                        return Redirect(Session["returnUrl"].ToString());
                }
                else
                {
                    logger.Info("Cập nhật tin tức không thành công!");
                    SetAlert("Sửa không thành công!", "error");
                    ModelState.AddModelError("", "Sửa không thành công!");
                }
            }
            return View();

        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new TinTucDAO();
            var result = dao.Delete(id);
            switch (result)
            {
                case true:
                    {
                        SetAlert("Xóa tin tức " + id + " thành công!", "success");
                        logger.Info("Xóa tin tức " + id + " thành công!");
                        return RedirectToAction("Index");
                    }
                case false:
                    {
                        SetAlert("Xóa tin tức " + id + " không thành công!", "error");
                        logger.Error("Xóa  tin tức " + id + " không thành công!");
                        return RedirectToAction("Index");
                    }
            }
            return View("AllTin");

        }

        public void SetViewBag(long? selectedID = null)
        {
            var dao = new DanhMucTinTucDAO();
            ViewBag.IdDanhMucTinTuc = new SelectList(dao.DsDanhMucTin(), "Id", "TenDanhMuc", selectedID);
        }

        public void SetViewBag1(long? selectedID = null)
        {
            var dao = new TaiKhoanDAO();
            ViewBag.IdNguoiTao = new SelectList(dao.dsTaiKhoan(), "Id", "TenHienThi", selectedID);
        }

        [HttpPost]
        public JsonResult ThayDoiTrangThai(long id)
        {
            var result = new TinTucDAO().ThayDoiTrangThai(id);
            return Json(new
            {
                trangthai = result
            });
        }

    }
}