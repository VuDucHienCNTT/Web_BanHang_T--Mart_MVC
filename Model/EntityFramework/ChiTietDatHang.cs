namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDatHang")]
    public partial class ChiTietDatHang
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IdDatHang { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IdSanPham { get; set; }

        public int? SoLuong { get; set; }

        public decimal? GiaSanPham { get; set; }

        public decimal? TongTien { get; set; }

        public virtual DatHang DatHang { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
