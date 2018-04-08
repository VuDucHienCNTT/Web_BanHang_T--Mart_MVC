namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhmucSanPham")]
    public partial class DanhmucSanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DanhmucSanPham()
        {
            DanhmucSanPham1 = new HashSet<DanhmucSanPham>();
            SanPhams = new HashSet<SanPham>();
        }

        public long Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Bạn chưa nhập tên danh mục sản phẩm")]
        public string TenDanhMuc { get; set; }

        public long? ParentId { get; set; }

        public bool TrangThai { get; set; }

        [StringLength(50)]
        //[Required(ErrorMessage = "Bạn chưa nhập link danh mục sản phẩm")]
        public string Link { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhmucSanPham> DanhmucSanPham1 { get; set; }

        public virtual DanhmucSanPham DanhmucSanPham2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
