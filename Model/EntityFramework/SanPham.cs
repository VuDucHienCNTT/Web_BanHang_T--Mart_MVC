namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            BinhLuans = new HashSet<BinhLuan>();
            ChiTietDatHangs = new HashSet<ChiTietDatHang>();
        }

        public long Id { get; set; }

        [StringLength(50)]
        [MaxLength(50, ErrorMessage = "Tên sản phẩm tối đa 50 ký tự")]
        [Required(ErrorMessage = "Bạn chưa nhập tên sản phẩm")]
        public string TenSanPham { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Bạn chưa chọn hình ảnh")]
        public string HinhAnh { get; set; }

        //[Required(ErrorMessage = "Bạn chưa nhập giá sản phẩm")]
        public decimal? GiaSanPham { get; set; }

        //[Required(ErrorMessage = "Bạn chưa nhập giá khuyến mại sản phẩm")]
        public decimal? GiaKhuyenMai { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập số lượng sản phẩm")]
        public int? SoLuong { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Bạn chưa nhập đơn vị cho sản phẩm")]
        public string DonVi { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Bạn chưa nhập nguồn gốc sản phẩm")]
        public string NguonGoc { get; set; }

        // [Required(ErrorMessage = "Bạn chưa nhập mô tả sản phẩm")]
        public string MoTaSanPham { get; set; }

        public bool TrangThai { get; set; }

        public int? LuotXem { get; set; }

        public int? TopSanPham { get; set; }

        public DateTime? NgayTao { get; set; }

        public DateTime? NgaySua { get; set; }

        public int? IdNguoiTao { get; set; }

        public long? IdDanhMucSanPham { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        public virtual DanhmucSanPham DanhmucSanPham { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDatHang> ChiTietDatHangs { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
