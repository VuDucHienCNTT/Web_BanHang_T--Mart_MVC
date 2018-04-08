namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoan()
        {
            BinhLuans = new HashSet<BinhLuan>();
            QuangCaos = new HashSet<QuangCao>();
            SanPhams = new HashSet<SanPham>();
            Slides = new HashSet<Slide>();
            TinTucs = new HashSet<TinTuc>();
        }

        public int Id { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Bạn chưa nhập tên hiển thị")]
        public string TenHienThi { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Bạn chưa nhập Email đăng nhập")]
        [DataType(DataType.EmailAddress)]
        public string EmailDangNhap { get; set; }

        [StringLength(50)]
        //[Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        public bool TrangThai { get; set; }

        [StringLength(50)]
        public string QuyenHan { get; set; }

        public DateTime? NgayDangKy { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ")]
        public string DiaChi { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại")]
        public string SoDT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuangCao> QuangCaos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Slide> Slides { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TinTuc> TinTucs { get; set; }
    }
}
