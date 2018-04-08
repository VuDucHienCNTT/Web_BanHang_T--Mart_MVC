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
    public class DanhMucSanPhamDAO
    {
        Web_ThucPhamDbContext db = null;
        public DanhMucSanPhamDAO()
        {
            db = new Web_ThucPhamDbContext();
        }

        public IEnumerable<DanhmucSanPham> SanPhamTrangChu()
        {
            return db.DanhmucSanPhams.OrderBy(n => n.Id).ToList();
        }

        public List<DanhmucSanPham> DsDanhMucSP()
        {
            return db.DanhmucSanPhams.Where(n => n.TrangThai == true && !(n.Link == null && n.ParentId == null)).ToList();
        }
        public List<DanhmucSanPham> DsDanhMucSPParentID()
        {
            return db.DanhmucSanPhams.Where(n => n.TrangThai == true && n.ParentId == null).ToList();
        }

        public IEnumerable<DanhmucSanPham> DsDanhMuc(string tukhoa, int page, int pageSize)
        {
            IQueryable<DanhmucSanPham> model = db.DanhmucSanPhams;
            if (!String.IsNullOrEmpty(tukhoa))
            {
                model = db.DanhmucSanPhams.Where(n => n.TenDanhMuc.Contains(tukhoa));
            }
            return model.OrderBy(n => n.Id).ToPagedList(page, pageSize);
        }

        public DanhmucSanPham ViewDetailByID(long id)
        {
            return db.DanhmucSanPhams.Find(id);

        }


        public long Them(DanhmucSanPham dmsp)
        {
            try
            {
                var checkTenDanhMuc = db.DanhmucSanPhams.SingleOrDefault(n => n.TenDanhMuc == dmsp.TenDanhMuc);
                if (checkTenDanhMuc == null)
                {
                    db.DanhmucSanPhams.Add(dmsp);
                    db.SaveChanges();
                    return dmsp.Id;
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


        public bool Sua(DanhmucSanPham dmsp)
        {
            try
            {
                var dmucspham = db.DanhmucSanPhams.Find(dmsp.Id);
                dmucspham.TenDanhMuc = dmsp.TenDanhMuc;
                dmucspham.ParentId = dmsp.ParentId;
                dmucspham.TrangThai = dmsp.TrangThai;
                dmucspham.Link = dmsp.Link;
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
                var xoadmsp = db.DanhmucSanPhams.Find(id);
                db.DanhmucSanPhams.Remove(xoadmsp);
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
            var dmsp = db.DanhmucSanPhams.Find(id);
            dmsp.TrangThai = !dmsp.TrangThai;
            db.SaveChanges();
            return dmsp.TrangThai;
        }
    }
}
