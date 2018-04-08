namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BinhLuan")]
    public partial class BinhLuan
    {
        public long Id { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "Bạn chưa nhập nội dung bình luận")]
        public string NoiDung { get; set; }

        public DateTime? NgayBinhLuan { get; set; }

        public bool TrangThai { get; set; }

        [StringLength(250)]
        public string Url { get; set; }

        public int? IdNguoiBluan { get; set; }

        public long? IdSanPham { get; set; }

        public long? IdTinTuc { get; set; }

        public virtual SanPham SanPham { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }

        public virtual TinTuc TinTuc { get; set; }
    }
}
