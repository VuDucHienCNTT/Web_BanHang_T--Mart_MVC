using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_ThucPham.Areas.Admin.Code
{
    [Serializable]
    public class UserInfo
    {
        public long ID { set; get; }
        public string email { set; get; }
    }
}