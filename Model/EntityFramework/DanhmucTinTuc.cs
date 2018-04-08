namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhmucTinTuc")]
    public partial class DanhmucTinTuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DanhmucTinTuc()
        {
            DanhmucTinTuc1 = new HashSet<DanhmucTinTuc>();
            TinTucs = new HashSet<TinTuc>();
        }

        public long Id { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Bạn chưa nhập tên danh mục tin tức")]
        public string TenDanhMuc { get; set; }

        public long? ParentId { get; set; }

        public bool TrangThai { get; set; }

        [StringLength(50)]
        //[Required(ErrorMessage = "Bạn chưa nhập link danh mục tin tức")]
        public string Link { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhmucTinTuc> DanhmucTinTuc1 { get; set; }

        public virtual DanhmucTinTuc DanhmucTinTuc2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TinTuc> TinTucs { get; set; }
    }
}
