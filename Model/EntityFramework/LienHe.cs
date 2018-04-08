namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LienHe")]
    public partial class LienHe
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string TenKhachHang { get; set; }

        [StringLength(50)]
        public string Sdt { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(100)]
        public string DiaChi { get; set; }

        [StringLength(250)]
        public string NoiDung { get; set; }

        public DateTime? NgayGui { get; set; }

        public bool? TrangThai { get; set; }
    }
}
