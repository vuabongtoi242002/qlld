using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLLDGV
{
    [Serializable]
    public class UserLogin
    {
        public System.Guid UserID { get; set; }
        public string UserName { get; set; }
    }
}