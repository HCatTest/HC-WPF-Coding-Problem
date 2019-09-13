using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WPFUserSearch.Data.Models.DB;

namespace WPFUserSearch.Data.Helpers
{
    public static class Various
    {
        public static string GetUserFullName(User user)
        {
            // Format full name with proper number of spaces between FirstName, LastName, and possible MiddleName
            string middleName = string.IsNullOrEmpty(user.MiddleName) ? " " : " " + user.MiddleName + " ";
            return user.FirstName + middleName + user.LastName;
        }
    }
}
