namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Bạn chưa nhập tên menu")]
        [Display(Name = "Tên menu")]
        public string TenMenu { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Bạn chưa nhập link menu")]
        [Display(Name = "Link menu")]
        public string Link { get; set; }

        public bool TrangThai { get; set; }
    }
}
