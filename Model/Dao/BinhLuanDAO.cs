using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
using PagedList;

namespace Model.Dao
{
    public class BinhLuanDAO
    {

        Web_ThucPhamDbContext db = null;
        public BinhLuanDAO()
        {
            db = new Web_ThucPhamDbContext();
        }

        public IEnumerable<BinhLuan> BinhLuanSanPham(string tukhoa, int page, int pageSize)
        {
            IQueryable<BinhLuan> model = db.BinhLuans.Where(n => n.IdSanPham != null);
            if (!String.IsNullOrEmpty(tukhoa))
            {
                model = db.BinhLuans.Where(n => n.NoiDung.Contains(tukhoa) && n.TrangThai == true && n.IdSanPham != null);
            }
            return model.OrderByDescending(n => n.NgayBinhLuan).ToPagedList(page, pageSize);
        }


        public IEnumerable<BinhLuan> BinhLuanTinTuc(string tukhoa, int page, int pageSize)
        {
            IQueryable<BinhLuan> model = db.BinhLuans.Where(n => n.IdTinTuc != null);
            if (!String.IsNullOrEmpty(tukhoa))
            {
                model = db.BinhLuans.Where(n => n.NoiDung.Contains(tukhoa) && n.TrangThai == true && n.IdTinTuc != null);
            }
            return model.OrderByDescending(n => n.NgayBinhLuan).ToPagedList(page, pageSize);
        }


        public List<BinhLuan> HienThiBinhLuan(long id = 0)
        {
            return db.BinhLuans.Where(n => n.IdTinTuc == id && n.TrangThai == true).ToList();
        }

        public List<BinhLuan> HienThiBinhLuanSPham(long id = 0)
        {
            return db.BinhLuans.Where(n => n.IdSanPham == id && n.TrangThai == true).ToList();
        }

        public long DangBinhLuan(BinhLuan bl)
        {
            try
            {
                bl.NgayBinhLuan = DateTime.Parse(DateTime.Now.ToString());
                bl.TrangThai = true;
                bl.NoiDung = bl.NoiDung;
                bl.Url = bl.Url;
                db.BinhLuans.Add(bl);
                db.SaveChanges();
                return bl.Id;
            }
            catch
            {
                return 0;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var xoabl = db.BinhLuans.Find(id);
                db.BinhLuans.Remove(xoabl);
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
            var bluan = db.BinhLuans.Find(id);
            bluan.TrangThai = !bluan.TrangThai;
            db.SaveChanges();
            return bluan.TrangThai;
        }
    }
}
