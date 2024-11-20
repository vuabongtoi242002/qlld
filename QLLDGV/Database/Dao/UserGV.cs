using Database.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;


namespace Database.Dao
{
    public class UserGV
    {
        UserDbContext db = null;
        public UserGV()
        {
            db = new UserDbContext();
        }
        public Guid Insert(GIAOVIEN entity)
        {
            db.GIAOVIENs.Add(entity);
            db.SaveChanges();
            return entity.MaGV;
        }
        public GIAOVIEN GetByEmail(string userEmail)
        {
            return db.GIAOVIENs.SingleOrDefault(x => x.EmailGV == userEmail);
        }
        public bool Login(string userName, string passWord)
        {
            var result = db.GIAOVIENs.Count(x => x.EmailGV == userName && x.MatKhauGV == passWord);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
