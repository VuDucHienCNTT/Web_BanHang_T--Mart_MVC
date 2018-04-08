using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using PagedList.Mvc;

namespace Model.Dao
{
    public class TaiKhoanDAO
    {
        Web_ThucPhamDbContext db = null;
        public TaiKhoanDAO()
        {
            db = new Web_ThucPhamDbContext();
        }

        public List<TaiKhoan> dsTaiKhoan()
        {
            return db.TaiKhoans.Where(n => n.QuyenHan.Equals("Admin")).ToList();
        }

        public IEnumerable<TinTuc> AllTin(string tukhoa, int page, int pageSize)
        {
            IQueryable<TinTuc> model = db.TinTucs;
            if (!String.IsNullOrEmpty(tukhoa))
            {
                model = db.TinTucs.Where(n => n.TieuDe.Contains(tukhoa) || n.TomTat.Contains(tukhoa));
            }
            return model.OrderByDescending(n => n.NgayTao).ToPagedList(page, pageSize);
        }


        public IEnumerable<TaiKhoan> TatCaTkAdmin(string tukhoa, int page, int pageSize)
        {
            IQueryable<TaiKhoan> model = db.TaiKhoans.Where(n => n.QuyenHan.Equals("Admin"));
            if (!String.IsNullOrEmpty(tukhoa))
            {
                model = model.Where(n => n.TenHienThi.Contains(tukhoa) || n.EmailDangNhap.Contains(tukhoa));
            }
            return model.OrderByDescending(n => n.Id).ToPagedList(page, pageSize);
        }

        public IEnumerable<TaiKhoan> TatCaTkNhanVien(string tukhoa, int page, int pageSize)
        {
            IQueryable<TaiKhoan> model = db.TaiKhoans.Where(n => n.QuyenHan.Equals("NhanVien"));
            if (!String.IsNullOrEmpty(tukhoa))
            {
                model = model.Where(n => n.TenHienThi.Contains(tukhoa) || n.EmailDangNhap.Contains(tukhoa));
            }
            return model.OrderByDescending(n => n.Id).ToPagedList(page, pageSize);
        }

        public IEnumerable<TaiKhoan> TatCaTkKhachHang(string tukhoa, int page, int pageSize)
        {
            IQueryable<TaiKhoan> model = db.TaiKhoans.Where(n => n.QuyenHan.Equals("KhachHang"));
            if (!String.IsNullOrEmpty(tukhoa))
            {
                model = model.Where(n => n.TenHienThi.Contains(tukhoa) || n.EmailDangNhap.Contains(tukhoa));
            }
            return model.OrderByDescending(n => n.Id).ToPagedList(page, pageSize);
        }

        public long Them(TaiKhoan tk)
        {
            var checkTenDN = db.TaiKhoans.SingleOrDefault(n => n.TenHienThi == tk.TenHienThi);
            var checkEmail = db.TaiKhoans.SingleOrDefault(n => n.EmailDangNhap == tk.EmailDangNhap);
            if (checkEmail == null && checkTenDN == null)
            {
                db.TaiKhoans.Add(tk);
                db.SaveChanges();
                return tk.Id;
            }
            else
            {
                return 0;
            }

        }

        public bool Sua(TaiKhoan tk)
        {
            try
            {
                var taikhoan = db.TaiKhoans.Find(tk.Id);

                taikhoan.TenHienThi = tk.TenHienThi;
                if (taikhoan.MatKhau == tk.MatKhau)
                {
                    taikhoan.MatKhau = tk.MatKhau;
                }
                else
                {
                    taikhoan.MatKhau = Md5.EncryptMD5(tk.EmailDangNhap + tk.MatKhau);
                }

                taikhoan.DiaChi = tk.DiaChi;
                taikhoan.SoDT = tk.SoDT;
                taikhoan.QuyenHan = tk.QuyenHan;
                taikhoan.TrangThai = tk.TrangThai;
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
                var xoatk = db.TaiKhoans.SingleOrDefault(x => x.Id == id);
                db.TaiKhoans.Remove(xoatk);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckTenHienThi(string tenhienthi)
        {
            return db.TaiKhoans.Count(n => n.TenHienThi == tenhienthi) > 0;
        }

        public bool CheckEmail(string emaildangnhap)
        {
            return db.TaiKhoans.Count(n => n.EmailDangNhap == emaildangnhap) > 0;
        }


        public TaiKhoan GetByID(string email)
        {
            return db.TaiKhoans.SingleOrDefault(u => u.EmailDangNhap == email);
        }

        public TaiKhoan ViewDetailByID(int id)
        {
            return db.TaiKhoans.Find(id);

        }
        public bool ThayDoiTrangThai(long id)
        {
            var tkhoan = db.TaiKhoans.Find(id);
            tkhoan.TrangThai = !tkhoan.TrangThai;
            db.SaveChanges();
            return tkhoan.TrangThai;
        }

        public int DangNhap(string email, string matkhau)
        {
            var result = db.TaiKhoans.SingleOrDefault(x => x.EmailDangNhap == email);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.TrangThai == false)
                {
                    return -1;
                }
                else
                {
                    if (result.MatKhau != matkhau)
                    {
                        return -2;
                    }

                    else
                    {
                        if (result.QuyenHan == "Admin")
                        {
                            return 1;
                        }
                        else if (result.QuyenHan == "NhanVien")
                        {
                            return 2;
                        }
                        else
                        {
                            return 3;
                        }
                    }

                }
            }
        }
    }
}
