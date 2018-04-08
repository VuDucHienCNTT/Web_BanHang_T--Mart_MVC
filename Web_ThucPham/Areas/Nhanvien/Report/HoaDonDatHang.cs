using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Model.EntityFramework;
using System.Linq;

namespace Web_ThucPham.Areas.Nhanvien.Report
{
    public partial class HoaDonDatHang : DevExpress.XtraReports.UI.XtraReport
    {
        public HoaDonDatHang(long Id)
        {
            Web_ThucPhamDbContext db = new Web_ThucPhamDbContext();
            InitializeComponent();
            var hoadon = db.ChiTietDatHangs.Where(n => n.IdDatHang == Id).ToList();
            this.DataSource = hoadon;

            xrtblTenSanPham.DataBindings.Add("Text", this.DataSource, "IdSanPham");
            xrtblSoLuong.DataBindings.Add("Text", this.DataSource, "SoLuong");
            xrtblGiaSanPham.DataBindings.Add("Text", this.DataSource, "GiaSanPham");
            xrtblTongTien.DataBindings.Add("Text", this.DataSource, "TongTien");
        }

        private void HoaDonDatHang_AfterPrint(object sender, EventArgs e)
        {
            PrintingSystem.Document.Name = "Hoadon-" + DateTime.Now.ToString("dd-MM-yyyy-hh:mm");
        }
    }
}
