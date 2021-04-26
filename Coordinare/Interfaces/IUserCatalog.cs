using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Models;

namespace Coordinare.Interfaces
{
    public interface IUserCatalog
    {
        List<User> Users { get; set; }
        List<User> GetAllUsersAsync();
        User GetUserFromIdAsync(int id);
        void CreateUserAsync(User user);
        void DeleteUserAsync(int id);
        void UpdateUserAsync(User user, int id);
    }
}
