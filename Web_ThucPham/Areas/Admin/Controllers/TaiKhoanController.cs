using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Model.EntityFramework;
using Web_ThucPham.Areas.Admin.Code;

namespace Web_ThucPham.Areas.Admin.Controllers
{
    public class TaiKhoanController : BaseAdminController
    {
        // GET: Admin/TaiKhoan/TkAdmin
        public ActionResult TkAdmin(string tukhoa, int page = 1, int pageSize = 20)
        {
            var dao = new TaiKhoanDAO();
            var model = dao.TatCaTkAdmin(tukhoa, page, pageSize);
            ViewBag.TuKhoa = tukhoa;
            return View(model);
        }

        // GET: Admin/TaiKhoan/TkNhanVien
        public ActionResult TkNhanVien(string tukhoa, int page = 1, int pageSize = 20)
        {
            var dao = new TaiKhoanDAO();
            var model = dao.TatCaTkNhanVien(tukhoa, page, pageSize);
            ViewBag.TuKhoa = tukhoa;
            return View(model);
        }

        // GET: Admin/TaiKhoan/TkKhachHang
        public ActionResult TkKhachHang(string tukhoa, int page = 1, int pageSize = 20)
        {
            var dao = new TaiKhoanDAO();
            var model = dao.TatCaTkKhachHang(tukhoa, page, pageSize);
            ViewBag.TuKhoa = tukhoa;
            return View(model);
        }


        public ActionResult Them()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Them(TaiKhoan tk)
        {
            if (ModelState.IsValid)
            {
                var dao = new TaiKhoanDAO();
                tk.MatKhau = Md5.EncryptMD5(tk.EmailDangNhap + tk.MatKhau);
                tk.NgayDangKy = DateTime.Now;
                long id = dao.Them(tk);
                if (id > 0)
                {
                    SetAlert("Thêm mới tài khoản " + tk.TenHienThi + " thành công!", "success");
                    logger.Info("Thêm mới tài khoản " + tk.TenHienThi + " thành công!");
                }
                else
                {
                    SetAlert("Thêm mới tài khoản " + tk.TenHienThi + " không thành công!", "error");
                    logger.Error("Thêm mới tài khoản " + tk.TenHienThi + " không thành công!");
                }

            }
            return View();
        }

        public ActionResult Sua(int id)
        {
            if (Request["returnUrl"] != null)
                Session["returnUrl"] = Request["returnUrl"];
            var taikhoan = new TaiKhoanDAO().ViewDetailByID(id);
            return View(taikhoan);
        }

        [HttpPost]
        public ActionResult Sua(TaiKhoan tk)
        {
            Web_ThucPhamDbContext db = new Web_ThucPhamDbContext();
            if (ModelState.IsValid)
            {
                var dao = new TaiKhoanDAO();
                var result = dao.Sua(tk);
                if (result)
                {
                    SetAlert("Cập nhật tài khoản " + tk.TenHienThi + " thành công", "success");
                    logger.Info("Cập nhật tài khoản " + tk.TenHienThi + " thành công!");
                    if (Session["returnUrl"] == null)
                        return Redirect("/Admin/Home/Index");
                    else
                        return Redirect(Session["returnUrl"].ToString());
                }
                else
                {
                    SetAlert("Cập nhật tài khoản " + tk.TenHienThi + " không thành công", "error");
                    logger.Info("Cập nhật tài khoản " + tk.TenHienThi + " không thành công!");
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }

            }
            return View();

        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new TaiKhoanDAO();
            var result = dao.Delete(id);
            switch (result)
            {
                case true:
                    {
                        SetAlert("Xóa tài khoản " + id + " thành công", "success");
                        logger.Info("Xóa tài khoản " + id + " thành công!");
                        return RedirectToAction("Index");
                    }
                case false:
                    {
                        SetAlert("Xóa tài khoản " + id + " không thành công", "error");
                        logger.Error("Xóa tài khoản " + id + " không thành công!");
                        return RedirectToAction("Index");
                    }
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult ThayDoiTrangThai(long id)
        {
            var result = new TaiKhoanDAO().ThayDoiTrangThai(id);
            return Json(new
            {
                trangthai = result
            });
        }
    }
}