using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicAuthentication.Secuirity
{
    public class UserValidate
    {
        public static bool Login(String Username,string Password)
        {
            using (APIEntities userentity = new APIEntities())
            {
                return userentity.logins.Any(user => user.Username.Equals(Username, StringComparison.OrdinalIgnoreCase)
                && user.Password == Password);
            }
        }
        public static login UserDetails(string Username, string Password)
        { 
            using (APIEntities userentity = new APIEntities())
            {
                return userentity.logins.FirstOrDefault(user => user.Username.Equals(Username, StringComparison.OrdinalIgnoreCase)
                && user.Password == Password);
            }
        }
    }
}