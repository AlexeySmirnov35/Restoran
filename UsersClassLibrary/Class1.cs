using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersClassLibrary
{
    public class Class1
    {
        public static bool ValidateUser(string password, string login)
        {
            if (password == null || login == null)
                return false;
            else if (password == "1" && login == "ivanov")
                return true;
            else return false;
        }
        public static bool ValidateClick(bool click)
        {
            if (click == false)
                return false;
            else return true;
        }
        public static bool ValidateAge(string age)
        {
            if (!int.TryParse(age, out var result))
                return false;
            else return true;
        }
    }
}
