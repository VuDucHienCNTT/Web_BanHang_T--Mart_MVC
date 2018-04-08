using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_ThucPham.Areas.Admin.Models
{
    public class DangnhapModel
    {
        [DisplayName("Email đăng nhập")]
        [Required(ErrorMessage = "Vui lòng nhập email!")]
        public string email { get; set; }

        [StringLength(20, ErrorMessage = "Mật khẩu đa 20 ký tự!")]
        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
        public string matkhau { get; set; }

        public bool remember { set; get; }
    }
}