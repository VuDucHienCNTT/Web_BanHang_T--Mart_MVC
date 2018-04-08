using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Model.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace Web_ThucPham.Areas.Admin.Report
{
    public partial class ThongKeSanPham : DevExpress.XtraReports.UI.XtraReport
    {
        public ThongKeSanPham(string tukhoa, string tungay, string denngay)
        {
            Web_ThucPhamDbContext db = new Web_ThucPhamDbContext();

            InitializeComponent();
            IQueryable<SanPham> model = db.SanPhams;

            if (!string.IsNullOrEmpty(tukhoa))
            {
                model = model.Where(n => n.TenSanPham.StartsWith(tukhoa) || n.TenSanPham.EndsWith(tukhoa) || n.TenSanPham.Contains(tukhoa));
            }
            if (!string.IsNullOrEmpty(tungay) || !string.IsNullOrEmpty(denngay))
            {
                string dateFrom = tungay.Split('-')[1] + "/" + tungay.Split('-')[2] + "/" + tungay.Split('-')[0];
                string dateTo = denngay.Split('-')[1] + "/" + denngay.Split('-')[2] + "/" + denngay.Split('-')[0];
                var _tungay = Convert.ToDateTime(dateFrom);
                var _denngay = Convert.ToDateTime(dateTo).AddDays(1).AddMilliseconds(-1);
                model = model.Where(n => n.NgayTao >= _tungay && n.NgayTao <= _denngay);
            }
            this.DataSource = model.ToList();

            xrtblTenSanPham.DataBindings.Add("Text", this.DataSource, "TenSanPham");
            xrtblGiaSanPham.DataBindings.Add("Text", this.DataSource, "GiaSanPham");
            xrtblGiaKhuyenMai.DataBindings.Add("Text", this.DataSource, "GiaKhuyenMai");
            xrtblNgayTao.DataBindings.Add("Text", this.DataSource, "NgayTao", "{0:dd/MM/yyyy}");
        }

        private void ThongKeSanPham_AfterPrint(object sender, EventArgs e)
        {
            PrintingSystem.Document.Name = "ThongkeSP-" + DateTime.Now.ToString("dd-MM-yyyy-hh:mm");
        }
    }
}
