using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
   public class LienHeDAO
    {
        Web_ThucPhamDbContext db = null;
        public LienHeDAO()
        {
            db = new Web_ThucPhamDbContext();
        }

        public int ThemLienHe(LienHe lh)
        {
            db.LienHes.Add(lh);
            db.SaveChanges();
            return lh.Id;
        }
    }
}
