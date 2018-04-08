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
    public class DanhMucTinTucDAO
    {
        Web_ThucPhamDbContext db = null;
        public DanhMucTinTucDAO()
        {
            db = new Web_ThucPhamDbContext();
        }

        public IEnumerable<DanhmucTinTuc> TinTucTrangChu()
        {
            return db.DanhmucTinTucs.OrderBy(n => n.Id).ToList();
        }

        public List<DanhmucTinTuc> DsDanhMucTin()
        {
            return db.DanhmucTinTucs.Where(n => n.TrangThai == true && !(n.Link == null && n.ParentId == null)).ToList();
        }
        public List<DanhmucTinTuc> DsDanhMucTinParentID()
        {
            return db.DanhmucTinTucs.Where(n => n.TrangThai == true && n.ParentId == null).ToList();
        }

        public IEnumerable<DanhmucTinTuc> DsDanhMuc(string tukhoa, int page, int pageSize)
        {
            IQueryable<DanhmucTinTuc> model = db.DanhmucTinTucs;
            if (!String.IsNullOrEmpty(tukhoa))
            {
                model = db.DanhmucTinTucs.Where(n => n.TenDanhMuc.Contains(tukhoa));
            }
            return model.OrderBy(n => n.Id).ToPagedList(page, pageSize);
        }

        public DanhmucTinTuc ViewDetailByID(long id)
        {
            return db.DanhmucTinTucs.Find(id);

        }

        public long Them(DanhmucTinTuc dmtt)
        {
            try
            {
                var checkTenDanhMuc = db.DanhmucTinTucs.SingleOrDefault(n => n.TenDanhMuc == dmtt.TenDanhMuc);
                if (checkTenDanhMuc == null)
                {
                    db.DanhmucTinTucs.Add(dmtt);
                    db.SaveChanges();
                    return dmtt.Id;
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


        public bool Sua(DanhmucTinTuc dmtt)
        {
            try
            {
                var dmuctintuc = db.DanhmucTinTucs.Find(dmtt.Id);
                dmuctintuc.TenDanhMuc = dmtt.TenDanhMuc;
                dmuctintuc.ParentId = dmtt.ParentId;
                dmuctintuc.TrangThai = dmtt.TrangThai;
                dmuctintuc.Link = dmtt.Link;
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
                var xoadmtt = db.DanhmucTinTucs.Find(id);
                db.DanhmucTinTucs.Remove(xoadmtt);
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
            var dmtt = db.DanhmucTinTucs.Find(id);
            dmtt.TrangThai = !dmtt.TrangThai;
            db.SaveChanges();
            return dmtt.TrangThai;
        }
    }
}
