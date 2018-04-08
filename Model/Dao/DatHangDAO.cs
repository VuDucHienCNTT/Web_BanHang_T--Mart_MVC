using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
namespace Model.Dao
{
    public class DatHangDAO
    {
        Web_ThucPhamDbContext db = null;
        public DatHangDAO()
        {
            db = new Web_ThucPhamDbContext();
        }

        public long Them(DatHang dhang)
        {
            try
            {
                db.DatHangs.Add(dhang);
                db.SaveChanges();
                return dhang.Id;
            }
            catch
            {
                return 0;
            }
        }
    }
}
