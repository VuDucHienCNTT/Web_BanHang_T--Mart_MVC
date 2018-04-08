using Model.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class QuangCaoDAO
    {
        Web_ThucPhamDbContext db = null;
        public QuangCaoDAO()
        {
            db = new Web_ThucPhamDbContext();
        }

        public IEnumerable<QuangCao> DanhSachQuangCao(string tukhoa, int page, int pageSize)
        {
            IQueryable<QuangCao> model = db.QuangCaos;
            if (!String.IsNullOrEmpty(tukhoa))
            {
                model = db.QuangCaos.Where(n => n.LinkQc.Contains(tukhoa) && n.TrangThai == true);
            }
            return model.OrderByDescending(n => n.Id).ToPagedList(page, pageSize);
        }

        public QuangCao ViewDetailByID(int id)
        {
            return db.QuangCaos.Find(id);
        }

        public IEnumerable<QuangCao> _QuangCaoTrangChu()
        {
            return db.QuangCaos.Where(n => n.TrangThai==true).OrderByDescending(n=>n.Id).Take(3);
        }

        public long Them(QuangCao qc)
        {
            try
            {
                db.QuangCaos.Add(qc);
                db.SaveChanges();
                return qc.Id;
            }
            catch
            {
                return 0;
            }
        }

        public bool Sua(QuangCao qc)
        {
            try
            {
                var quangcao = db.QuangCaos.Find(qc.Id);
                quangcao.LinkQc = qc.LinkQc;
                quangcao.AnhQc = qc.AnhQc;
                quangcao.TrangThai = qc.TrangThai;
                quangcao.IdNguoiTao = qc.IdNguoiTao;
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
                var xoapc = db.QuangCaos.Find(id);
                db.QuangCaos.Remove(xoapc);
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
            var qcao = db.QuangCaos.Find(id);
            qcao.TrangThai = !qcao.TrangThai;
            db.SaveChanges();
            return qcao.TrangThai;
        }
    }
}
