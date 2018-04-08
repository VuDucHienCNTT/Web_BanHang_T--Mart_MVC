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
    public class MenuDAO
    {
        Web_ThucPhamDbContext db = null;
        public MenuDAO()
        {
            db = new Web_ThucPhamDbContext();
        }

        public IEnumerable<Menu> DsMenu(string tukhoa, int page, int pageSize)
        {
            IQueryable<Menu> model = db.Menus;
            if (!String.IsNullOrEmpty(tukhoa))
            {
                model = db.Menus.Where(n => n.TenMenu.Contains(tukhoa));
            }
            return model.OrderByDescending(n => n.Id).ToPagedList(page, pageSize);
        }

        public List<Menu> DsMenu()
        {
            return db.Menus.Where(n => n.TrangThai == true).ToList();
        }

        public Menu ViewDetailByID(int id)
        {
            return db.Menus.Find(id);
        }

        public long Them(Menu mn)
        {
            try
            {
                db.Menus.Add(mn);
                db.SaveChanges();
                return mn.Id;
            }
            catch
            {
                return 0;
            }
        }


        public bool Sua(Menu mn)
        {
            try
            {
                var menu = db.Menus.Find(mn.Id);
                menu.TenMenu = mn.TenMenu;
                menu.Link = mn.Link;
                menu.TrangThai = mn.TrangThai;
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
                var xoamn = db.Menus.Find(id);
                db.Menus.Remove(xoamn);
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
            var menu = db.Menus.Find(id);
            menu.TrangThai = !menu.TrangThai;
            db.SaveChanges();
            return menu.TrangThai;
        }
    }
}
