using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ThucPham.Areas.Admin.Code;
using Web_ThucPham.Areas.Admin.Models;

namespace Web_ThucPham.Areas.Admin.Controllers
{
    public class DangnhapController : Controller
    {
       readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: Admin/Dangnhap
        public ActionResult Dangnhap()
        {
            // Admin:
            var sesA = (UserInfo)Session[CommonConstant.ADMIN_SESSION];
            
            // Nhân viên:
            var sesU = (UserInfo)Session[CommonConstant.USER_SESSION];

            //Khách hàng: 
            var sesC = (UserInfo)Session[CommonConstant.CUSTOMER_SESSION];

            if (sesA != null)
            {            
                return RedirectToAction("Index", "Home");
            }
            else if (sesU != null)
            {               
                return Redirect("/Nhanvien/Home/Index");
            }
            else if (sesC != null)
            {
                return Redirect("/Home/Index");
            }
            return View();
        }

        // POST: Admin/Dangnhap
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dangnhap(DangnhapModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new TaiKhoanDAO();
                var result = dao.DangNhap(model.email, Md5.EncryptMD5(model.email + model.matkhau));
                switch (result)
                {
                    //Đăng nhập quyền admin
                    case 1:
                        {

                            var user = new TaiKhoanDAO().GetByID(model.email);
                            var userSession = new UserInfo();
                            userSession.email = user.EmailDangNhap;
                            userSession.ID = user.Id;
                            Session.Add(CommonConstant.ADMIN_SESSION, userSession);
                            Session["USER_ID"] = userSession.ID;
                            logger.Info("Đăng nhập tài khoản Admin thành công!");
                            return RedirectToAction("Index", "Home");

                        }
                    //Đăng nhập nhân viên
                    case 2:
                        {
                            var userId = new TaiKhoanDAO().GetByID(model.email);
                            var userSession = new UserInfo();
                            userSession.email = userId.EmailDangNhap;
                            userSession.ID = userId.Id;
                            Session.Add(CommonConstant.USER_SESSION, userSession);
                            Session["USER_ID"] = userSession.ID;
                            logger.Info("Đăng nhập tài khoản Nhân viên thành công!");
                            return Redirect("/Nhanvien/Home/Index");
                        }
                    //Đăng nhập khách hàng
                    case 3:
                        {
                            var userId = new TaiKhoanDAO().GetByID(model.email);
                            var userSession = new UserInfo();
                            userSession.email = userId.EmailDangNhap;
                            userSession.ID = userId.Id;
                            Session.Add(CommonConstant.CUSTOMER_SESSION, userSession);
                            Session["USER_ID"] = userSession.ID;
                            logger.Info("Đăng nhập tài khoản Khách hàng thành công!");
                            return Redirect("/Home/Index");
                        }

                    //Đăng nhập trường hợp tài khoản không tồn tại
                    case 0:
                        {
                            logger.Error("Tài khoản này không tồn tại!");
                            ModelState.AddModelError("", "Tài khoản này không tồn tại!");
                            break;
                        }

                    //Đăng nhập trường hợp tài khoản bị khóa
                    case -1:
                        {
                            logger.Error("Tài khoản này đã bị khóa!");
                            ModelState.AddModelError("", "Tài khoản này đã bị khóa!");
                            break;

                        }

                    //Đăng nhập trường hợp sai mật khẩu
                    case -2:
                        {
                            logger.Error("Mật khẩu không chính xác!");
                            ModelState.AddModelError("", "Mật khẩu không chính xác!");
                            break;
                        }
                }

            }
            return View("Dangnhap");
        }


        public ActionResult Dangxuat()
        {
            Session.Clear();
            logger.Info("Đăng xuất tài khoản thành công!");
            return RedirectToAction("Dangnhap", "Dangnhap");
        }
    }
}