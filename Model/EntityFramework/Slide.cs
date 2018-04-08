namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        public long Id { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Bạn chưa chọn hình ảnh")]
        public string HinhAnh { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Bạn chưa nhập link slide")]
        public string LinkSlide { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Bạn chưa nhập mô tả slide")]
        public string MoTaSlide { get; set; }

        public bool TrangThai { get; set; }

        public int? IdNguoiTao { get; set; }

        public DateTime? NgayTao { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
