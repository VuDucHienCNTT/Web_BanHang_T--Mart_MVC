namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TinTuc")]
    public partial class TinTuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TinTuc()
        {
            BinhLuans = new HashSet<BinhLuan>();
        }

        public long Id { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "Bạn chưa nhập tiêu đề tin tức")]
        public string TieuDe { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "Bạn chưa nhập tóm tắt tin tức")]
        public string TomTat { get; set; }

        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "Bạn chưa nhập nội dung tin tức")]
        public string NoiDung { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Bạn chưa chọn hình ảnh tin tức")]
        public string HinhAnh { get; set; }

        public long? IdDanhMucTinTuc { get; set; }

        public DateTime? NgayTao { get; set; }

        public DateTime? NgaySua { get; set; }

        public int? IdNguoiTao { get; set; }

        public bool TrangThai { get; set; }

        public int? LuotXem { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Bạn chưa nhập tác giả")]
        public string TacGia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        public virtual DanhmucTinTuc DanhmucTinTuc { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
