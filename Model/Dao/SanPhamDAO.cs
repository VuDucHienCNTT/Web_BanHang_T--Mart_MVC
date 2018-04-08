using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
using PagedList;
using System.Web;

namespace Model.Dao
{
    public class SanPhamDAO
    {
        Web_ThucPhamDbContext db = null;
        public SanPhamDAO()
        {
            db = new Web_ThucPhamDbContext();
        }

        public IEnumerable<SanPham> AllSanPham(string tukhoa, int page, int pageSize)
        {
            IQueryable<SanPham> model = db.SanPhams;
            if (!String.IsNullOrEmpty(tukhoa))
            {
                model = db.SanPhams.Where(n => n.TenSanPham.Contains(tukhoa));
            }
            return model.OrderByDescending(n => n.Id).ToPagedList(page, pageSize);
        }

        public IEnumerable<SanPham> SanPhamDaDuyet(string tukhoa, int page, int pageSize)
        {
            IQueryable<SanPham> model = db.SanPhams.Where(n => n.TrangThai.Equals(true));
            if (!String.IsNullOrEmpty(tukhoa))
            {
                model = db.SanPhams.Where(n => n.TenSanPham.Contains(tukhoa));
            }
            return model.Where(n => n.TrangThai.Equals(true)).OrderByDescending(n => n.Id).ToPagedList(page, pageSize);
        }

        public IEnumerable<SanPham> SanPhamChoDuyet(string tukhoa, int page, int pageSize)
        {
            IQueryable<SanPham> model = db.SanPhams.Where(n => n.TrangThai.Equals(false));
            if (!String.IsNullOrEmpty(tukhoa))
            {
                model = db.SanPhams.Where(n => n.TenSanPham.Contains(tukhoa));
            }
            return model.Where(n => n.TrangThai.Equals(false)).OrderByDescending(n => n.Id).ToPagedList(page, pageSize);
        }


        public IEnumerable<SanPham> SanPhamTop(string tukhoa, int page, int pageSize)
        {
            IQueryable<SanPham> model = db.SanPhams.Where(n => n.TrangThai.Equals(true) && n.TopSanPham != 0);
            if (!String.IsNullOrEmpty(tukhoa))
            {
                model = db.SanPhams.Where(n => n.TenSanPham.Contains(tukhoa));
            }
            return model.Where(n => n.TrangThai.Equals(true) && n.TopSanPham != 0).OrderByDescending(n => n.Id).ToPagedList(page, pageSize);
        }

        // Lấy sản phẩm top 1

        public List<SanPham> _SanPhamTop1()
        {
            return db.SanPhams.Where(n => n.TopSanPham == 1 && n.TrangThai == true).OrderByDescending(n => n.NgayTao).Take(1).ToList();
        }


        // Lấy sản phẩm top 2

        public List<SanPham> _SanPhamTop2()
        {
            return db.SanPhams.Where(n => n.TopSanPham == 2 && n.TrangThai == true).OrderByDescending(n => n.NgayTao).Take(1).ToList();
        }

        // Lấy sản phẩm top 3

        public List<SanPham> _SanPhamTop3()
        {
            return db.SanPhams.Where(n => n.TopSanPham == 3 && n.TrangThai == true).OrderByDescending(n => n.NgayTao).Take(1).ToList();
        }

        // Lấy tất cả sản phẩm

        public IEnumerable<SanPham> _ListSanPham(int page, int pageSize)
        {
            return db.SanPhams.Where(n => n.TopSanPham == 0 && n.TrangThai == true).OrderByDescending(n => n.Id).ToPagedList(page, pageSize);
        }

        // Sản phẩm xem nhiều

        public IEnumerable<SanPham> _SanPhamXemNhieu()
        {
            return db.SanPhams.Where(n => n.TrangThai == true).OrderByDescending(n => n.LuotXem).Take(8).ToList();
        }

        public SanPham ChiTiet(long id)
        {
            SanPham sp = db.SanPhams.Find(id);
            sp.LuotXem++;
            db.SaveChanges();
            return sp;
        }


        public SanPham ViewDetailByID(long id)
        {
            return db.SanPhams.Find(id);
        }

        public List<SanPham> SanPhamLienQuan(long id)
        {
            var sanpham = db.SanPhams.Find(id);
            return db.SanPhams.Where(n => n.Id != id && n.IdDanhMucSanPham == sanpham.IdDanhMucSanPham && n.TrangThai == true).Take(12).ToList();
        }

        public IEnumerable<SanPham> DanhSachSanPhamTrongDanhMuc(long id, int page, int pageSize)
        {
            return db.SanPhams.Where(x => x.IdDanhMucSanPham == id && x.TrangThai == true).OrderByDescending(n => n.Id).ToPagedList(page, pageSize);
        }


        public IEnumerable<SanPham> TimKiemSPNguoiDung(string tukhoa, string giaspham, int page, int pageSize)
        {
            string[] gia = giaspham.Split('-');
            int giathap = Convert.ToInt32(gia[0]);
            int giacao = Convert.ToInt32(gia[1]);

            IQueryable<SanPham> model = db.SanPhams.Where(n => n.TrangThai == true && (n.TenSanPham.Contains(tukhoa) || n.TenSanPham.StartsWith(tukhoa)
            || n.TenSanPham.EndsWith(tukhoa) || tukhoa == null) && (n.GiaSanPham >= giathap || giathap == 0) && (n.GiaSanPham <= giacao || giacao == 0));

            return model.OrderByDescending(n => n.TenSanPham).ToPagedList(page, pageSize);
        }


        public long Them(SanPham sp)
        {
            try
            {
                var checkTenSp = db.SanPhams.SingleOrDefault(n => n.TenSanPham == sp.TenSanPham);
                if (checkTenSp == null)
                {
                    sp.NgayTao = DateTime.Now;
                    if (sp.GiaKhuyenMai == 0)
                    {
                        sp.GiaKhuyenMai = null;
                    }
                    else
                    {
                        sp.GiaKhuyenMai = sp.GiaSanPham - ((sp.GiaSanPham * sp.GiaKhuyenMai) / 100);
                    }
                    sp.LuotXem = 0;
                    sp.NgaySua = null;
                    db.SanPhams.Add(sp);
                    db.SaveChanges();
                    return sp.Id;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }


        public bool Sua(SanPham sp)
        {
            try
            {
                var spham = db.SanPhams.Find(sp.Id);
                spham.TenSanPham = sp.TenSanPham;
                spham.HinhAnh = sp.HinhAnh;
                spham.TrangThai = sp.TrangThai;
                spham.GiaSanPham = sp.GiaSanPham;
                if (sp.GiaKhuyenMai == 0)
                {
                    spham.GiaKhuyenMai = null;
                }
                else
                {
                    spham.GiaKhuyenMai = sp.GiaSanPham - ((sp.GiaSanPham * sp.GiaKhuyenMai) / 100);
                }
                spham.NguonGoc = sp.NguonGoc;
                spham.MoTaSanPham = sp.MoTaSanPham;
                spham.SoLuong = sp.SoLuong;
                spham.NgaySua = DateTime.Now;
                spham.TopSanPham = sp.TopSanPham;
                spham.IdNguoiTao = sp.IdNguoiTao;
                spham.IdDanhMucSanPham = sp.IdDanhMucSanPham;
                db.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var xoasp = db.SanPhams.Find(id);
                db.SanPhams.Remove(xoasp);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ThayDoiTrangThai(long id)
        {
            var sp = db.SanPhams.Find(id);
            sp.TrangThai = !sp.TrangThai;
            db.SaveChanges();
            return sp.TrangThai;
        }
    }
}
