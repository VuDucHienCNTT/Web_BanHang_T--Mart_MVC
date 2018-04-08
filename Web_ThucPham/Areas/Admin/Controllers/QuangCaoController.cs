using Model.Dao;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_ThucPham.Areas.Admin.Controllers
{
    public class QuangCaoController : BaseAdminController
    {
        // GET: Admin/QuangCao
        public ActionResult DanhSachQuangCao(string tukhoa, int page = 1, int pageSize = 30)
        {
            var dao = new QuangCaoDAO();
            var model = dao.DanhSachQuangCao(tukhoa, page, pageSize);
            ViewBag.TuKhoa = tukhoa;
            return View(model);
        }


        public ActionResult Them()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Them(QuangCao qc)
        {
            SetViewBag();
            if (ModelState.IsValid)
            {
                var dao = new QuangCaoDAO();

                long id = dao.Them(qc);
                if (id > 0)
                {
                    SetAlert("Thêm mới quảng cáo " + qc.Id + " thành công!", "success");
                    logger.Info("Thêm mới quảng cáo " + qc.Id + " thành công!");
                    return RedirectToAction("Them");
                }
                else
                {
                    SetAlert("Thêm mới quảng cáo " + qc.Id + " không thành công!", "error");
                    logger.Error("Thêm mới quảng cáo " + qc.Id + " không thành công!");
                }
            }
            return View();
        }


        public ActionResult Sua(int id)
        {
            SetViewBag();
            if (Request["returnUrl"] != null)
                Session["returnUrl"] = Request["returnUrl"];
            var quangcao = new QuangCaoDAO().ViewDetailByID(id);
            return View(quangcao);
        }

        [HttpPost]
        public ActionResult Sua(QuangCao qc)
        {
            SetViewBag();
            if (ModelState.IsValid)
            {
                var dao = new QuangCaoDAO();
                var result = dao.Sua(qc);
                if (result)
                {
                    SetAlert("Cập nhật quảng cáo " + qc.Id + " thành công", "success");
                    logger.Info("Thêm mới quảng cáo " + qc.Id + " thành công!");
                    if (Session["returnUrl"] == null)
                        return Redirect("/Admin/Home/Index");
                    else
                        return Redirect(Session["returnUrl"].ToString());
                }
                else
                {
                    SetAlert("Cập nhật quảng cáo " + qc.Id + " không thành công", "error");
                    logger.Error("Cập nhật quảng cáo " + qc.Id + " không thành công!");
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }

            }
            return View();

        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new QuangCaoDAO();
            var result = dao.Delete(id);
            switch (result)
            {
                case true:
                    {
                        SetAlert("Xóa quảng cáo " + id + " thành công", "success");
                        logger.Info("Xóa quảng cáo " + id + " thành công!");
                        return RedirectToAction("Index");
                    }
                case false:
                    {
                        SetAlert("Xóa quảng cáo " + id + " không thành công", "error");
                        logger.Error("Xóa quảng cáo " + id + " không thành công!");
                        return RedirectToAction("Index");
                    }
            }
            return RedirectToAction("DanhSachQuangCao");
        }


        public void SetViewBag(long? selectedID = null)
        {
            var dao = new TaiKhoanDAO();
            ViewBag.IdNguoiTao = new SelectList(dao.dsTaiKhoan(), "Id", "TenHienThi", selectedID);
        }


        [HttpPost]
        public JsonResult ThayDoiTrangThai(long id)
        {
            var result = new QuangCaoDAO().ThayDoiTrangThai(id);
            return Json(new
            {
                trangthai = result
            });
        }
    }
}