namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DatHang")]
    public partial class DatHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DatHang()
        {
            ChiTietDatHangs = new HashSet<ChiTietDatHang>();
        }

        public long Id { get; set; }

        public DateTime? NgayTao { get; set; }

        public long? IdKhachHang { get; set; }

        [StringLength(50)]
        public string TenNguoiNhan { get; set; }

        [StringLength(50)]
        public string SDTNguoiNhan { get; set; }

        [StringLength(100)]
        public string DiaChiNguoiNhan { get; set; }

        [StringLength(50)]
        public string EmailNguoiNhan { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        public bool? TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDatHang> ChiTietDatHangs { get; set; }
    }
}
