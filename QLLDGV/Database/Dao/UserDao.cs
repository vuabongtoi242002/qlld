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
    public class UserDao
    {
        UserDbContext db = null;
        public UserDao()
        {
            db = new UserDbContext();
        }
        public Guid Insert(QuanTri entity)
        {
            db.QuanTris.Add(entity);
            db.SaveChanges();
            return entity.MaAdmin;
        }
        public QuanTri GetByEmail(string userEmail)
        {
            return db.QuanTris.SingleOrDefault(x => x.Email == userEmail);
        }
        public bool Login(string userName, string passWord)
        {
            var result = db.QuanTris.Count(x => x.Email == userName && x.MatKhau == passWord);
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
