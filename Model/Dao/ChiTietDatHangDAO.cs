using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ChiTietDatHangDAO
    {
        Web_ThucPhamDbContext db = null;
        public ChiTietDatHangDAO()
        {
            db = new Web_ThucPhamDbContext();
        }

        public bool Them(ChiTietDatHang ctdhang)
        {
            try
            {
                db.ChiTietDatHangs.Add(ctdhang);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
