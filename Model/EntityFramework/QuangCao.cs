namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuangCao")]
    public partial class QuangCao
    {
        public int Id { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage ="Bạn chưa nhập đường link quảng cáo")]
        [DataType(DataType.Url)]
        [Display(Name = "Link quảng cáo")]
        public string LinkQc { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "Bạn chưa chọn ảnh quảng cáo")]
        public string AnhQc { get; set; }

        public bool TrangThai { get; set; }

        public int IdNguoiTao { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
