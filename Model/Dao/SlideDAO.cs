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
    public class SlideDAO
    {
        Web_ThucPhamDbContext db = null;
        public SlideDAO()
        {
            db = new Web_ThucPhamDbContext();
        }

        public IEnumerable<Slide> DsSlide(string tukhoa, int page, int pageSize)
        {
            IQueryable<Slide> model = db.Slides;
            if (!String.IsNullOrEmpty(tukhoa))
            {
                model = db.Slides.Where(n => n.MoTaSlide.Contains(tukhoa));
            }
            return model.OrderByDescending(n => n.Id).ToPagedList(page, pageSize);
        }

        public List<Slide> SlideTrangChu()
        {
            return db.Slides.Where(n => n.TrangThai == true).Take(3).ToList();
        }

        public Slide ViewDetailByID(int id)
        {
            return db.Slides.Find(id);

        }

        public long Them(Slide sl)
        {
            try
            {
                db.Slides.Add(sl);
                db.SaveChanges();
                return sl.Id;
            }
            catch
            {
                return 0;
            }
        }


        public bool Sua(Slide sl)
        {
            try
            {
                var slide = db.Slides.Find(sl.Id);
                slide.LinkSlide = sl.LinkSlide;
                slide.MoTaSlide = sl.MoTaSlide;
                slide.HinhAnh = sl.HinhAnh;
                slide.NgayTao = DateTime.Now;
                slide.IdNguoiTao = sl.IdNguoiTao;
                slide.TrangThai = sl.TrangThai;
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
                var xoasl = db.Slides.Find(id);
                db.Slides.Remove(xoasl);
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
            var slide = db.Slides.Find(id);
            slide.TrangThai = !slide.TrangThai;
            db.SaveChanges();
            return slide.TrangThai;
        }
    }
}
