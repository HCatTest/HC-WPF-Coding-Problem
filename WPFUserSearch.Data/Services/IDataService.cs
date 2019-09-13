using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WPFUserSearch.Data.Models.DB;

namespace WPFUserSearch.Data.Services
{
    public interface IDataService
    {
        Task<List<User>> GetUsers();
        Task<List<User>> GetUsers(string fullNameLike);
        Task<string> CreateUser(User user);
        Task<List<string>> GetUsersFullNames();
    }
}
