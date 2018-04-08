using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
using PagedList;
using PagedList.Mvc;

namespace Model.Dao
{
    public class TinTucDAO
    {
        Web_ThucPhamDbContext db = null;

        public TinTucDAO()
        {
            db = new Web_ThucPhamDbContext();
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

        public IEnumerable<TinTuc> TinDaDuyet(string tukhoa, int page, int pageSize)
        {
            IQueryable<TinTuc> model = db.TinTucs.Where(n => n.TrangThai.Equals(true));
            if (!String.IsNullOrEmpty(tukhoa))
            {
                model = db.TinTucs.Where(n => n.TieuDe.Contains(tukhoa) || n.TomTat.Contains(tukhoa));
            }
            return model.Where(n => n.TrangThai.Equals(true)).OrderByDescending(n => n.NgayTao).ToPagedList(page, pageSize);
        }

        public IEnumerable<TinTuc> TinChoDuyet(string tukhoa, int page, int pageSize)
        {
            IQueryable<TinTuc> model = db.TinTucs.Where(n => n.TrangThai.Equals(false));
            if (!String.IsNullOrEmpty(tukhoa))
            {
                model = db.TinTucs.Where(n => n.TieuDe.Contains(tukhoa) || n.TomTat.Contains(tukhoa));
            }
            return model.Where(n => n.TrangThai.Equals(false)).OrderByDescending(n => n.NgayTao).ToPagedList(page, pageSize);
        }

        public IEnumerable<TinTuc> TimKiemTinTucNguoiDung(string tukhoa, int page, int pageSize)
        {
            IQueryable<TinTuc> model = db.TinTucs.Where(n => n.TrangThai == true);
            //if (!String.IsNullOrEmpty(tukhoa))
            //{
                model = db.TinTucs.Where((n => n.TieuDe.Contains(tukhoa) || n.TieuDe.StartsWith(tukhoa) || n.TieuDe.EndsWith(tukhoa) || n.TomTat.StartsWith(tukhoa) || n.TomTat.EndsWith(tukhoa) || n.TomTat.Contains(tukhoa) || tukhoa == null));
            //}
            return model.OrderByDescending(n => n.TieuDe).ToPagedList(page, pageSize);
        }

        public List<TinTuc> TinTucLienQuan(long id)
        {
            var tintuc = db.TinTucs.Find(id);
            return db.TinTucs.Where(n => n.Id != id && n.IdDanhMucTinTuc == tintuc.IdDanhMucTinTuc && n.TrangThai == true).Take(6).ToList();
        }

        public TinTuc ViewDetailByID(int id)
        {
            return db.TinTucs.Find(id);

        }

        // tất cả tin tức

        public List<TinTuc> _ListTinTuc()
        {
            return db.TinTucs.Where(n => n.TrangThai == true).OrderByDescending(n => n.NgayTao).ToList();
        }

        public TinTuc ChiTiet(long id)
        {
            TinTuc tt = db.TinTucs.Find(id);
            tt.LuotXem++;
            db.SaveChanges();
            return tt;
        }

        public IEnumerable<TinTuc> DanhSachTinTucTrongDanhMuc(long id, int page, int pageSize)
        {
            return db.TinTucs.Where(x => x.IdDanhMucTinTuc == id && x.TrangThai == true).OrderByDescending(n => n.Id).ToPagedList(page, pageSize);
        }


        public long Them(TinTuc tt)
        {
            try
            {
                db.TinTucs.Add(tt);
                db.SaveChanges();
                return tt.Id;
            }
            catch
            {
                return 0;
            }
        }

        public bool Sua(TinTuc tt)
        {
            try
            {
                var tintuc = db.TinTucs.Find(tt.Id);
                tintuc.TieuDe = tt.TieuDe;
                tintuc.TomTat = tt.TomTat;
                tintuc.NoiDung = tt.NoiDung;
                tintuc.HinhAnh = tt.HinhAnh;
                tintuc.NgaySua = DateTime.Now;
                tintuc.IdNguoiTao = tt.IdNguoiTao;
                tintuc.IdDanhMucTinTuc = tt.IdDanhMucTinTuc;
                tintuc.TrangThai = tt.TrangThai;
                tintuc.TacGia = tt.TacGia;
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
                var xoatt = db.TinTucs.Find(id);
                db.TinTucs.Remove(xoatt);
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
            var ttuc = db.TinTucs.Find(id);
            ttuc.TrangThai = !ttuc.TrangThai;
            db.SaveChanges();
            return ttuc.TrangThai;
        }
    }
}
